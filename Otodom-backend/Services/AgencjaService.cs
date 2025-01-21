using Otodom.DTO;
using Otodom.Models;
using Otodom.Repositories;

namespace Otodom.Services
{
    public interface IAgencjaService
    {
        public Task<List<Agencja>> GetAgencjas();
        public Task<Agencja> GetAgencja(int id);
        public Task<List<Agencja>> GetAgencjasBiggerThan(int id);
        public Task<Agencja> PostAgencja(AgencjaRequest AgencjaToAdd);
    }
    public class AgencjaService : IAgencjaService
    {
        private readonly IAgencjaRepository _agencjaRepository;

        public AgencjaService(IAgencjaRepository agencjaRepository)
        {
            _agencjaRepository = agencjaRepository;
        }

        public async Task<Agencja> GetAgencja(int id)
        {
            if (id <= 0)
                throw new Exception("Podałeś ujemne id.");
            var Agencje = await _agencjaRepository.GetAgencja(id);
            if (Agencje == null)
                throw new Exception(String.Format("Nie ma żadnej agencji o id {0}.", id));
            return Agencje;
        }

        public async Task<List<Agencja>> GetAgencjas()
        {
            var Agencje = await _agencjaRepository.GetAgencjas();
            if (!Agencje.Any())
                throw new Exception("Nie ma żadnej agencji.");
            return Agencje;
        }

        public async Task<List<Agencja>> GetAgencjasBiggerThan(int id)
        {
            if (id <= 0)
                throw new Exception("Podałeś ujemne id.");
            var Agencje = await _agencjaRepository.GetAgencjasBiggerThan(id);
            if (!Agencje.Any())
                throw new Exception(String.Format("Nie ma żadnej agencji o id większym niż {0}.", id));
            return Agencje;
        }

        public async Task<Agencja> PostAgencja(AgencjaRequest AgencjaToAdd)
        {
            if (!AgencjaToAdd.Email.Contains("@"))
                throw new Exception("Nie podałeś znaku @.");
            return await _agencjaRepository.PostAgencja(AgencjaToAdd);
        }
    }
}