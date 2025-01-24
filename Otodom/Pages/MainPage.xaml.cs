using Microsoft.Maui.Controls;
using System.Windows.Input;
using Otodom.Models.ViewModels;

namespace Otodom.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}
