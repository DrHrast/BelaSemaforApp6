using CommunityToolkit.Maui.Views;
using BelaSemaforApp6.ViewModels;

namespace BelaSemaforApp6.Views;

public partial class HelpPopup : Popup
{
	public HelpPopup()
	{
        InitializeComponent();
        BindingContext = new HelpPopupViewModel(this);
    }
}