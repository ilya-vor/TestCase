using TestCase.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace TestCase.Views.Pages
{
    public partial class Page21 : INavigableView<ViewModel21>
    {
        public ViewModel21 ViewModel { get; }

        public Page21(ViewModel21 viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
