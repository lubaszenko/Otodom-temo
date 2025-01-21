using Microsoft.EntityFrameworkCore;
using Otodom.Models;
using Otodom.DTO;

namespace Otodom.Repositories
{
    public interface INieruchomoscRepository
    {
        public Task<List<NieruchomoscResponse>> GetNieruchomoscsWithPhotos();
        public Task<NieruchomoscResponse> GetNieruchomoscs(int id);
        public Task<Nieruchomosc> GetNieruchomosc(int id);
        public Task<Nieruchomosc> PostNieruchomosc(NieruchomoscRequest NieruchomoscToAdd);
        public Task<Nieruchomosc> DeleteNieruchomoscs(Nieruchomosc NieruchomoscToDelete);
    }
    public class NieruchomoscRepository : INieruchomoscRepository
    {
        private readonly OtodomContext _context;

        public NieruchomoscRepository(OtodomContext context)
        {
            _context = context;
        }

        public async Task<NieruchomoscResponse> GetNieruchomoscs(int id)
        {
            return await _context.Nieruchomoscs
                .Include(b => b.Zdjecies)
                .Select(b => new NieruchomoscResponse
                {
                    Id = b.IdNieruchomosci,
                    Wojewodztwo = b.Wojewodztwo,
                    Miasto = b.Miasto,
                    KodPocztowy = b.KodPocztowy,
                    Ulica = b.Ulica,
                    NrDomu = b.NrDomu,
                    PowierzchniaDomu = b.PowierzchniaDomu,
                    LiczbaPieter = b.LiczbaPieter,
                    RokBudowy = b.RokBudowy,
                    StanWykonczenia = b.StanWykonczenia,
                    RodzajOkna = b.RodzajOkna,
                    TypOgrzewania = b.TypOgrzewania,
                    RodzajZabudowy = b.RodzajZabudowy,
                    Zdjecia = b.Zdjecies.Select(z => new ZdjecieResponse
                    {
                        Zdjecie = z.ZdjecieData
                    }).ToList()
                })
                .Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<NieruchomoscResponse>> GetNieruchomoscsWithPhotos()
        {
            return await _context.Nieruchomoscs.Include(n => n.Zdjecies)
                .Select(b => new NieruchomoscResponse
            {
                Id = b.IdNieruchomosci,
                Wojewodztwo = b.Wojewodztwo,
                Miasto = b.Miasto,
                KodPocztowy = b.KodPocztowy,
                Ulica = b.Ulica,
                NrDomu = b.NrDomu,
                PowierzchniaDomu = b.PowierzchniaDomu,
                LiczbaPieter = b.LiczbaPieter,
                RokBudowy = b.RokBudowy,
                StanWykonczenia = b.StanWykonczenia,
                RodzajOkna = b.RodzajOkna,
                TypOgrzewania = b.TypOgrzewania,
                RodzajZabudowy = b.RodzajZabudowy,
                Zdjecia = b.Zdjecies.Select(z => new ZdjecieResponse
                {
                    Zdjecie = z.ZdjecieData
                }).ToList()
            }).ToListAsync();
        }

        public async Task<Nieruchomosc> PostNieruchomosc(NieruchomoscRequest NieruchomoscToAdd)
        {
            var nieruchomosc = new Nieruchomosc
            {
                Wojewodztwo = NieruchomoscToAdd.Wojewodztwo,
                Miasto = NieruchomoscToAdd.Miasto,
                KodPocztowy = NieruchomoscToAdd.KodPocztowy,
                Ulica = NieruchomoscToAdd.Ulica,
                NrDomu = NieruchomoscToAdd.NrDomu,
                PowierzchniaDomu = NieruchomoscToAdd.PowierzchniaDomu,
                LiczbaPieter = NieruchomoscToAdd.LiczbaPieter,
                RokBudowy = NieruchomoscToAdd.RokBudowy,
                StanWykonczenia = NieruchomoscToAdd.StanWykonczenia,
                RodzajOkna = NieruchomoscToAdd.RodzajOkna,
                TypOgrzewania = NieruchomoscToAdd.TypOgrzewania,
                RodzajZabudowy = NieruchomoscToAdd.RodzajZabudowy,
            };
            await _context.Nieruchomoscs.AddAsync(nieruchomosc);
            await _context.SaveChangesAsync();
            return nieruchomosc;
        }

        public async Task<Nieruchomosc> DeleteNieruchomoscs(Nieruchomosc NieruchomoscToDelete)
        {
            _context.Nieruchomoscs.Remove(NieruchomoscToDelete);
            await _context.SaveChangesAsync();
            return NieruchomoscToDelete;
        }

        public async Task<Nieruchomosc> GetNieruchomosc(int id)
        {
            return await _context.Nieruchomoscs.Where(b => b.IdNieruchomosci == id).FirstOrDefaultAsync();
        }
    }
}