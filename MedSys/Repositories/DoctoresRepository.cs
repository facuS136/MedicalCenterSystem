using MedSys.Data;
using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MedSys.Repositories
{
    public class DoctoresRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctoresRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorResponse> AddDoctorAsync(NewDoctorRequest request)
        {
            Doctor doctor = request.ToDoctor();
            _context.Doctores.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor.ToDoctorResponse();
        }

        public async Task<IEnumerable<DoctorResponse>> GetAllDoctoresAsync()
        {
            return (await _context.Doctores.ToListAsync()).Select(doctor => doctor.ToDoctorResponse());
        }

        public async Task<IEnumerable<DoctorResponse>> GetDoctoresPaginatedAsync(int page, int pageSize)
        {
            return (await _context.Doctores
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync())
                                 .Select(user => user.ToDoctorResponse());
        }

        public async Task<DoctorResponse> GetDoctorByIdAsync(int id)
        {
            return (await _context.Doctores.FindAsync(id)).ToDoctorResponse();
        }

        public async Task<DoctorResponse> GetDoctorByDniAsync(string dni)
        {
            // TODO : Tratar en caso de NULL
            return (await _context.Doctores.FirstOrDefaultAsync(doctor => doctor.DNI == dni)).ToDoctorResponse();
        }

        public async Task<DoctorResponse> GetDoctorByUserName(string userName)
        {
            // TODO : Tratar en caso de NULL
            return (await _context.Doctores.FirstOrDefaultAsync(doctor => doctor.Username == userName)).ToDoctorResponse();
        }

        public async Task<bool> DoctorExistsByIdAsync(int id)
        {
            return await _context.Doctores.AnyAsync(doctor => doctor.Id == id);
        }

        public async Task<bool> DoctorExistsByDniAsync(string dni)
        {
            return await _context.Doctores.AnyAsync(doctor => doctor.DNI == dni);
        }

        public async Task<bool> DoctorExistsByUserName(string userName)
        {
            return await _context.Doctores.AnyAsync(doctor => doctor.Username == userName);
        }

        public async Task UpdateDoctorAsync(UpdateDoctorRequest request)
        {
            Doctor? doctor = await _context.Doctores.FindAsync(request.Id);

            request.DoUpdate(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            Doctor? doctor = await _context.Doctores.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctores.Remove(doctor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
