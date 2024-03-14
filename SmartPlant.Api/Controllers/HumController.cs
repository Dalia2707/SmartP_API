using Microsoft.AspNetCore.Mvc;
using SmartPlant.Api.Models;
using SmartPlant.Api.Services;
using SmartPlant.Api.Configurations;
using MongoDB.Bson;

namespace SmartPlant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HumController : ControllerBase
{
    private readonly ILogger<HumController> _logger;
    private readonly HumServices _humServices;

    public HumController(ILogger<HumController> logger, HumServices humServices)
    {
        _logger = logger;
        _humServices = humServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetHum(){
        var hume = await _humServices.GetAsync();
        return Ok(hume);
    }

    [HttpPost]
    public async Task<IActionResult> InsertHum([FromBody] Hum HumToInsert)
    {
        if(HumToInsert == null)
            return BadRequest();
        if(Convert.ToString(HumToInsert.Humidity) == string.Empty)
            ModelState.AddModelError("Humedad","El sensor no debe estar vacio");
        await _humServices.InsertHum(HumToInsert);

        return Created("Created",true);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeleteHum(string id)
    {
        if(id == null)
            return BadRequest();
        if(id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");

        await _humServices.DeleteHum(id);

        return Ok();
    }

    [HttpPut("HumeToUpdate")]
    public async Task<IActionResult> UpdateHum(Hum hume)
    {
        if(hume == null)
            return BadRequest();
        if(hume.Id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");
        if(Convert.ToString(hume.Humidity) == string.Empty)
            ModelState.AddModelError("Name","No debe dejar el nombre vacio");
        

        await _humServices.UpdateHum(hume);

        return Ok();
    }
}
