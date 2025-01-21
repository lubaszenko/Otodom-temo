using Otodom.DTO;
using Otodom.Models;
using Otodom.Repositories;

namespace Otodom.Services
{
    public interface INieruchomoscService
    {
        public Task<NieruchomoscResponse> GetNieruchomoscs(int id);
        public Task<Nieruchomosc> GetNieruchomosc(int id);
        public Task<List<NieruchomoscResponse>> GetNieruchomoscsWithPhotos();

        /*public Task<Nieruchomosc> GetNieruchomoscs(string miasto);*/
        public Task<Nieruchomosc> PostNieruchomosc(NieruchomoscRequest NieruchomoscToAdd);
        public Task<Nieruchomosc> DeleteNieruchomoscs(int id);
    }
    public class NieruchomoscService : INieruchomoscService
    {
        private readonly INieruchomoscRepository _nieruchomoscRepository;

        public NieruchomoscService(INieruchomoscRepository nieruchomoscRepository)
        {
            _nieruchomoscRepository = nieruchomoscRepository;
        }

        public async Task<NieruchomoscResponse> GetNieruchomoscs(int id)
        {
            if (id <= 0)
                throw new Exception("Podałeś ujemne id.");
            var Nieruchomosc = await _nieruchomoscRepository.GetNieruchomoscs(id);
            if (Nieruchomosc == null)
                throw new Exception(String.Format("Nie ma żadnej nieruchomości o id {0}.", id));
            return Nieruchomosc;
        }

        public async Task<List<NieruchomoscResponse>> GetNieruchomoscsWithPhotos()
        {
            var Nieruchomosci = await _nieruchomoscRepository.GetNieruchomoscsWithPhotos();
            if (!Nieruchomosci.Any())
                throw new Exception("Nie ma żadnej nieruchomości.");
            return Nieruchomosci;
        }
        /*public async Task<Nieruchomosc> GetNieruchomoscs(string miasto)
        {
            var nieruchomosc = await _nieruchomoscRepository.GetNieruchomoscs(miasto);
            if (miasto == null)
                throw new Exception("Nie podano wartości dla miasta.");
            if (nieruchomosc == null)
                throw new Exception(String.Format("Nie znaleziono nieruchomości dla miasta o nazwie {0}.", miasto));
            return nieruchomosc;
        }*/

        public async Task<Nieruchomosc> PostNieruchomosc(NieruchomoscRequest NieruchomoscToAdd)
        {
            string kodPocztowyStr = NieruchomoscToAdd.KodPocztowy.ToString();

            // Remove any decimal point and digits after it, if present
            int dotIndex = kodPocztowyStr.IndexOf('.');
            if (dotIndex != -1)
            {
                kodPocztowyStr = kodPocztowyStr.Substring(0, dotIndex);
            }

            if (kodPocztowyStr.Length != 5)
                throw new Exception("Kod pocztowy nie składa się z 5 cyfr.");

            return await _nieruchomoscRepository.PostNieruchomosc(NieruchomoscToAdd);
        }

        public async Task<Nieruchomosc> DeleteNieruchomoscs(int id)
        {
            if (id <= 0)
                throw new Exception("Podałeś ujemne id.");
            var NieruchomoscToDelete = await GetNieruchomosc(id);
            await _nieruchomoscRepository.DeleteNieruchomoscs(NieruchomoscToDelete);
            return NieruchomoscToDelete;
        }

        public async Task<Nieruchomosc> GetNieruchomosc(int id)
        {
            var Nieruchomosc = await _nieruchomoscRepository.GetNieruchomosc(id);
            if (Nieruchomosc == null)
                throw new Exception(String.Format("Nie ma żadnej nieruchomości o id {0}.", id));
            return Nieruchomosc;
        }
    }
}
