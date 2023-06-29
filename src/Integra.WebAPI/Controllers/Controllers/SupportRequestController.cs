using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Integra.WebApi.Models;
using Integra.Persistence;
using Integra.Application.SupportRequests.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Integra.WebApi.Controllers.Controllers;

[Route("api/[controller]")]
public class SupportRequestController : BaseController
{
    private readonly IMapper _mapper;
    
    public SupportRequestController(IMapper mapper) => _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        return Ok("Сервис интеграции ContentCapture");
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateSupportRequestDto createSupportRequestDto)
    {
        var command = _mapper.Map<CreateSupportRequestCommand>(createSupportRequestDto);
        var supportRequestId = await Mediator.Send(command);
        return Ok(supportRequestId);
        return Ok(1);
    }
}