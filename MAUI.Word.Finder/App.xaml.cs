using MAUI.Word.Finder.src.WordFinder.Presentation.Views;

namespace MAUI.Word.Finder
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
