using Otodom.Models.ViewModels;
namespace Otodom.Pages;

public partial class Agencies : ContentPage
{
    public Agencies(AgenciesViewModel agenciesViewModel)
    {
        InitializeComponent();
        BindingContext = agenciesViewModel;
    }
}