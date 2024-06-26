﻿using MediatR;
using Integra.Application.Interfaces;
using Integra.Domain.Support;

namespace Integra.Application.SupportRequests.Commands;


/// <summary>
/// Тип реализует обработчик для команды создания записи SupportRequest в СУБД.
/// СМ. документацию MediatR.
/// </summary>
public class CreateSupportRequestCommandHandle
    : IRequestHandler<CreateSupportRequestCommand, int>
{
    private readonly ISupportRequestDBContext _dbContext;

    public async Task<int> Handle(CreateSupportRequestCommand request,
        CancellationToken cancellationToken)
    {
        SupportRequest supportRequest = new SupportRequest
        {
            SMID = request.SMID,
            Priority = request.Priority,
            BatchId = request.BatchId,
            BatchOwner = request.BatchOwner,
            Comment = request.Comment,
            Categories = request.Categories,
            DocumentsIds = request.DocumentsIds
        };

        await _dbContext.SupportRequests.AddAsync(supportRequest, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return supportRequest.ID;
    }

    public CreateSupportRequestCommandHandle(ISupportRequestDBContext dbContext)
    {
        _dbContext = dbContext;
    }
}