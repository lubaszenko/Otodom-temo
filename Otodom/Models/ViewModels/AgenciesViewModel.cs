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
        private readonly ApiClientService _clientService;

        public ICommand LoadAgencyDataCommand { get; }

        public ICommand NavigateBackCommand { get; }

        [ObservableProperty]
        private ObservableCollection<AgencjaResponse> _agencjas = new();

        public AgenciesViewModel(ApiClientService clientService)
        {
            _clientService = clientService;
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackCommandAsync);
            LoadAgencyDataCommand = new AsyncRelayCommand(LoadAgencyDataAsync);
            LoadAgencyDataCommand.Execute(null);
        }

        private async Task LoadAgencyDataAsync()
        {
            try
            {
                Console.WriteLine("Endpoint LoadAgencyData works.");
                List<AgencjaResponse> agencjas = await _clientService.GetAgencjas();
                foreach(var Agencja in agencjas)
                {
                    if(!_agencjas.Any(a => a.IdAgencji == Agencja.IdAgencji))
                    {
                        _agencjas.Add(Agencja);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Endpoint LoadAgencyData doesn't work.");
            }
        }

        private async Task NavigateBackCommandAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
