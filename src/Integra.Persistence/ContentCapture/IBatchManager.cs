using Integra.Domain.ContentCapture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Persistence.ContentCapture
{
    public interface IBatchManager
    {
        Task<int> HandleBatchAsync(ContentBatch contentBatch);
    }
}
