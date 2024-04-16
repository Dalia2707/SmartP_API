using Microsoft.AspNetCore.Mvc;
using SmartPlant.Api.Models;
using SmartPlant.Api.Services;
using SmartPlant.Api.Configurations;
using MongoDB.Bson;
using System.Collections.Generic;

namespace SmartPlant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantController : ControllerBase
{
    private readonly ILogger<PlantController> _logger;
    private readonly PlantServices _plantServices;
    private readonly UserServices _UserServices;

    public PlantController(ILogger<PlantController> logger, PlantServices plantServices, UserServices userServices)
    {
        _logger = logger;
        _plantServices = plantServices;
        _UserServices = userServices;
    }

    [HttpGet("Lista")]
    public async Task<IActionResult> GetPlants(){
        Response<List<Plant>> _response = new Response<List<Plant>> ();

        try
        {
            var plants = await _plantServices.GetAsync();

            if (plants != null)
                _response = new Response<List<Plant>>() { status = true, msg = "ok", value = plants };
            else
                _response = new Response<List<Plant>>() { status = false, msg = "no se encontraron registros", value = null };

            return StatusCode(StatusCodes.Status200OK, _response);
        }
        catch (Exception ex)
        {
            _response = new Response<List<Plant>>() { status = false, msg = ex.Message, value = null };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }

        //var plants = await _plantServices.GetAsync();
        //return Ok(plants);
    }

    [HttpPost("Crear")]
    public async Task<IActionResult> InsertPlant([FromBody] Plant plantToInsert)
    {
        if (plantToInsert == null)
            return BadRequest();
        if (plantToInsert.NamePlant == string.Empty)
            ModelState.AddModelError("Name", "La planta no debe estar vacio");
        if (plantToInsert.TypePlant == string.Empty)
            ModelState.AddModelError("Type", "La planta no debe estar vacio");
        if (plantToInsert.UsersId == string.Empty)
            ModelState.AddModelError("User", "La planta no debe estar vacio");

        if (!string.IsNullOrEmpty(plantToInsert.UsersId))
        {
            var user = await _UserServices.GetUserId(plantToInsert.UsersId);
            if(user == null)
            {
                return BadRequest("No existe el usuario");
            }
        }

        await _plantServices.InsertPlant(plantToInsert);

        plantToInsert = await _plantServices.plantporid(plantToInsert.Id);

        return Created("Created", plantToInsert);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeletePlant(string id)
    {
        if(id == null)
            return BadRequest();
        if(id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");

        await _plantServices.DeletePlant(id);

        return Ok();
    }

    [HttpPut("PlantToUpdate")]
    public async Task<IActionResult> UpdatePlant(Plant plant)
    {
        if(plant == null)
            return BadRequest();
        if(plant.Id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");
        if(plant.NamePlant == string.Empty)
            ModelState.AddModelError("Name","No debe dejar el nombre vacio");
        if(plant.TypePlant == string.Empty )
            ModelState.AddModelError("Tipo","No debe dejar el tipo vacio");
        if(plant.UsersId == string.Empty)
            ModelState.AddModelError("User","No debe dejar el user vacio");

        await _plantServices.UpdatePlant(plant);

        return Ok();
    }

    [HttpGet("Buscar/{Id}")]
    public async Task<IActionResult> plantporid(string Id)
    {
        return Ok(await _plantServices.plantporid(Id));
    }
}
