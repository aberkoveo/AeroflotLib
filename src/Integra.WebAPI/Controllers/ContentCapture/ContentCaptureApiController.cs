using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Integra.WebApi.Models;
using Integra.Persistence;
using Integra.Application.SupportRequests.Commands;
using Microsoft.AspNetCore.Authorization;
using Integra.Domain.ContentCapture;
using NLog;
using Integra.Persistence.ContentCapture.Web;
using Integra.WebApi.Utils;
using ILogger = NLog.ILogger;
using Integra.WebApi.Controllers.ContentCapture.Validation;
using FluentValidation;

namespace Integra.WebApi.Controllers.ContentCapture;

[Route("api/[controller]")]
public class ContentCaptureApiController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IValidator<ContentBatch> _validator;
    private readonly NLog.ILogger _logger = LogManager.GetLogger("ContentCaptureLogger");

    public ContentCaptureApiController(IServiceProvider provider, IValidator<ContentBatch> validator)
    {
        _serviceProvider = provider;
        _validator = validator;
    }


    /// <summary>
    /// Заголовочный метод контроллера
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        return Ok("Сервис интеграции ContentCapture для создания и обработки пакетов документов");
    }


    /// <summary>
    /// Загружает образы документов, создает пакет и
    /// запускает его в обработку ContentCapture
    /// </summary>
    /// <param name="batch"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<int>> HandleBatch([FromBody] ContentBatch batch)
    {
        _logger.Info($"Получен запрос на обработку пакета {batch.Name}.");
        _logger.Debug(JsonWriter.ConvertObject(batch));

        //Валидация входных данных
        var validationResult = await _validator.ValidateAsync(batch);
        
        if (!validationResult.IsValid)
        {
            string errorMessage = $"Данные пакета {batch.Name} переданы неверно:";
            foreach (var error in validationResult.Errors)
            {
                errorMessage += $"\r\n {error.PropertyName} : {error.ErrorMessage}";
            }
            _logger.Error(errorMessage);
            return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
        }

        using (var scope = _serviceProvider.CreateScope())
        {
            try
            {
                var batchManager = scope.ServiceProvider.GetRequiredService<IBatchManager>();

                int result = await batchManager.HandleBatchAsync(batch);

                if (result == 0)
                {
                    return BadRequest($"Запуск пакета {batch.Name} в обработку не может быть выполнен!");
                }

                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.Error(ex.Message + ": \n" + ex.StackTrace);
            }

            return StatusCode(500);
        }

        
    }
}