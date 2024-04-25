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

    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        return Ok("Сервис интеграции ContentCapture");
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateSupportRequestDto createSupportRequestDto)
    {
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

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<int>> CreateIncident([FromBody] SupportRequest request)
    {
        try
        {
            string id = await _incidentManager.CreateIncidentAsync(request);

            if (id is null)
            {
                _logger.Error("Bad request:\n" + JsonWriter.ConvertObject(request));
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