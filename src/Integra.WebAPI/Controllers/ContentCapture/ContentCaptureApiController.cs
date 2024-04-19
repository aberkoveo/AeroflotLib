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

namespace Integra.WebApi.Controllers.Controllers;

[Route("api/[controller]")]
public class ContentCaptureApiController : BaseController
{
    private readonly IServiceProvider _serviceProvider;
    private readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

    public ContentCaptureApiController(IServiceProvider provider)
    {
        _serviceProvider = provider;
    }

    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        return Ok("Сервис интеграции ContentCapture для создания и обработки пакетов документов");
    }

    [HttpPost]
    public async Task<ActionResult<int>> HandleBatch([FromBody] ContentBatch batch)
    {
        _logger.Info($"Получен запрос на обработку пакета {batch.Name}.");
        _logger.Debug(JsonWriter.ConvertObject(batch));

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