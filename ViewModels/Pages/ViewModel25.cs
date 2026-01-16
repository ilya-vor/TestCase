using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using TestCase.Models;
using TestCase.Services.Xml;
using TestCase.Services.Xml.TestCase.Services.Xml;
using TestCase.Views.Windows;

namespace TestCase.ViewModels.Pages
{
    public partial class ViewModel25 : ObservableObject
    {
        private readonly string data1Path = "Resources\\Data\\Source\\Data1.xml";
        private readonly string data2Path = "Resources\\Data\\Source\\Data2.xml";
        private readonly string xsltPath = "Resources\\Transforms\\TransformToEmployees.xslt";
        private readonly string employeesPath = "Resources\\Data\\Generated\\Employees.xml";

        [ObservableProperty]
        private Employees _employeesData;

        [ObservableProperty]
        private ObservableCollection<MonthlySalarySummary> _monthlySalarySummaries;

        [ObservableProperty]
        public PayItem _payItem = new PayItem();

        [RelayCommand] 
        public void OnRunXsltTransformation()
        {
            try
            {
                var employeesDataService = App.Services.GetRequiredService<IEmployeeDataService>();
                EmployeesData = employeesDataService.LoadEmployeesData(
                    data1Path, data2Path, xsltPath, employeesPath);
                MonthlySalarySummaries = GetMonthlySummaries(EmployeesData.EmployeeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand] 
        public void OnButtonClicked()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(PayItem.Name) ||
                string.IsNullOrWhiteSpace(PayItem.Surname) ||
                string.IsNullOrWhiteSpace(PayItem.Amount) ||
                string.IsNullOrWhiteSpace(PayItem.Month))
                {
                    var mb = new Wpf.Ui.Controls.MessageBox
                    {
                        Title = "Заполните все поля",
                        CloseButtonText = "OK",
                    };
                    mb.ShowDialogAsync();
                    return;
                }

                if (!double.TryParse(PayItem.Amount, out _))
                {
                    var mb = new Wpf.Ui.Controls.MessageBox
                    {
                        Title = "Введите корректную сумму",
                        CloseButtonText = "OK",
                    };
                    mb.ShowDialogAsync();
                    return;
                }


                var xmlService = App.Services.GetRequiredService<IXmlService>();
                var payData = xmlService.LoadXmlFile<PayData>(data1Path);
                PayItem.Month = PayItem.Month.Split(' ').Last();
                payData.Items.Add(PayItem);
                SavePay(data1Path, payData);

                OnRunXsltTransformation();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SavePay(string filePath, PayData data)
        {
            var serializer = new XmlSerializer(typeof(PayData));

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }

        public ObservableCollection<MonthlySalarySummary> GetMonthlySummaries(ObservableCollection<Employee> employees)
        {
            var grouped = employees
                         .SelectMany(e => e.Salaries.Select(s => new
                         {
                             Salary = s,
                             EmployeeName = e.FullName
                         }))
                         .GroupBy(x => x.Salary.Month)
                         .Select(g => new MonthlySalarySummary
                         {
                             Month = g.Key,
                             TotalAmount = g.Sum(x => x.Salary.Amount),
                             AllPayments = string.Join("\n",
                                 g.Select(x => $"{x.EmployeeName}: {x.Salary.Amount:C}"))
                         })
                         .OrderBy(s => s.Month);

            return new ObservableCollection<MonthlySalarySummary>(grouped);
        }
    }
}
