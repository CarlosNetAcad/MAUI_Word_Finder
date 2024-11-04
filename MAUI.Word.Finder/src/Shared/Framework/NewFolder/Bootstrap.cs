using MAUI.Word.Finder.src.WordFinder.Domain.Services;
using MAUI.Word.Finder.src.WordFinder.Presentation.ViewModels;
using MAUI.Word.Finder.src.WordFinder.Presentation.Views;

namespace MAUI.Word.Finder.src.Shared.Framework.NewFolder;

public static class Bootstrap
{
    public static void Startup(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IWordFinderService, WordFinderService>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();
    }
}
