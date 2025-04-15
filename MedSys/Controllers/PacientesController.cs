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
    public class PacientesController : ControllerBase
    {
        private readonly PacientesService _pacientesService;

        public PacientesController(PacientesService pacientesService)
        {
            _pacientesService = pacientesService;
        }

        // GET: api/pacientes
        // Retorna todos los pacientes.
        [HttpGet]
        public async Task<IActionResult> GetAllPacientes()
        {
            var pacientes = await _pacientesService.GetAllPacientesAsync();
            return Ok(pacientes);
        }

        // GET: api/pacientes/paginated?page=1&pageSize=10
        // Retorna los pacientes según la paginación solicitada.
        [HttpGet("paginated")]
        public async Task<IActionResult> GetPacientesPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var pacientes = await _pacientesService.GetPacientesPaginatedAsync(page, pageSize);
            return Ok(pacientes);
        }

        // GET: api/pacientes/5
        // Retorna un paciente en función de su ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacienteById(int id)
        {
            try
            {
                PacienteResponse paciente = await _pacientesService.GetPacienteByIdAsync(id);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // POST: api/pacientes
        // Crea un nuevo paciente utilizando NewPacienteRequest.
        [HttpPost]
        public async Task<IActionResult> CreatePaciente([FromBody] NewPacienteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // El servicio se encarga de procesar la creación
                PacienteResponse response = await _pacientesService.CrearPacienteAsync(request);

                // Usamos CreatedAtAction para devolver 201 Created
                return CreatedAtAction(nameof(GetPacienteById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/pacientes/5
        // Actualiza un paciente existente.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaciente(int id, [FromBody] UpdatePacienteRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("El ID del paciente no coincide con el ID en la URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _pacientesService.UpdatePacienteAsync(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // DELETE: api/pacientes/5
        // Elimina el paciente con el ID especificado.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            try
            {
                await _pacientesService.DeletePacienteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
