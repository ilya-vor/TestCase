using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace TestCase.Services.File
{
    public interface IFileService
    {
        void OpenFile(string filePath);
        void OpenFileWithDefaultProgram(string filePath);
        void OpenDirectory(string directoryPath);
        bool FileExists(string filePath);
    }

    public class FileService : IFileService
    {
        public void OpenFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл не найден: {filePath}");
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Win32Exception ex)
            {
                throw new InvalidOperationException(
                    $"Не найдена программа для открытия файла: {ex.Message}", ex);
            }
        }

        public void OpenFileWithDefaultProgram(string filePath)
        {
            OpenFile(filePath);
        }

        public void OpenDirectory(string directoryPath)
        {
            if (!System.IO.Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"Директория не найдена: {directoryPath}");
            }

            Process.Start("explorer.exe", directoryPath);
        }

        public bool FileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }
    }
}