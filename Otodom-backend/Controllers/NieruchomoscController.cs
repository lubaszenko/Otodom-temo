using Microsoft.AspNetCore.Mvc;
using Otodom.DTO;
using Otodom.Services;

namespace Otodom.Controllers
{
    [ApiController]
    [Route("nieruchomoscs")]
    public class NieruchomoscController : ControllerBase
    {
        private readonly INieruchomoscService _nieruchomoscService;
        public NieruchomoscController(INieruchomoscService nieruchomoscService)
        {
            _nieruchomoscService = nieruchomoscService;
        }
        [HttpGet]
        public async Task<IActionResult> GetNieruchomoscsWithPhotos()
        {
            try
            {
                var nieruchomoscs = await _nieruchomoscService.GetNieruchomoscsWithPhotos();
                return Ok(nieruchomoscs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNieruchomoscs(int id)
        {
            try
            {
                var mieszkania = await _nieruchomoscService.GetNieruchomoscs(id);
                return Ok(mieszkania);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /*[HttpGet("{miasto}")]
        public async Task<IActionResult> GetNieruchomoscs(string miasto)
        {
            try
            {
                var nieruchomoscs = await _nieruchomoscService.GetNieruchomoscs(miasto);
                return Ok(nieruchomoscs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }*/
        [HttpPost]
        public async Task<IActionResult> PostNieruchomosc(NieruchomoscRequest NieruchomoscToAdd)
        {
            try
            {
                var nieruchomoscs = await _nieruchomoscService.PostNieruchomosc(NieruchomoscToAdd);
                return Ok(nieruchomoscs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNieruchomoscs(int id)
        {
            try
            {
                var deletedNieruchomoscs = await _nieruchomoscService.DeleteNieruchomoscs(id);
                return Ok(deletedNieruchomoscs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
