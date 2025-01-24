using Otodom.Models.ViewModels;
namespace Otodom.Pages;

public partial class Currencies : ContentPage
{
	public Currencies()
	{
		InitializeComponent();
        BindingContext = new CurrenciesViewModel();
    }
}