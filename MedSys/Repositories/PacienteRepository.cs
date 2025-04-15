using MedSys.Data;
using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;

namespace MedSys.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;

        public PacienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PacienteResponse> AddPacienteAsync(NewPacienteRequest request)
        {
            Paciente paciente = request.ToPaciente();
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente.ToPacienteResponse();
        }

        public async Task<IEnumerable<PacienteResponse>> GetAllPacientesAsync()
        {
            return (await _context.Pacientes.ToListAsync()).Select(paciente => paciente.ToPacienteResponse());
        }

        public async Task<IEnumerable<PacienteResponse>> GetPacientesPaginatedAsync(int page, int pageSize)
        {
            return (await _context.Pacientes
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync())
                                 .Select(user => user.ToPacienteResponse());
        }

        public async Task<PacienteResponse> GetPacienteByIdAsync(int id)
        {
            // TODO : Tratar en caso de NULL
            return (await _context.Pacientes.FindAsync(id)).ToPacienteResponse();
        }

        public async Task<PacienteResponse> GetPacienteByDniAsync(string dni)
        {
            // TODO : Tratar en caso de NULL
            return (await _context.Pacientes.FirstOrDefaultAsync(paciente => paciente.DNI == dni)).ToPacienteResponse();
        }

        public async Task<bool> PacienteExistsByIdAsync(int id)
        {
            return await _context.Pacientes.AnyAsync(paciente => paciente.Id == id);
        }

        public async Task<bool> PacienteExistsByDniAsync(string dni)
        {
            return await _context.Pacientes.AnyAsync(paciente => paciente.DNI == dni);
        }

        public async Task UpdatePacienteAsync(UpdatePacienteRequest request)
        {
            Paciente? paciente = await _context.Pacientes.FindAsync(request.Id);

            request.DoUpdate(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePacienteAsync(int id)
        {
            Paciente? paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
