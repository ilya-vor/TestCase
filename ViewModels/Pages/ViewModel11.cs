using System.ComponentModel;
using System.Diagnostics;

namespace TestCase.ViewModels.Pages
{
    public partial class ViewModel11 : ObservableObject
    {
        [ObservableProperty]
        private int _counter = 0;

        [RelayCommand]
        public void OnOpenFile()
        {
            string filePath = "Resources\\Transforms\\TransformToEmployees.xslt";

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
                MessageBox.Show($"Не найдена программа для открытия файла: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
