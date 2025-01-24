using Otodom.Models.ViewModels;
namespace Otodom.Pages;

public partial class Advertisement : ContentPage
{
    public Advertisement(DisplayAdvertisementViewModel advertisementViewModel)
    {
        InitializeComponent();
        BindingContext = advertisementViewModel;
    }

}