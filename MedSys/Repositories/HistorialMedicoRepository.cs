using MedSys.Data;
using MedSys.Models;
using Microsoft.EntityFrameworkCore;

namespace MedSys.Repositories
{
    public class HistorialMedicoRepository : IHistorialMedicoRepository
    {
        private readonly AppDbContext _context;

        public HistorialMedicoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddHistorialAsync(HistorialMedico historial)
        {
            _context.HistorialesMedicos.Add(historial);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHistorialAsync(int id)
        {
            HistorialMedico? historial = await _context.HistorialesMedicos.FindAsync(id);
            if (historial != null)
            {
                _context.HistorialesMedicos.Remove(historial);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<HistorialMedico>> GetAllHistorialesAsync()
        {
            return await _context.HistorialesMedicos.ToListAsync();
        }

        public async Task<HistorialMedico> GetHistorialByIdAsync(int id)
        {
            // TODO : Tratar en caso de NULL
            return await _context.HistorialesMedicos.FindAsync(id);
        }

        public async Task<IEnumerable<HistorialMedico>> GetHistorialesByPacienteIdAsync(int pacienteId)
        {
            // TODO : Tratar en caso de NULL
            return await _context.HistorialesMedicos.Where(h => h.IdPaciente == pacienteId).ToListAsync();
        }

        public async Task UpdateHistorialAsync(HistorialMedico historial)
        {
            throw new NotImplementedException();
        }
    }
}
