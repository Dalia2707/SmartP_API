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

    [HttpGet]
    [Route("IniciarSesion")]
    public async Task<IActionResult> IniciarSesion(string correo, string password)
    {
        Response<User> _response = new Response<User>();

        try
        {
            var usuario = await _userServices.GetUser(correo, password);

            if (usuario != null)
                _response = new Response<User>() { status = true, msg = "ok", value = usuario };
            else
                _response = new Response<User>() { status = false, msg = "no encontrado", value = null };

            return StatusCode(StatusCodes.Status200OK, _response);
        }
        catch (Exception ex)
        {
            _response = new Response<User>() { status = false, msg = ex.Message, value = null };
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }


    [HttpPost]
    public async Task<IActionResult> InsertUser([FromBody] User userToInsert)
    {
        Response<User> _response = new Response<User>();
        try
        {
            if (userToInsert == null)
            {
                return BadRequest();
            }

            if (userToInsert.NameUser == string.Empty)
            {
                ModelState.AddModelError("Name", "El usuario no debe estar vacio");
                _response.status = false;
                _response.msg = "El usuario no debe estar vacio";
                _response.value = userToInsert;
            }
            else
            {
                User findUser = await _userServices.GetUser(userToInsert.EmailUser);
                if (findUser == null)
                {
                    await _userServices.InsertUser(userToInsert);
                    _response.value = userToInsert;
                    _response.status = true;
                    _response.msg = "OK";
                }
                else {
                    _response.status = false;
                    _response.msg = "El usuario ya existe";
                    _response.value = userToInsert;
                
                }
               
            }

        } catch (Exception ex)
        {
            _response.status = false;
            _response.msg = ex.Message;
        }
        return Ok(_response);
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
