using Integra.Application.Interfaces;
using Integra.Domain;
using MediatR;

namespace Integra.Application.SupportRequests.Commands;

internal class CreateSupportRequestCommandHandler : IRequestHandler<CreateSupportRequestCommand, int>
{
    private readonly ISupportRequestDBContext _dbContext;

    public CreateSupportRequestCommandHandler(ISupportRequestDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task<int> Handle(CreateSupportRequestCommand request,CancellationToken cancellationToken)
    {
        var supportRequest = new SupportRequest
        {
            
            SMID = request.SMID,
            Priority = request.Priority,
            BatchId = request.BatchId,
            BatchOwner = request.BatchOwner,
            Categories = request.Categories,
            Comment = request.Comment
        };
        await _dbContext.SupportRequests.AddAsync(supportRequest);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return supportRequest.ID;
    }

}