using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Persistence.FileSystem
{
    public interface IFilesReader
    {
        Task<IEnumerable<byte[]>> ReadFolderFilesAsync(string folderPath);
    }
  
}
