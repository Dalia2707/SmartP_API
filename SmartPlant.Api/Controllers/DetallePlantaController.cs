using Microsoft.AspNetCore.Mvc;
using SmartPlant.Api.Models;
using SmartPlant.Api.Services;

namespace SmartPlant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallePlantaController : ControllerBase
    {
        private readonly ILogger<DetallePlantaController> _logger;
        private readonly DetallePlantaServices _detallePlantaServices;

        public DetallePlantaController(ILogger<DetallePlantaController> logger, DetallePlantaServices detallePlantaServices)
        {
            _logger = logger;
            _detallePlantaServices = detallePlantaServices;
        }

        //Mostrar 
        [HttpGet("Lista")]
        public async Task<IActionResult>GetDetalleplanta()
        {
            var detalleplanta = await _detallePlantaServices.GetAsync();
            return Ok(detalleplanta);
        }

        //Insertar
        [HttpPost("Crear")]
        public async Task<IActionResult> CreateDetallePlanta([FromBody] DetallePlanta detallePlanta)
        {
            if (detallePlanta == null)
            {
                return BadRequest();
            }
            if (detallePlanta.Planta == string.Empty)
            {
                ModelState.AddModelError("Planta", "La planta no puede estar Vacia");
            }
            await _detallePlantaServices.InsertarDetallePlant(detallePlanta);
            detallePlanta = await _detallePlantaServices.DetalleId(detallePlanta.Id);
            return Created("Created", detallePlanta);
        }

        //Buscar por Id
        [HttpGet("Buscar/{Id}")]
        public async Task<IActionResult> detalleporId(string Id)
        {
            return Ok(await _detallePlantaServices.DetalleId(Id));
        }
    }
}
