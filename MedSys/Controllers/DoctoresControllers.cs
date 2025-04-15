using Microsoft.AspNetCore.Mvc;
using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedSys.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctoresController : ControllerBase
    {
        private readonly DoctoresService _doctoresService;

        public DoctoresController(DoctoresService doctoresService)
        {
            _doctoresService = doctoresService;
        }

        // POST: api/doctores
        // Crea un nuevo doctor utilizando NewDoctorRequest.
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] NewDoctorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // El servicio se encargará de convertir el request a la entidad, validar, encriptar, etc.
                DoctorResponse response = await _doctoresService.CrearDoctorAsync(request);

                // Usamos CreatedAtAction para devolver 201 Created, pasando como valor el id del recurso creado.
                return CreatedAtAction(nameof(GetDoctorById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/doctores
        // Retorna todos los doctores.
        [HttpGet]
        public async Task<IActionResult> GetAllDoctores()
        {
            var doctores = await _doctoresService.GetAllDoctoresAsync();
            return Ok(doctores);
        }

        // GET: api/doctores/paginated?page=1&pageSize=10
        // Retorna los doctores según la paginación solicitada.
        [HttpGet("paginated")]
        public async Task<IActionResult> GetDoctoresPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var doctores = await _doctoresService.GetDoctoresPaginatedAsync(page, pageSize);
            return Ok(doctores);
        }

        // GET: api/doctores/5
        // Retorna un doctor en función de su ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            try
            {
                DoctorResponse doctor = await _doctoresService.GetDoctorByIdAsync(id);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // PUT: api/doctores/5
        // Actualiza un doctor existente.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("El ID del doctor no coincide con el ID en la URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _doctoresService.UpdateDoctorAsync(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // DELETE: api/doctores/5
        // Elimina el doctor con el ID especificado.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                await _doctoresService.DeleteDoctorAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
