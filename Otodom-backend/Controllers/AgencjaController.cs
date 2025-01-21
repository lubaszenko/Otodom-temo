using Microsoft.AspNetCore.Mvc;
using Otodom.DTO;
using Otodom.Services;

namespace Otodom.Controllers
{
    [ApiController]
    [Route("agencjas")]
    public class AgencjaController : ControllerBase
    {
        private readonly IAgencjaService _agencjaService;
        public AgencjaController(IAgencjaService agencjaService)
        {
            _agencjaService = agencjaService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAgencjas()
        {
            try
            {
                var agencjas = await _agencjaService.GetAgencjas();
                return Ok(agencjas);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgencja(int id)
        {
            try
            {
                var agencjas = await _agencjaService.GetAgencja(id);
                return Ok(agencjas);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("bigger/{id}")]
        public async Task<IActionResult> GetAgencjasBiggerThan(int id)
        {
            try
            {
                var agencjas = await _agencjaService.GetAgencjasBiggerThan(id);
                return Ok(agencjas);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostAgencja(AgencjaRequest agencjaToAdd)
        {
            try
            {
                var agencjas = await _agencjaService.PostAgencja(agencjaToAdd);
                return Ok(agencjas);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}