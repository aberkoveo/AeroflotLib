using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;


namespace TestsIntegra
{
    public static class JsonWriter
    {
        /// <summary>
        /// Сериализирует объекты в читабельный вид
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>JSON строка с beautify форматированием</returns>
        public static string ConvertObject(Object obj)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            return JsonSerializer.Serialize(obj, options);
        }
    }
}
