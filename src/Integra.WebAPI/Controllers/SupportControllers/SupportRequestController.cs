using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Integra.WebApi.Models;
using Integra.Persistence;
using Integra.Application.SupportRequests.Commands;

using Integra.Domain.Support;
using Integra.Persistence.Solman;
using SolutionManagerApi;
using NLog;
using Integra.Persistence.Utils;

namespace Integra.WebApi.Controllers.SupportControllers;

[ApiVersionNeutral]
[Route("api/[controller]")]
public class SupportRequestController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IIncidentManager _incidentManager;
    private readonly NLog.ILogger _logger;

    public SupportRequestController(IMapper mapper, IIncidentManager manager)
    {
        _mapper = mapper;
        _incidentManager = manager;
        _logger = LogManager.GetLogger("SolmanLogger");
    }


    /// <summary>
    /// Заголовочный метод контроллера
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        return Ok("Сервис интеграции c Solman для создания инцидентов");
    }


    /// <summary>
    /// Выполняет запись инцидента в таблицу базы данных для ведения статистики
    /// </summary>
    /// <param name="createSupportRequestDto"></param>
    /// <returns>Внутренний для таблицы ID инцидента, автоинкремент</returns>
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateSupportRequestDto createSupportRequestDto)
    {
        _logger.Info($"Создание записи по инциденту {createSupportRequestDto.SMID}");

        try
        {
            var command = _mapper.Map<CreateSupportRequestCommand>(createSupportRequestDto);
            var supportRequestId = await Mediator.Send(command);
            return Ok(supportRequestId);
        }
        catch(Exception ex)
        {
            _logger.Error(ex.Message + " : " + ex.StackTrace);
        }

        return StatusCode(500);
        
    }


    /// <summary>
    /// Выполняет создание инцидента в Solution Manager
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Идентификатор инцидента в Solution Manager</returns>
    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<int>> CreateIncident([FromBody] SupportRequest request)
    {
        _logger.Info($"Запрос создания инцидента по пакету c ID={request.BatchId}");
        _logger.Debug($"{JsonWriter.ConvertObject(request)}");

        try
        {
            string id = await _incidentManager.CreateIncidentAsync(request);

            if (id is null)
            {
                throw new Exception("Создание инцидента в Solman не выполнено! ID = null");
            }

            return Ok(id);

        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message + " : " + ex.StackTrace);
        }

        return StatusCode(500);

    }


}