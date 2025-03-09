using System;
using System.IO;

namespace BankAccountManagement.Data.Helpers;

public interface IFileReader
{
    Task<string> ReadSerializedData(string filePath);

}

public class FileReader : IFileReader
{
    public async Task<string> ReadSerializedData(string filePath)
    {
        try
        {

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string content = await sr.ReadToEndAsync();
                    return content;
                }
            }
        }
        catch(FileNotFoundException ex)
        {
            return string.Empty;
        }
    }
}

