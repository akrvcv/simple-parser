using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace simple_parser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.WriteLine("SIMPLE PARSER!");
            string currentTime = DateTime.Now.ToString();
            currentTime = currentTime.Replace(":", ".");
            string rawFilePath = @"..\..\..\source.txt";
            string sourceFilePath = @"..\..\..\source_" + currentTime + ".txt";
            string resultFilePath = @"..\..\..\result_"+ currentTime + ".txt";
            string str = "";
            using (FileStream fileStream = File.OpenRead(rawFilePath))
            {
                byte[] raw = new byte[fileStream.Length];
                await fileStream.ReadAsync(raw.AsMemory(0, raw.Length));
                str = Encoding.GetEncoding(1251).GetString(raw);
            }
            using (FileStream fileStream = File.OpenWrite(sourceFilePath))
            {
                byte[] source = Encoding.GetEncoding(1251).GetBytes(str);
                await fileStream.WriteAsync(source.AsMemory(0, source.Length));
            }
            str = str.Replace(",\r\n", "\r\n");
            str = "\'" + str.Replace("\r\n", "\',\n\'");
            str = str.Remove(str.Length - 2);
            using (FileStream fileStream = File.OpenWrite(resultFilePath))
            {
                byte[] result = Encoding.GetEncoding(1251).GetBytes(str);
                await fileStream.WriteAsync(result.AsMemory(0, result.Length));
            }
            Console.WriteLine("DONE!");
        }
    }
}
