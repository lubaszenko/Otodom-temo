using Microsoft.AspNetCore.Mvc;
using Otodom.DTO;
using Otodom.Services;

namespace Otodom.Controllers
{
    [ApiController]
    [Route("klients")]
    public class KlientController : ControllerBase
    {
        private readonly IKlientService _klientService;
        public KlientController(IKlientService klientService)
        {
            _klientService = klientService;
        }
        [HttpGet]
        public async Task<IActionResult> GetKlient()
        {
            try
            {
                var klients = await _klientService.GetKlient();
                return Ok(klients);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostKlient(KlientRequest KlientToAdd)
        {
            try
            {
                var klients = await _klientService.PostKlient(KlientToAdd);
                return Ok(klients);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
