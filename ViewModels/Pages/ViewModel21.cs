using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace TestCase.ViewModels.Pages
{
    public partial class ViewModel21 : ObservableObject
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
        public void OnRunXsltTransformation()
        {
            Counter++;
            RunXsltTransformation();
            LoadXmlFile("Resources\\Data\\Generated\\Employees.xml");
        }

        private void LoadXmlFile(string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Employees));

                using (var reader = new System.IO.StreamReader(filePath))
                {
                    EmployeesData = (Employees)serializer.Deserialize(reader);

                    MessageBox.Show($"Загружено {_employeesData?.EmployeeList?.Count ?? 0} сотрудников",
                                  "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке XML: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RunXsltTransformation()
        {
            string sourceFile = File.Exists(data1Path) ? data1Path : data2Path;

            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException("Не найден файл Data1.xml или Data2.xml");
            }

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            string directory = Path.GetDirectoryName(employeesPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (XmlWriter writer = XmlWriter.Create(employeesPath))
            {
                xslt.Transform(sourceFile, writer);
            }
        }
    }
    public class Salary
    {
        [XmlAttribute("amount")]
        public string AmountString { get; set; }

        [XmlAttribute("mount")]
        public string Month { get; set; }

        // Свойство для получения суммы как числа
        public decimal Amount
        {
            get
            {
                // Заменяем запятую на точку для корректного парсинга
                var normalized = AmountString.Replace(',', '.');
                return decimal.Parse(normalized, System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }

    public class Employee
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("surname")]
        public string Surname { get; set; }

        [XmlElement("salary")]
        public ObservableCollection<Salary> Salaries { get; set; } = new ObservableCollection<Salary>();

        // Свойство для отображения полного имени
        public string FullName => $"{Name} {Surname}";

        // Свойство для получения общей суммы зарплат
        public decimal TotalSalary => Salaries.Sum(s => s.Amount);
    }

    [XmlRoot("Employees")]
    public class Employees
    {
        [XmlElement("Employee")]
        public ObservableCollection<Employee> EmployeeList { get; set; } = new ObservableCollection<Employee>();
    }
}
