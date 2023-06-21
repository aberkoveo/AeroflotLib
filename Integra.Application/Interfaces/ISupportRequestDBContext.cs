using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FC12.SupportExtensions.Models;

namespace Integra.Application.Interfaces
{
    public interface ISupportRequestDBContext
    {
        DbSet<SupportRequest> SupportRequests { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
