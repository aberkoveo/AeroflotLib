using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;


namespace Integra.Persistence.FileSystem
{

    /// <summary>
    /// Читает все файлы из каталога по переданному пути.
    /// </summary>
    /// <returns>Словарь с ключом=имени файла, значением=массиву байт прочитанного файла</returns>
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

        public static async Task<Dictionary<string, byte[]>> ReadFromBase64Async(
            Dictionary<string, string> base64Dictionary)
        {
            Dictionary<string, byte[]> result = new();

            foreach (KeyValuePair<string, string> document in base64Dictionary)
            {
                result[document.Key] = Convert.FromBase64String(document.Value);
            }

            return result;
        }


    }
}
