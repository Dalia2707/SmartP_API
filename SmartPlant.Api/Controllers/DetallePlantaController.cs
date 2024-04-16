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
        private readonly PlantServices _plantaServices;


        public DetallePlantaController(ILogger<DetallePlantaController> logger, DetallePlantaServices detallePlantaServices, PlantServices plantaServices)
        {
            _logger = logger;
            _detallePlantaServices = detallePlantaServices;
            _plantaServices = plantaServices;
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
            Response<DetallePlanta> _response = new Response<DetallePlanta>();

            try
            {
                if (detallePlanta == null)
            {
                    _response = new Response<DetallePlanta>() { status = false, msg = "error", value = null };
                    return StatusCode(StatusCodes.Status500InternalServerError, _response);
                    
            }
            //if (detallePlanta.Planta == string.Empty)
            //{
            //    ModelState.AddModelError("Planta", "La planta no puede estar Vacia");
            //}
            await _plantaServices.InsertPlant(detallePlanta.Planta);
            detallePlanta.Plant = detallePlanta.Planta.Id;

            await _detallePlantaServices.InsertarDetallePlant(detallePlanta);
            detallePlanta = await _detallePlantaServices.DetalleId(detallePlanta.Id);
           // return Created("Created", detallePlanta);



        
                await _detallePlantaServices.UpdateDetallePlant(detallePlanta);
                _response = new Response<DetallePlanta>() { status = true, msg = "ok", value = detallePlanta };


                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new Response<DetallePlanta>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        //Insertar
        [HttpPost("Actualizar")]
        public async Task<IActionResult> UpdateDetallePlanta([FromBody] DetallePlanta detallePlanta)
        {
           
            Response<DetallePlanta> _response = new Response<DetallePlanta>();

            try
            {
                await _detallePlantaServices.UpdateDetallePlant(detallePlanta);
                _response = new Response<DetallePlanta>() { status = true, msg = "ok", value = detallePlanta };
              

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new Response<DetallePlanta>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }



        }

        //Buscar por Id
        [HttpGet("Buscar/{Id}")]
        public async Task<IActionResult> detalleporId(string Id)
        {
            return Ok(await _detallePlantaServices.DetalleId(Id));
        }

        //Buscar por Id
        [HttpGet("BuscarByPlant/{Id}")]
        public async Task<IActionResult> detalleporIdPlant(string Id)
        {

            // Ok(await _detallePlantaServices.detalleporIdPlant(Id));

            Response<DetallePlanta> _response = new Response<DetallePlanta>();

            try
            {
                var usuario = await _detallePlantaServices.detalleporIdPlant(Id);

                if (usuario != null)
                    _response = new Response<DetallePlanta>() { status = true, msg = "ok", value = usuario };
                else
                    _response = new Response<DetallePlanta>() { status = false, msg = "no encontrado", value = null };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {
                _response = new Response<DetallePlanta>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }
    }
}
