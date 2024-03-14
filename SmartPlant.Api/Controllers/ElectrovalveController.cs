using Microsoft.AspNetCore.Mvc;
using SmartPlant.Api.Models;
using SmartPlant.Api.Services;
using SmartPlant.Api.Configurations;
using MongoDB.Bson;

namespace SmartPlant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ElectrovalveController : ControllerBase
{
    private readonly ILogger<ElectrovalveController> _logger;
    private readonly ElectrovalveServices _eleServices;

    public ElectrovalveController(ILogger<ElectrovalveController> logger, ElectrovalveServices eleServices)
    {
        _logger = logger;
        _eleServices = eleServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetEle(){
        var electrovalves = await _eleServices.GetAsync();
        return Ok(electrovalves);
    }

    [HttpPost]
    public async Task<IActionResult> InsertEle([FromBody] Electrovalve eleToInsert)
    {
        if(eleToInsert == null)
            return BadRequest();
        if(Convert.ToString(eleToInsert.Id) == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");
        if(Convert.ToString(eleToInsert.Open) == string.Empty)
            ModelState.AddModelError("Open","No debe dejar el Open vacio");
        if(Convert.ToString(eleToInsert.Date) == string.Empty )
            ModelState.AddModelError("Date","No debe dejar el Date vacio");
        await _eleServices.InsertEle(eleToInsert);

        return Created("Created",true);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeleteEle(string id)
    {
        if(id == null)
            return BadRequest();
        if(id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");

        await _eleServices.DeleteEle(id);

        return Ok();
    }

    [HttpPut("EleToUpdate")]
    public async Task<IActionResult> UpdateEle(Electrovalve electrovalve)
    {
        if(electrovalve == null)
            return BadRequest();
        if(Convert.ToString(electrovalve.Id) == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");
        if(Convert.ToString(electrovalve.Open) == string.Empty)
            ModelState.AddModelError("Open","No debe dejar el Open vacio");
        if(Convert.ToString(electrovalve.Date) == string.Empty )
            ModelState.AddModelError("Date","No debe dejar el Date vacio");
        

        await _eleServices.UpdateEle(electrovalve);

        return Ok();
    }
}
