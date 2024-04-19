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

namespace Integra.WebApi.Controllers.Controllers;

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
        _logger = LogManager.GetCurrentClassLogger();
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
    [Route("api/[controller]/[action]")]
    public async Task<ActionResult<int>> CreateIncident([FromBody] SupportRequest request)
    {
        try
        {
            string id = await _incidentManager.CreateIncidentAsync(request);
            return Ok(id);
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message + " : " + ex.StackTrace);
        }

        return StatusCode(500);

    }


}