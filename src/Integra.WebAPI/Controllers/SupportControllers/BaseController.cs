using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Integra.WebApi.Controllers.SupportControllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator? Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}