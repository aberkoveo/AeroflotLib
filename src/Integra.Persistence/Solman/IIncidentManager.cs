using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integra.Domain.Support;


namespace Integra.Persistence.Solman
{
    public interface IIncidentManager
    {
        Task<string> CreateIncidentAsync(SupportRequest request);
        Task<string> GetGuidAsync();
    }
}
