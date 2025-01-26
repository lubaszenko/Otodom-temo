using Microsoft.Maui.Controls;
using System.Windows.Input;
using Otodom.Models.ViewModels;

namespace Otodom.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
