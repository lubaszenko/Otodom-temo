using Microsoft.EntityFrameworkCore;
using Otodom.Models;
using Otodom.DTO;

namespace Otodom.Repositories
{
    public interface IAgencjaRepository
    {
        public Task<List<Agencja>> GetAgencjas();
        public Task<Agencja> GetAgencja(int id);
        public Task<List<Agencja>> GetAgencjasBiggerThan(int id);
        public Task<Agencja> PostAgencja(AgencjaRequest AgencjaToAdd);
    }
    public class AgencjaRepository : IAgencjaRepository
    {
        private readonly OtodomContext _context;

        public AgencjaRepository(OtodomContext context)
        {
            _context = context;
        }

        public async Task<Agencja> GetAgencja(int id)
        {
            return await _context.Agencjas.Where(a => a.IdAgencji == id).FirstOrDefaultAsync();
        }

        public async Task<List<Agencja>> GetAgencjas()
        {
            return await _context.Agencjas.ToListAsync();
        }

        public async Task<List<Agencja>> GetAgencjasBiggerThan(int id)
        {
            return await _context.Agencjas.Where(a => a.IdAgencji > id).ToListAsync();
        }

        public async Task<Agencja> PostAgencja(AgencjaRequest AgencjaToAdd)
        {
            var agencja = new Agencja
            {
                Email = AgencjaToAdd.Email,
                NazwaAgencji = AgencjaToAdd.NazwaAgencji,
                Nip = AgencjaToAdd.Nip,
                NrTelefonuAgencji = AgencjaToAdd.NrTelefonuAgencji
            };
            await _context.Agencjas.AddAsync(agencja);
            await _context.SaveChangesAsync();
            return agencja;
        }
    }
}