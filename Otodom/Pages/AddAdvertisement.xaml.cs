using Otodom.Models.ViewModels;

namespace Otodom.Pages;

public partial class AddAdvertisement : ContentPage
{
	public AddAdvertisement(AddAdvertisementViewModel addAdvertisementViewModel)
	{
		InitializeComponent();
		BindingContext = addAdvertisementViewModel;
	}
}