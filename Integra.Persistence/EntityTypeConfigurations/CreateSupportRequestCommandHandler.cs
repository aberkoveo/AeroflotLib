using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Integra.Domain;
using Integra.Application.Interfaces;

namespace Integra.Application.SupportRequests.Commands
{
    internal class CreateSupportRequestCommandHandle 
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
            };

            supportRequest.AddCategories(request.Categories);
            await _dbContext.SupportRequests.AddAsync(supportRequest, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return request.ID;
        }
        public CreateSupportRequestCommandHandle(ISupportRequestDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
