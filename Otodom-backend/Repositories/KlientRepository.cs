using Microsoft.EntityFrameworkCore;
using Otodom.Models;
using Otodom.DTO;

namespace Otodom.Repositories
{
    public interface IKlientRepository
    {
        public Task<List<KlientResponse>> GetKlient();
        public Task<Klient> PostKlient(KlientRequest KlientToAdd);
    }
    public class KlientRepository : IKlientRepository
    {
        private readonly OtodomContext _context;
        public KlientRepository(OtodomContext context)
        {
            _context = context;
        }
        public async Task<List<KlientResponse>> GetKlient()
        {
            return await _context.Klients.Include(b => b.AgencjaIdAgencjiNavigation)
                .Select (b => new KlientResponse
                {
                    IdKlienta = b.IdKlienta,
                    Imie = b.Imie,
                    Nazwisko = b.Nazwisko,
                    Email = b.Email,
                    NrTelefonuKlienta = b.NrTelefonuKlienta,
                    NazwaAgencji = b.AgencjaIdAgencjiNavigation.NazwaAgencji
                })
                .ToListAsync();
        }

        public async Task<Klient> PostKlient(KlientRequest KlientToAdd)
        {
            var klient = new Klient
            {
                Imie = KlientToAdd.Imie,
                Nazwisko = KlientToAdd.Nazwisko,
                Email = KlientToAdd.Email,
                NrTelefonuKlienta = KlientToAdd.NrTelefonuKlienta,
                AgencjaIdAgencji = KlientToAdd.AgencjaIdAgencji,
            };
            await _context.Klients.AddAsync(klient);
            await _context.SaveChangesAsync();
            return klient;
        }
    }
}
