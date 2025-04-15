using MedSys.Data;
using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MedSys.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly AppDbContext _context;

        public TurnoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TurnoResponse> AddTurnoAsync(NewTurnoRequest request)
        {
            Turno turno = request.ToTurno();
            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();
            return turno.ToTurnoResponse();
        }

        public async Task<IEnumerable<TurnoResponse>> GetAllTurnosAsync()
        {
            return (await _context.Turnos.ToListAsync()).Select(turno => turno.ToTurnoResponse());
        }

        public async Task<TurnoResponse> GetTurnoByIdAsync(int id)
        {
            return (await _context.Turnos.FindAsync(id)).ToTurnoResponse();
        }

        public async Task<IEnumerable<TurnoResponse>> GetTurnosPaginatedAsync(GetTurnoRequest request, int page, int pageSize)
        {
            /* TODO : Tratar en caso de NULL
            return await _context.Turnos
                .Where(turno => request.Coincide(turno))
                .Select(turno => turno.ToTurnoResponse())
                .ToListAsync();
            */
            // Iniciamos la consulta a partir del DbSet
            var query = _context.Turnos.AsQueryable();

            // Agrega filtros condicionalmente
            if (request.IdUser.HasValue)
            {
                query = query.Where(t => t.IdUser == request.IdUser.Value);
            }

            if (request.IdDoctor.HasValue)
            {
                query = query.Where(t => t.IdDoctor == request.IdDoctor.Value);
            }

            if (request.IdPaciente.HasValue)
            {
                query = query.Where(t => t.IdPaciente == request.IdPaciente.Value);
            }

            if (request.Dia.HasValue)
            {
                query = query.Where(t => t.Dia == request.Dia.Value);
            }

            if (request.Hora.HasValue)
            {
                query = query.Where(t => t.Hora == request.Hora.Value);
            }

            // Aplica la paginación a la consulta:
            query = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            // Proyecta la entidad Turno a TurnoResponse y retorna la lista
            return await query.Select(t => t.ToTurnoResponse()).ToListAsync();
        }

        public async Task<bool> TurnoExistsByIdAsync(int id)
        {
            return await _context.Turnos.AnyAsync(turno => turno.Id == id);
        }

        public async Task<bool> TurnoEstaDisponible(TurnoRequest request)
        {
            return !await _context.Turnos.AnyAsync(turno => turno.IdDoctor == request.IdDoctor && turno.Hora == request.Hora);
        }

        public async Task UpdateTurnoAsync(UpdateTurnoRequest request)
        {
            Turno? turno = await _context.Turnos.FindAsync(request.Id);

            request.DoUpdate(turno);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTurnoAsync(int id)
        {
            Turno? turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                _context.Turnos.Remove(turno);
                await _context.SaveChangesAsync();
            }
        }
    }
}
