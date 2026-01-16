using TestCase.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace TestCase.Views.Pages
{
    public partial class Page11 : INavigableView<ViewModel11>
    {
        public ViewModel11 ViewModel { get; }

        public Page11(ViewModel11 viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
