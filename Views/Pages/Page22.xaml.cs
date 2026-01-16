using TestCase.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace TestCase.Views.Pages
{
    public partial class Page22 : INavigableView<ViewModel22>
    {
        public ViewModel22 ViewModel { get; }

        public Page22(ViewModel22 viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
