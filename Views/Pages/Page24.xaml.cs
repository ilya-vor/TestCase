using TestCase.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace TestCase.Views.Pages
{
    public partial class Page24 : INavigableView<ViewModel24>
    {
        public ViewModel24 ViewModel { get; }

        public Page24(ViewModel24 viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
