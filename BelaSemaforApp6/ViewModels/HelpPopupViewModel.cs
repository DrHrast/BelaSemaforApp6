using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;

namespace BelaSemaforApp6.ViewModels;

public partial class HelpPopupViewModel : ObservableObject
{
    private readonly Popup _popup;

    public HelpPopupViewModel(Popup popup)
    {
        _popup = popup;
    }

    [RelayCommand]
    private void Close()
    {
        _popup.Close();
    }
}
