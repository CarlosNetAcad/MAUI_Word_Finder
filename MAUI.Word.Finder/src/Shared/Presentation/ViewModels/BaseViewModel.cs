
using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUI.Word.Finder.src.Shared.Presentation.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    #region Flds

    /// <summary>
    /// State to set the changes of the properties.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    /// <summary>
    /// Title of the UI interface.
    /// </summary>
    [ObservableProperty]
    string title;

    #endregion Fdls

    #region Ctors
    #endregion

    #region Props

    /// <summary>
    /// State to set the oposite of isBusy.
    /// </summary>
    public bool IsNotBusy => !IsBusy;

    #endregion Props
}
