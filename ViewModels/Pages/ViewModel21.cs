using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using TestCase.Models;
using TestCase.Services.Xml;
using TestCase.Views.Windows;

namespace TestCase.ViewModels.Pages
{
    public partial class ViewModel21 : ObservableObject
    {
        private readonly string data1Path = "Resources\\Data\\Source\\Data1.xml";
        private readonly string data2Path = "Resources\\Data\\Source\\Data2.xml";
        private readonly string xsltPath = "Resources\\Transforms\\TransformToEmployees.xslt";
        private readonly string employeesPath = "Resources\\Data\\Generated\\Employees.xml";

        [ObservableProperty]
        private Employees _employeesData;

        [RelayCommand]
        public void OnRunXsltTransformation()
        {
            try
            {
                var employeesDataService = App.Services.GetRequiredService<IEmployeeDataService>();
                EmployeesData = employeesDataService.LoadEmployeesData(
                    data1Path, data2Path, xsltPath, employeesPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
