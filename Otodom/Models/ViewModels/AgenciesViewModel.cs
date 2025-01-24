using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ApiConsumer;
using System.Collections.ObjectModel;
using ApiConsumer.DTO;

namespace Otodom.Models.ViewModels
{
    public partial class AgenciesViewModel : ObservableObject
    {
        public ICommand NavigateBackCommand { get; }

        public AgenciesViewModel()
        {
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackCommandAsync);
        }

        private async Task NavigateBackCommandAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
