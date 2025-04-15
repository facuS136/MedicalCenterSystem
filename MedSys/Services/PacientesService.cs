using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Repositories;

namespace MedSys.Services
{
    public class PacientesService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacientesService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<PacienteResponse> CrearPacienteAsync(NewPacienteRequest request)
        {
            AssertNotNull(request);
            AssertNombreApellidoValidos(request);
            await AssertDniValido(request);
            AssertTelefonoValido(request);
            AssertObraSocialValida(request);

            return (await _pacienteRepository.AddPacienteAsync(request));
        }

        public async Task<IEnumerable<PacienteResponse>> GetAllPacientesAsync()
        {
            return await _pacienteRepository.GetAllPacientesAsync();
        }

        public async Task<IEnumerable<PacienteResponse>> GetPacientesPaginatedAsync(int page, int pageSize)
        {
            return await _pacienteRepository.GetPacientesPaginatedAsync(page, pageSize);
        }

        public async Task<PacienteResponse> GetPacienteByIdAsync(int id)
        {
            await AssertExistePacienteById(id);
            PacienteResponse paciente = await _pacienteRepository.GetPacienteByIdAsync(id);
            return paciente;
        }

        public async Task UpdatePacienteAsync(UpdatePacienteRequest request)
        {
            AssertNotNull(request);
            AssertNombreApellidoValidos(request);
            await AssertDniUpdateValido(request);
            AssertTelefonoValido(request);
            AssertObraSocialValida(request);

            await _pacienteRepository.UpdatePacienteAsync(request);
        }

        public async Task DeletePacienteAsync(int id)
        {
            await AssertExistePacienteById(id);

            await _pacienteRepository.DeletePacienteAsync(id);
        }
        
        #region ASSERTS

        private void AssertNotNull(PacienteRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "El objeto request no puede ser nulo.");
        }

        private static void AssertNombreApellidoValidos(PacienteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new Exception("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.Apellido))
                throw new Exception("El apellido es obligatorio.");
        }

        private async Task AssertDniValido(PacienteRequest request)
        {
            AssertDniFormatValido(request);

            if (await _pacienteRepository.PacienteExistsByDniAsync(request.DNI))
                throw new Exception($"El DNI {request.DNI} ya existe.");
        }

        private static void AssertDniFormatValido(PacienteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DNI))
                throw new Exception("El DNI es obligatorio.");
        }

        private void AssertObraSocialValida(PacienteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DNI))
                throw new Exception("El DNI es obligatorio.");
        }

        private void AssertTelefonoValido(PacienteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DNI))
                throw new Exception("El DNI es obligatorio.");
        }

        private async Task AssertExistePacienteById(int id)
        {
            if (!await _pacienteRepository.PacienteExistsByIdAsync(id))
                throw new Exception("El paciente no existe.");
        }

        private async Task AssertDniUpdateValido(UpdatePacienteRequest request)
        {
            AssertDniFormatValido(request);

            if ((await _pacienteRepository.PacienteExistsByDniAsync(request.DNI) && (await _pacienteRepository.GetPacienteByDniAsync(request.DNI)).Id != request.Id))
                throw new Exception($"El DNI {request.DNI} ya existe.");
        }
        #endregion
    }
}
