using Microsoft.AspNetCore.Mvc;
using Otodom.DTO;
using Otodom.Services;

namespace Otodom.Controllers
{
    [ApiController]
    [Route("ogloszenies")]
    public class OgloszenieController : ControllerBase
    {
        private readonly IOgloszenieService _ogloszenieService;
        public OgloszenieController(IOgloszenieService ogloszenieService)
        {
            _ogloszenieService = ogloszenieService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOgloszenie()
        {
            try
            {
                var ogloszenies = await _ogloszenieService.GetOgloszenie();
                return Ok(ogloszenies);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOgloszenie(int id)
        {
            try
            {
                var ogloszenies = await _ogloszenieService.GetOgloszenie(id);
                return Ok(ogloszenies);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOgloszenies(int id)
        {
            try
            {
                var deletedOgloszenies = await _ogloszenieService.DeleteOgloszenies(id);
                return Ok(deletedOgloszenies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostOgloszenie(OgloszenieRequest OgloszenieToAdd)
        {
            try
            {
                var ogloszenies = await _ogloszenieService.PostOgloszenie(OgloszenieToAdd);
                return Ok(ogloszenies);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("nieruchomosc")]
        public async Task<IActionResult> PostOgloszenie(Ogloszenieznieruchomoscia OgloszenieToAdd)
        {
            try
            {
                var ogloszenies = await _ogloszenieService.PostOgloszenieznieruchomoscia(OgloszenieToAdd);
                return Ok(ogloszenies);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
