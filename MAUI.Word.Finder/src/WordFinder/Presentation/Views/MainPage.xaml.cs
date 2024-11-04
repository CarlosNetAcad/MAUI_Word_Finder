using MAUI.Word.Finder.src.WordFinder.Presentation.ViewModels;
using System.Diagnostics;

namespace MAUI.Word.Finder.src.WordFinder.Presentation.Views
{
    public partial class MainPage : ContentPage
    {
       
        private MainViewModel _viewModel;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        private void OnFindWordClicked(object sender, EventArgs e)
        {
            _viewModel.FindWordsCommand.Execute(null);
        }
    }

}
