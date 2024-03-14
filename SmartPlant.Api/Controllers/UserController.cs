using Microsoft.AspNetCore.Mvc;
using SmartPlant.Api.Models;
using SmartPlant.Api.Services;
//using SmartPlant.Api.Configurations;

namespace SmartPlant.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserServices _userServices;

    public UserController(ILogger<UserController> logger, UserServices userServices)
    {
        _logger = logger;
        _userServices = userServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers(){
        var users = await _userServices.GetAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> InsertUser([FromBody] User userToInsert)
    {
        if(userToInsert == null)
        {
            return BadRequest();
        }

        if(userToInsert.NameUser == string.Empty)
        {
            ModelState.AddModelError("Name", "El usuario no debe estar vacio");
        }
 
        await _userServices.InsertUser(userToInsert);

        return Created("Created",true);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        if(id == null)
            return BadRequest();
        if(id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");

        await _userServices.DeleteUser(id);

        return Ok();
    }

    [HttpPut("UserToUpdate")]
    public async Task<IActionResult> UpdateUser(User user)
    {
        if(user == null)
            return BadRequest();
        if(user.Id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");
        if(user.NameUser == string.Empty)
            ModelState.AddModelError("Name","No debe dejar el nombre vacio");
        if(user.EmailUser == string.Empty )
            ModelState.AddModelError("Email","No debe dejar el email vacio");
        if(user.Password == string.Empty)
            ModelState.AddModelError("Contraseña","No debe dejar la contraseña vacio");

        await _userServices.UpdateUser(user);

        return Ok();
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Userporid(string id)
    {
        return Ok(await _userServices.GetUserId(id));
    }
}
