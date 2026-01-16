using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using TestCase.Models;
using TestCase.Services.File;
using TestCase.Services.Xml;

namespace TestCase.ViewModels.Pages
{
    public partial class ViewModel22 : ObservableObject
    {
        private readonly string data1Path = "Resources\\Data\\Source\\Data1.xml";
        private readonly string data2Path = "Resources\\Data\\Source\\Data2.xml";
        private readonly string xsltPath = "Resources\\Transforms\\TransformToEmployees.xslt";
        private readonly string employeesPath = "Resources\\Data\\Generated\\Employees.xml";

        [ObservableProperty]
        private int _counter = 0;

        [ObservableProperty]
        private Employees _employeesData;

        [RelayCommand]
        public void OnButtonClicked()
        {
            var employeesDataService = App.Services.GetRequiredService<IEmployeeDataService>();
            EmployeesData = employeesDataService.LoadEmployeesData(data1Path, data2Path, xsltPath, employeesPath);
            SaveEmployeesWithTotal(employeesPath, EmployeesData.EmployeeList);
        }

        [RelayCommand]
        public void OnButtonClicked1()
        {
            string filePath = "Resources\\Data\\Generated\\Employees_with_total.xml";
            var FileService = App.Services.GetRequiredService<IFileService>();
            FileService.OpenFile(filePath);
        }
        public void SaveEmployeesWithTotal(string sourceFilePath, ObservableCollection<Employee> employees)
        {
            string newFilePath = Path.Combine(
                Path.GetDirectoryName(sourceFilePath),
                $"{Path.GetFileNameWithoutExtension(sourceFilePath)}_with_total{Path.GetExtension(sourceFilePath)}"
            );

            var employeesForSerialization = employees.Select(e => new EmployeeWithTotalSalary
            {
                Name = e.Name,
                Surname = e.Surname,
                TotalSalary = e.TotalSalary,
                Salaries = e.Salaries
            }).ToList();

            var root = new
            {
                Employee = employeesForSerialization
            };

            var serializer = new XmlSerializer(typeof(List<EmployeeWithTotalSalary>), new XmlRootAttribute("Employees"));
            using (var writer = new StreamWriter(newFilePath))
            {
                serializer.Serialize(writer, employeesForSerialization);
            }
        }
    }
}
