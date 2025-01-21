using Microsoft.EntityFrameworkCore;
using Otodom.Models;
using Otodom.DTO;

namespace Otodom.Repositories
{
    public interface IOgloszenieRepository
    {
        public Task<List<OgloszenieResponse>> GetOgloszenie();
        public Task<OgloszenieResponse> GetOgloszenie(int id);
        public Task<Ogloszenie> GetOgloszenieModel(int id);
        public Task<Ogloszenie> DeleteOgloszenies(Ogloszenie OgloszenieToDelete);
        public Task<Ogloszenie> PostOgloszenie(OgloszenieRequest OgloszenieToAdd);
    }

    public class OgloszenieRepository : IOgloszenieRepository
    {
        private readonly OtodomContext _context;

        public OgloszenieRepository(OtodomContext context)
        {
            _context = context;
        }
        public async Task<List<OgloszenieResponse>> GetOgloszenie()
        {// grupowanie po ogloszeniu
            return await _context.Ogloszenies
                .Include(n => n.NieruchomoscIdNieruchomosciNavigation)
                .Include(n => n.NieruchomoscIdNieruchomosciNavigation.Zdjecies)
                .Include(m => m.KlientIdKlientaNavigation)
                .GroupBy(n => n.IdOgloszenia)
                .Select(group => new OgloszenieResponse
                {
                    Id = group.First().IdOgloszenia,
                    Tytul = group.First().Tytul,
                    DataDodania = group.First().DataDodania,
                    Status = group.First().Status,
                    Opis = group.First().Opis,
                    Cena = group.First().Cena,
                    KlientIdKlienta = group.First().KlientIdKlienta,
                    ImieKlienta = group.First().KlientIdKlientaNavigation.Imie,
                    NazwiskoKlienta = group.First().KlientIdKlientaNavigation.Nazwisko,
                    Email = group.First().KlientIdKlientaNavigation.Email,
                    Telefon = group.First().KlientIdKlientaNavigation.NrTelefonuKlienta,
                    Nieruchomosc = new NieruchomoscResponse
                    {
                        Id = group.First().NieruchomoscIdNieruchomosciNavigation.IdNieruchomosci,
                        Wojewodztwo = group.First().NieruchomoscIdNieruchomosciNavigation.Wojewodztwo,
                        Miasto = group.First().NieruchomoscIdNieruchomosciNavigation.Miasto,
                        KodPocztowy = group.First().NieruchomoscIdNieruchomosciNavigation.KodPocztowy,
                        Ulica = group.First().NieruchomoscIdNieruchomosciNavigation.Ulica,
                        NrDomu = group.First().NieruchomoscIdNieruchomosciNavigation.NrDomu,
                        PowierzchniaDomu = group.First().NieruchomoscIdNieruchomosciNavigation.PowierzchniaDomu,
                        LiczbaPieter = group.First().NieruchomoscIdNieruchomosciNavigation.LiczbaPieter,
                        RokBudowy = group.First().NieruchomoscIdNieruchomosciNavigation.RokBudowy,
                        StanWykonczenia = group.First().NieruchomoscIdNieruchomosciNavigation.StanWykonczenia,
                        RodzajOkna = group.First().NieruchomoscIdNieruchomosciNavigation.RodzajOkna,
                        TypOgrzewania = group.First().NieruchomoscIdNieruchomosciNavigation.TypOgrzewania,
                        RodzajZabudowy = group.First().NieruchomoscIdNieruchomosciNavigation.RodzajZabudowy,
                        Zdjecia = group.First().NieruchomoscIdNieruchomosciNavigation.Zdjecies
                    .Select(z => new ZdjecieResponse
                    {
                        Zdjecie = z.ZdjecieData
                    }).ToList()
                    }
                }).ToListAsync();
        }
        public async Task<Ogloszenie> DeleteOgloszenies(Ogloszenie OgloszenieToDelete)
        {
            _context.Ogloszenies.Remove(OgloszenieToDelete);
            await _context.SaveChangesAsync();
            return OgloszenieToDelete;
        }

