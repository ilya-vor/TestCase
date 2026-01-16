using System;
using System.IO;
using TestCase.Models;
using TestCase.Services.Xml.TestCase.Services.Xml;
using TestCase.ViewModels.Pages;

namespace TestCase.Services.Xml
{
    public interface IEmployeeDataService
    {
        Employees LoadEmployeesData(string source1Path, string source2Path,
                                    string xsltPath, string outputPath);
        Employees LoadEmployeesFromFile(string filePath);
    }

    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly IXmlService _xmlTransformService;

        public EmployeeDataService(IXmlService xmlTransformService)
        {
            _xmlTransformService = xmlTransformService;
        }

        public Employees LoadEmployeesData(string source1Path, string source2Path,
                                          string xsltPath, string outputPath)
        {
            string sourceFile = System.IO.File.Exists(source1Path) ? source1Path : source2Path;

            if (!System.IO.File.Exists(sourceFile))
            {
                throw new FileNotFoundException($"Не найден ни один из файлов: {source1Path} или {source2Path}");
            }

            _xmlTransformService.TransformXml(sourceFile, xsltPath, outputPath);

            return LoadEmployeesFromFile(outputPath);
        }

        public Employees LoadEmployeesFromFile(string filePath)
        {
            try
            {
                return _xmlTransformService.LoadXmlFile<Employees>(filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Ошибка при загрузке данных сотрудников: {ex.Message}", ex);
            }
        }
    }
}