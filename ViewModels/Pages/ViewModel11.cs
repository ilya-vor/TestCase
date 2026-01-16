using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Diagnostics;
using TestCase.Services.File;
using TestCase.Services.Xml;

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
            var FileService = App.Services.GetRequiredService<IFileService>();
            FileService.OpenFile(filePath);
        }
    }
}
