using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BelaSemaforApp6.ViewModels;

namespace BelaSemaforApp6.Views;

public partial class GameView : ContentPage
{
    public GameView()
    {
        InitializeComponent();
        
        BindingContext = IPlatformApplication.Current!.Services.GetService<GameViewModel>();
    }
}