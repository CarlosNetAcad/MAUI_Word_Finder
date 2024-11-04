
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Word.Finder.src.Shared.Presentation.ViewModels;
using MAUI.Word.Finder.src.WordFinder.Domain.Services;
using MAUI.Word.Finder.src.WordFinder.Infrastructure.Repository.MockRepository;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MAUI.Word.Finder.src.WordFinder.Presentation.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private IWordFinderService _wordFinderService;

    [ObservableProperty]
    ObservableCollection<string> _wordsFound;

    public MainViewModel()
    {
        var matrix = new List<string>
        {
            "abcdc",
            "fgwio",
            "chill",
            "pqnsd",
            "uvdxy"
        };

        _wordFinderService = new WordFinderService(matrix);

        Title = "Word Finder";
    }

    [RelayCommand]
    async Task FindWords()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var wordStream = new List<string>
            {
                "cold",
                "chill",
                "snow",
                "wind"
            };

            var wordsFound = await _wordFinderService.FindAsync(wordStream);

            if (wordsFound.Any())
            {
                WordsFound?.Clear();

                foreach (var word in wordsFound)
                    WordsFound?.Add(word);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
