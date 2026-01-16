using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using TestCase.Models;
using TestCase.Services.File;
using TestCase.Services.Xml;
using TestCase.Services.Xml.TestCase.Services.Xml;

namespace TestCase.ViewModels.Pages
{
    public partial class ViewModel23 : ObservableObject
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
            string filePathPays = "Resources\\Data\\Source\\Data1.xml";
            string filePathPaysWithTotal = "Resources\\Data\\Source\\Data1_with_total.xml";

            var xmlService = App.Services.GetRequiredService<IXmlService>();
            var payData = xmlService.LoadXmlFile<PayData>(filePathPays);
            SavePaysWithTotal(filePathPaysWithTotal, payData);

            var FileService = App.Services.GetRequiredService<IFileService>();
            FileService.OpenFile(filePathPaysWithTotal);
        }

        public void SavePaysWithTotal(string filePath, PayData data)
        {
            data.CalculateTotal();

            var serializer = new XmlSerializer(typeof(PayData));

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}
