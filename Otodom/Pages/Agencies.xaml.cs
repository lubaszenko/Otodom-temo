using Otodom.Models.ViewModels;
namespace Otodom.Pages;

public partial class Agencies : ContentPage
{
	public Agencies()
	{
		InitializeComponent();
        BindingContext = new AgenciesViewModel();
    }
}