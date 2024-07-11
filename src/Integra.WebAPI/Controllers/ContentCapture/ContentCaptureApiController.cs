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
using FluentValidation.Results;

namespace Integra.WebApi.Controllers.ContentCapture;


[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
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
    /// Cоздает пакет, загружает образы документов из папки и
    /// запускает его в обработку ContentCapture
    /// </summary>
    /// <param name="batch"></param>
    /// <returns></returns>
    [HttpPost]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<int>> HandleBatchV1([FromBody] ContentBatch batch)
    {
        _logger.Info($"Получен запрос на обработку пакета {batch.Name}.");
        _logger.Debug(JsonWriter.ConvertObject(batch));

        //Валидация входных данных
        ContentBatchValidator validator = new ContentBatchValidator();

        var validationResult = await IsBatchValid(batch, validator);

        if (!validationResult.IsValid)
        {
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

    /// <summary>
    /// Cоздает пакет, создает образы документов из BASE64 и
    /// запускает его в обработку ContentCapture
    /// </summary>
    /// <param name="batch"></param>
    /// <returns></returns>
    [HttpPost]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult<int>> HandleBatchV2([FromBody] ContentBatch batch)
    {
        _logger.Info($"Получен запрос на обработку пакета {batch.Name}.");
        _logger.Trace($"Запрос от SAP:\n{JsonWriter.ConvertObject(batch)}");


        //Валидация входных данных
        ContentBatchBase64Validator validator = new ContentBatchBase64Validator();

        var validationResult = await IsBatchValid(batch, validator);

        if (!validationResult.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
        }

        using (var scope = _serviceProvider.CreateScope())
        {
            try
            {
                var batchManager = scope.ServiceProvider.GetRequiredService<IBatchManager>();

                int result = await batchManager.HandleBatchBase64Async(batch);

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

    private async Task<ValidationResult> IsBatchValid(ContentBatch batch, AbstractValidator<ContentBatch> validator)
    {
        var validationResult = await validator.ValidateAsync(batch);

        if (!validationResult.IsValid)
        {
            string errorMessage = $"Данные пакета {batch.Name} переданы неверно:";
            foreach (var error in validationResult.Errors)
            {
                errorMessage += $"\r\n {error.PropertyName} : {error.ErrorMessage}";
            }
            _logger.Error(errorMessage);
            
        }

        return validationResult;
    }

}