using System.Collections.Generic;
using System.Threading.Tasks;
using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Models;

namespace MedSys.Repositories
{
    public interface IUsuarioRepository
    {
        Task<UserResponse> AddUsuarioAsync(NewUserRequest newUser);
        Task<IEnumerable<UserResponse>> GetAllUsuariosAsync();
        Task<IEnumerable<UserResponse>> GetUsuariosPaginatedAsync(int page, int pageSize);
        Task<UserResponse> GetUsuarioByIdAsync(int id);
        Task<UserResponse> GetUsuarioByDniAsync(string dni);
        Task<UserResponse> GetUsuarioByUserName(string userName);
        Task<bool> UsuarioExistsByIdAsync(int id);
        Task<bool> UsuarioExistsByDniAsync(string dni);
        Task<bool> UsuarioExistsByUserName(string userName);
        Task UpdateUsuarioAsync(UpdateUserRequest update);
        Task DeleteUsuarioAsync(int id);
    }

    public interface IDoctorRepository
    {
        Task<DoctorResponse> AddDoctorAsync(NewDoctorRequest request);
        Task<IEnumerable<DoctorResponse>> GetAllDoctoresAsync();
        Task<IEnumerable<DoctorResponse>> GetDoctoresPaginatedAsync(int page, int pageSize);
        Task<DoctorResponse> GetDoctorByIdAsync(int id);
        Task<DoctorResponse> GetDoctorByDniAsync(string dni);
        Task<DoctorResponse> GetDoctorByUserName(string userName);
        Task<bool> DoctorExistsByIdAsync(int id);
        Task<bool> DoctorExistsByDniAsync(string dni);
        Task<bool> DoctorExistsByUserName(string userName);
        Task UpdateDoctorAsync(UpdateDoctorRequest request);
        Task DeleteDoctorAsync(int id);
    }
    public interface IHistorialMedicoRepository
    {
        Task<IEnumerable<HistorialMedico>> GetAllHistorialesAsync();
        Task<HistorialMedico> GetHistorialByIdAsync(int id);
        Task<IEnumerable<HistorialMedico>> GetHistorialesByPacienteIdAsync(int pacienteId);
        Task AddHistorialAsync(HistorialMedico historial);
        Task UpdateHistorialAsync(HistorialMedico historial);
        Task DeleteHistorialAsync(int id);
    }
    public interface IPacienteRepository
    {
        Task<PacienteResponse> AddPacienteAsync(NewPacienteRequest request);
        Task<IEnumerable<PacienteResponse>> GetAllPacientesAsync();
        Task<IEnumerable<PacienteResponse>> GetPacientesPaginatedAsync(int page, int pageSize);
        Task<PacienteResponse> GetPacienteByIdAsync(int id);
        Task<PacienteResponse> GetPacienteByDniAsync(string dNI);
        Task<bool> PacienteExistsByIdAsync(int id);
        Task UpdatePacienteAsync(UpdatePacienteRequest request);
        Task DeletePacienteAsync(int id);
        Task<bool> PacienteExistsByDniAsync(string dNI);
    }
    public interface ITurnoRepository
    {
        Task<TurnoResponse> AddTurnoAsync(NewTurnoRequest request);
        Task<IEnumerable<TurnoResponse>> GetAllTurnosAsync();
        Task<IEnumerable<TurnoResponse>> GetTurnosPaginatedAsync(GetTurnoRequest request, int page, int pageSize);
        Task<TurnoResponse> GetTurnoByIdAsync(int id);
        Task<bool> TurnoExistsByIdAsync(int id);
        Task<bool> TurnoEstaDisponible(TurnoRequest request);
        Task UpdateTurnoAsync(UpdateTurnoRequest request);
        Task DeleteTurnoAsync(int id);
    }
}
