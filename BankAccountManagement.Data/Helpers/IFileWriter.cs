using System;
namespace BankAccountManagement.Data.Helpers
{
	public interface IFileWriter
	{
		Task WriteToFile(string filePath, string content);
	}

    public class FileWriter : IFileWriter
    {
        public async Task WriteToFile(string filePath, string content)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteAsync(content);
                }
            }
        }
    }
}

