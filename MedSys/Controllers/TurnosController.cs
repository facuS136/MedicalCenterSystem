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
    public class TurnosController : ControllerBase
    {
        private readonly TurnoService _turnosService;
        
        public TurnosController(TurnoService turnosService)
        {
            _turnosService = turnosService;
        }

        // POST: api/turnos
        [HttpPost]
        public async Task<IActionResult> CreateTurno([FromBody] NewTurnoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TurnoResponse response = (await _turnosService.CrearTurnoAsync(request));

                // Proporcionamos el id para construir la URL correctamente
                return CreatedAtAction(nameof(GetTurnoById), new { id = response.Id }, response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // GET: api/turnos
        // Retorna todos los turnos
        [HttpGet]
        public async Task<IActionResult> GetAllTurnos()
        {
            List<TurnoResponse> turnos = (await _turnosService.GetAllTurnosAsync()).ToList();
            return Ok(turnos);
        }

        // GET: api/turnos/paginated?page=1&pageSize=10
        // Retorna los turnos de acuerdo a la paginación solicitada
        [HttpGet("paginated")]
        public async Task<IActionResult> GetTurnosPaginated([FromQuery] GetTurnoRequest request, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            List<TurnoResponse> turnos = (await _turnosService.GetTurnosPaginatedAsync(request, page, pageSize)).ToList();
            return Ok(turnos);
        }

        // GET: api/turnos/5
        // Retorna un turno en función de su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTurnoById(int id)
        {
            try
            {
                TurnoResponse turno = await _turnosService.GetTurnoByIdAsync(id);
                return Ok(turno);
            }
            catch (System.Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // PUT: api/turnos/5
        // Actualiza un turno existente. El ID en la URL debe coincidir con el ID del objeto.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTurno(int id, [FromBody] UpdateTurnoRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("El ID del turno no coincide con el ID en la URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _turnosService.UpdateTurnoAsync(request);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // DELETE: api/turnos/5
        // Elimina el turno con el id especificado.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurno(int id)
        {
            try
            {
                await _turnosService.DeleteTurnoAsync(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
