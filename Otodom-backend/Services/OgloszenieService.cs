using Otodom.DTO;
using Otodom.Models;
using Otodom.Repositories;

namespace Otodom.Services
{
    public interface IOgloszenieService
    {
        public Task<List<OgloszenieResponse>> GetOgloszenie();
        public Task<OgloszenieResponse> GetOgloszenie(int id);
        public Task<Ogloszenie> GetOgloszenieModel(int id);
        public Task<Ogloszenie> DeleteOgloszenies(int id);
        public Task<OgloszenieResponse> PostOgloszenie(OgloszenieRequest OgloszenieToAdd);
        public Task<OgloszenieResponse> PostOgloszenieznieruchomoscia(Ogloszenieznieruchomoscia OgloszenieToAdd);
    }

    public class OgloszenieService : IOgloszenieService
    {
        private readonly IOgloszenieRepository _ogloszenieRepository;
        private readonly INieruchomoscService _nieruchomoscService;
        private readonly IZdjecieService _zdjecieService;

        public OgloszenieService(IOgloszenieRepository ogloszenieRepository, INieruchomoscService nieruchomoscservice, IZdjecieService zdjecieservice)
        {
            _ogloszenieRepository = ogloszenieRepository;
            _nieruchomoscService = nieruchomoscservice;
            _zdjecieService = zdjecieservice;
        }

        public async Task<List<OgloszenieResponse>> GetOgloszenie()
        {
            var Ogloszenia = await _ogloszenieRepository.GetOgloszenie();
            if (!Ogloszenia.Any())
                throw new Exception("Nie ma żadnego ogłoszenia.");
            return Ogloszenia;
        }
        public async Task<Ogloszenie> DeleteOgloszenies(int id)
        {
            if (id <= 0)
                throw new Exception("Podałeś ujemne id.");
            var OgloszenieToDelete = await GetOgloszenieModel(id);
            await _ogloszenieRepository.DeleteOgloszenies(OgloszenieToDelete);
            return OgloszenieToDelete;
        }

        public async Task<OgloszenieResponse> PostOgloszenie(OgloszenieRequest OgloszenieToAdd)
        {
            if (OgloszenieToAdd.Opis is null)
                throw new Exception("Wprowadź opis ogłoszenia.");
            await _nieruchomoscService.GetNieruchomoscs(OgloszenieToAdd.NieruchomoscIdNieruchomosci);
            var ogloszenie = await _ogloszenieRepository.PostOgloszenie(OgloszenieToAdd);
            return await GetOgloszenie(ogloszenie.IdOgloszenia);
        }

        public async Task<OgloszenieResponse> GetOgloszenie(int id)
        {
            var Ogloszenia = await _ogloszenieRepository.GetOgloszenie(id);
            if (Ogloszenia == null)
                throw new Exception(String.Format("Nie ma ogłoszenia o {0}.", id));
            return Ogloszenia;
        }

        public async Task<OgloszenieResponse> PostOgloszenieznieruchomoscia(Ogloszenieznieruchomoscia OgloszenieToAdd)
        {
            var Nieruchomosc = new NieruchomoscRequest
            {
                Wojewodztwo = OgloszenieToAdd.Wojewodztwo,
                Miasto = OgloszenieToAdd.Miasto,
                KodPocztowy = OgloszenieToAdd.KodPocztowy,
                Ulica = OgloszenieToAdd.Ulica,
                NrDomu = OgloszenieToAdd.NrDomu,
                PowierzchniaDomu = OgloszenieToAdd.PowierzchniaDomu,
                LiczbaPieter = OgloszenieToAdd.LiczbaPieter,
                RokBudowy = OgloszenieToAdd.RokBudowy,
                StanWykonczenia = OgloszenieToAdd.StanWykonczenia,
                RodzajOkna = OgloszenieToAdd.RodzajOkna,
                TypOgrzewania = OgloszenieToAdd.TypOgrzewania,
                RodzajZabudowy = OgloszenieToAdd.RodzajZabudowy
            };
            var DodanaNieruchomosc = await _nieruchomoscService.PostNieruchomosc(Nieruchomosc);
            foreach (var ZdjecieD in OgloszenieToAdd.Zdjecie)
            {
                var ZdjecieToAdd = new ZdjecieRequest
                {
                    Zdjecie = ZdjecieD,
                    IdNieruchomosci = DodanaNieruchomosc.IdNieruchomosci
                };
                await _zdjecieService.PostPhoto(ZdjecieToAdd);
            }
            var OgloszenieRequest = new OgloszenieRequest
            {
                Tytul = OgloszenieToAdd.Tytul,
                DataDodania = OgloszenieToAdd.DataDodania,
                Status = OgloszenieToAdd.Status,
                Opis = OgloszenieToAdd.Opis,
                Cena = OgloszenieToAdd.Cena,
                KlientIdKlienta = OgloszenieToAdd.KlientIdKlienta,
                NieruchomoscIdNieruchomosci = DodanaNieruchomosc.IdNieruchomosci
            };
            var DodaneOgloszenie = await PostOgloszenie(OgloszenieRequest);
            return DodaneOgloszenie;
        }

        public async Task<Ogloszenie> GetOgloszenieModel(int id)
        {
            var Ogloszenia = await _ogloszenieRepository.GetOgloszenieModel(id);
            if (Ogloszenia == null)
                throw new Exception(String.Format("Nie ma ogłoszenia o id {0}.", id));
            return Ogloszenia;
        }
    }
}
