using Microsoft.AspNetCore.Mvc;
using MedSys.Models;
using MedSys.Services;
using System.Threading.Tasks;
using MedSys.DTOs.Responses;
using MedSys.DTOs.Requests;

namespace MedSys.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] NewUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserResponse response = (await _usuarioService.CrearUsuarioAsync(request));

                // Proporcionamos el id para construir la URL correctamente
                return CreatedAtAction(nameof(GetUsuarioById), new { id = response.Id }, response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/usuarios
        // Retorna todos los usuarios
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            List<UserResponse> usuarios = (await _usuarioService.GetAllUsuariosAsync()).ToList();
            return Ok(usuarios);
        }

        // GET: api/usuarios/paginated?page=1&pageSize=10
        // Retorna los usuarios de acuerdo a la paginación solicitada
        [HttpGet("paginated")]
        public async Task<IActionResult> GetUsuariosPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            List<UserResponse> usuarios = (await _usuarioService.GetUsuariosPaginatedAsync(page, pageSize)).ToList();
            return Ok(usuarios);
        }

        // GET: api/usuarios/5
        // Retorna un usuario en función de su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            try
            {
                UserResponse usuario = await _usuarioService.GetUsuarioByIdAsync(id);
                return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // PUT: api/usuarios/5
        // Actualiza un usuario existente. El ID en la URL debe coincidir con el ID del objeto.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUserRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("El ID del usuario no coincide con el ID en la URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _usuarioService.UpdateUsuarioAsync(request);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // DELETE: api/usuarios/5
        // Elimina el usuario con el id especificado.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                await _usuarioService.DeleteUsuarioAsync(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
