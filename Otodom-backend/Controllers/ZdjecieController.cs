using Microsoft.AspNetCore.Mvc;
using Otodom.DTO;
using Otodom.Services;

namespace Otodom.Controllers
{
    [ApiController]
    [Route("zdjecies")]
    public class ZdjecieController : ControllerBase
    {
        private readonly IZdjecieService _zdjecieService;
        public ZdjecieController(IZdjecieService zdjecieService)
        {
            _zdjecieService = zdjecieService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPhoto()
        {
            try
            {
                var zdjecies = await _zdjecieService.GetPhoto();
                return Ok(zdjecies);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