        public async Task<Ogloszenie> PostOgloszenie(OgloszenieRequest OgloszenieToAdd)
        {
            var ogloszenie = new Ogloszenie
            {
                Tytul = OgloszenieToAdd.Tytul,
                DataDodania = OgloszenieToAdd.DataDodania,
                Status = OgloszenieToAdd.Status,
                Opis = OgloszenieToAdd.Opis,
                Cena = OgloszenieToAdd.Cena,
                KlientIdKlienta = OgloszenieToAdd.KlientIdKlienta,
                NieruchomoscIdNieruchomosci = OgloszenieToAdd.NieruchomoscIdNieruchomosci,
            };
            await _context.Ogloszenies.AddAsync(ogloszenie);
            await _context.SaveChangesAsync();

            return ogloszenie;
        }

        public async Task<OgloszenieResponse> GetOgloszenie(int id)
        {// grupowanie po ogloszeniu
            return await _context.Ogloszenies
                .Include(n => n.NieruchomoscIdNieruchomosciNavigation)
                .Include(n => n.NieruchomoscIdNieruchomosciNavigation.Zdjecies)
                .Include(m => m.KlientIdKlientaNavigation)
                .Where(n => n.IdOgloszenia == id)
                .Select(n => new OgloszenieResponse
                {
                    Id = n.IdOgloszenia,
                    Tytul = n.Tytul,
                    DataDodania = n.DataDodania,
                    Status = n.Status,
                    Opis = n.Opis,
                    Cena = n.Cena,
                    KlientIdKlienta = n.KlientIdKlienta,
                    ImieKlienta = n.KlientIdKlientaNavigation.Imie,
                    NazwiskoKlienta = n.KlientIdKlientaNavigation.Nazwisko,
                    Email = n.KlientIdKlientaNavigation.Email,
                    Telefon = n.KlientIdKlientaNavigation.NrTelefonuKlienta,
                    Nieruchomosc = new NieruchomoscResponse
                    {
                        Id = n.NieruchomoscIdNieruchomosciNavigation.IdNieruchomosci,
                        Wojewodztwo = n.NieruchomoscIdNieruchomosciNavigation.Wojewodztwo,
                        Miasto = n.NieruchomoscIdNieruchomosciNavigation.Miasto,
                        KodPocztowy = n.NieruchomoscIdNieruchomosciNavigation.KodPocztowy,
                        Ulica = n.NieruchomoscIdNieruchomosciNavigation.Ulica,
                        NrDomu = n.NieruchomoscIdNieruchomosciNavigation.NrDomu,
                        PowierzchniaDomu = n.NieruchomoscIdNieruchomosciNavigation.PowierzchniaDomu,
                        LiczbaPieter = n.NieruchomoscIdNieruchomosciNavigation.LiczbaPieter,
                        RokBudowy = n.NieruchomoscIdNieruchomosciNavigation.RokBudowy,
                        StanWykonczenia = n.NieruchomoscIdNieruchomosciNavigation.StanWykonczenia,
                        RodzajOkna = n.NieruchomoscIdNieruchomosciNavigation.RodzajOkna,
                        TypOgrzewania = n.NieruchomoscIdNieruchomosciNavigation.TypOgrzewania,
                        RodzajZabudowy = n.NieruchomoscIdNieruchomosciNavigation.RodzajZabudowy,
                        Zdjecia = n.NieruchomoscIdNieruchomosciNavigation.Zdjecies
                    .Select(z => new ZdjecieResponse
                    {
                        Zdjecie = z.ZdjecieData
                    }).ToList()
                    }
                }).FirstOrDefaultAsync();
        }

        public async Task<Ogloszenie> GetOgloszenieModel(int id)
        {
            return await _context.Ogloszenies.Where(o => o.IdOgloszenia == id).FirstOrDefaultAsync();
        }
    }
}
