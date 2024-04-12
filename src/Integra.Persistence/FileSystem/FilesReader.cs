using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;


namespace Integra.Persistence.FileSystem
{
    public static class FilesReader
    {

        public static async Task<Dictionary<string,byte[]>> ReadFolderFilesAsync(string folderPath)
        {
            Dictionary<string, byte[]> result = new();

            string[] files = Directory.GetFiles(folderPath);

            if (files.Any())
            {
                foreach (string file in files)
                {
                    byte[] fileBytes = await File.ReadAllBytesAsync(file);

                    result[file] = fileBytes;
                }

                return result;
            }

            return result;
        }


    }
}
