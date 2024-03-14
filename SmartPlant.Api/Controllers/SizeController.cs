using Microsoft.AspNetCore.Mvc;
using SmartPlant.Api.Models;
using SmartPlant.Api.Services;
using SmartPlant.Api.Configurations;
using MongoDB.Bson;

namespace SmartPlant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SizeController : ControllerBase
{
    private readonly ILogger<SizeController> _logger;
    private readonly SizeServices _sizeServices;

    public SizeController(ILogger<SizeController> logger, SizeServices sizeServices)
    {
        _logger = logger;
        _sizeServices = sizeServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetHum(){
        var size = await _sizeServices.GetAsync();
        return Ok(size);
    }

    [HttpPost]
    public async Task<IActionResult> InsertSize([FromBody] Size sizeToInsert)
    {
        if(sizeToInsert == null)
            return BadRequest();
        if(Convert.ToString(sizeToInsert.tam) == string.Empty)
            ModelState.AddModelError("tamaño","El sensor no debe estar vacio");
        await _sizeServices.InsertSize(sizeToInsert);

        return Created("Created",true);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeleteSize(string id)
    {
        if(id == null)
            return BadRequest();
        if(id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");

        await _sizeServices.DeleteSize(id);

        return Ok();
    }

    [HttpPut("SizeToUpdate")]
    public async Task<IActionResult> UpdateSize(Size size)
    {
        if(size == null)
            return BadRequest();
        if(size.Id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");
        if(Convert.ToString(size.tam) == string.Empty)
            ModelState.AddModelError("Name","No debe dejar el nombre vacio");
        

        await _sizeServices.UpdateSize(size);

        return Ok();
    }
}
