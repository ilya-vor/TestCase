using TestCase.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace TestCase.Views.Pages
{
    public partial class Page25 : INavigableView<ViewModel25>
    {
        public ViewModel25 ViewModel { get; }

        public Page25(ViewModel25 viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
