using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Models;
using MedSys.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace MedSys.Services
{
    public class TurnoService
    {
        private readonly IUsuarioRepository _userRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ITurnoRepository _turnoRepository;
        private readonly int _duracionTurno = 20;

        public TurnoService(
            IUsuarioRepository userRepository,
            IDoctorRepository doctorRepository,
            IPacienteRepository pacienteRepository,
            ITurnoRepository turnoRepository)
        {
            _userRepository = userRepository;
            _doctorRepository = doctorRepository;
            _pacienteRepository = pacienteRepository;
            _turnoRepository = turnoRepository;
        }

        public async Task<TurnoResponse> CrearTurnoAsync(NewTurnoRequest request)
        {
            AssertNotNull(request);
            await AssertIdUserValido(request);
            await AssertIdDoctorValido(request);
            await AssertIdPacienteValido(request);
            AssertHoraValida(request); // Ver que es multiplo de _duracionTurno
            await AssertTurnoDisponible(request);            // Ver que el turno no se pisa


            return await _turnoRepository.AddTurnoAsync(request);
        }

        public async Task<IEnumerable<TurnoResponse>> GetAllTurnosAsync()
        {
            return await _turnoRepository.GetAllTurnosAsync();
        }

        public async Task<IEnumerable<TurnoResponse>> GetTurnosPaginatedAsync(GetTurnoRequest request, int page, int pageSize)
        {
            return await _turnoRepository.GetTurnosPaginatedAsync(request, page, pageSize);
        }

        public async Task<TurnoResponse> GetTurnoByIdAsync(int id)
        {
            await AssertExisteTurnoById(id);
            return await _turnoRepository.GetTurnoByIdAsync(id);
        }

        public async Task UpdateTurnoAsync(UpdateTurnoRequest request)
        {
            AssertNotNull(request);
            await AssertExisteTurnoById(request.Id);
            await AssertIdUserValido(request);
            await AssertIdDoctorValido(request);
            await AssertIdPacienteValido(request);
            // TODO : Assert fecha y hora valida
            AssertHoraValida(request); // Ver que es multiplo de _duracionTurno
            await AssertTurnoDisponible(request);            // Ver que el turno no se pisa

            await _turnoRepository.UpdateTurnoAsync(request);
        }

        public async Task DeleteTurnoAsync(int id)
        {
            // Verifica la existencia antes de eliminar
            await AssertExisteTurnoById(id);

            await _doctorRepository.DeleteDoctorAsync(id);
        }


        #region Asserts
        private async Task AssertExisteTurnoById(int id)
        {
            if(!await _turnoRepository.TurnoExistsByIdAsync(id))
            {
                throw new Exception("El turno no existe.");
            }
        }
        private async Task AssertIdPacienteValido(TurnoRequest request)
        {
            // Validación de negocio: Verificar que el paciente exista
            if (!await _pacienteRepository.PacienteExistsByIdAsync((int)request.IdPaciente))
            {
                throw new Exception($"El paciente de ID {request.IdPaciente} no existe.");
            }
        }

        private async Task AssertIdDoctorValido(TurnoRequest request)
        {
            // Validación de negocio: Verificar que el doctor exista
            if (!await _doctorRepository.DoctorExistsByIdAsync((int)request.IdDoctor))
            {
                throw new Exception($"El doctor de ID {request.IdDoctor} no existe.");
            }
        }

        private async Task AssertIdUserValido(TurnoRequest request)
        {
            // Validación de negocio: Verificar que el usuario exista
            if (!await _userRepository.UsuarioExistsByIdAsync((int)request.IdUser))
            {
                throw new Exception($"El usuario de ID {request.IdUser} no existe.");
            }
        }

        private static void AssertNotNull(TurnoRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "El objeto request no puede ser nulo.");
        }

        private async Task AssertTurnoDisponible(TurnoRequest request)
        {
            if (!await _turnoRepository.TurnoEstaDisponible(request))
            {
                throw new Exception("Este turno no esta disponible");
            }
        }

        private void AssertHoraValida(TurnoRequest request)
        {
            if (((TimeSpan)request.Hora).TotalMinutes % _duracionTurno != 0)
            {
                throw new Exception($"La hora del turno no es valida, debe ser multiplo de {_duracionTurno} en minutos");
            }
        }
        #endregion
    }
}
