using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Repositories;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace MedSys.Services
{
    public class DoctoresService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctoresService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<DoctorResponse> CrearDoctorAsync(NewDoctorRequest request)
        {
            // Validación básica de datos (puede complementarse con DataAnnotations en el modelo)
            AssertNotNull(request);
            AssertNombreApellidoValidos(request);
            await AssertDniValido(request);
            await AssertUserNameValido(request);
            AssertClaveValida(request.Clave);
            AssertEspecilidadValida(request);

            request.Clave = EncriptarClave(request.Clave);

            return (await _doctorRepository.AddDoctorAsync(request));
        }

        public async Task<IEnumerable<DoctorResponse>> GetAllDoctoresAsync()
        {
            return await _doctorRepository.GetAllDoctoresAsync();
        }

        public async Task<IEnumerable<DoctorResponse>> GetDoctoresPaginatedAsync(int page, int pageSize)
        {
            return await _doctorRepository.GetDoctoresPaginatedAsync(page, pageSize);
        }

        public async Task<DoctorResponse> GetDoctorByIdAsync(int id)
        {
            await AssertExisteDoctorById(id);
            DoctorResponse doctor = await _doctorRepository.GetDoctorByIdAsync(id);
            return doctor;
        }

        public async Task UpdateDoctorAsync(UpdateDoctorRequest request)
        {
            await AssertExisteDoctorById(request.Id);
            AssertNotNull(request);
            AssertNombreApellidoValidos(request);
            await AssertDniUpdateValido(request);
            await AssertUserNameUpdateValido(request);
            AssertEspecilidadValida(request);

            await _doctorRepository.UpdateDoctorAsync(request);
        }

        public async Task DeleteDoctorAsync(int id)
        {
            // Verifica la existencia antes de eliminar
            await AssertExisteDoctorById(id);

            await _doctorRepository.DeleteDoctorAsync(id);
        }

        public string EncriptarClave(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convierte la contraseña en un array de bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Genera el hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convierte el hash en una cadena hexadecimal
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString();
            }
        }

        #region Asserts

        private void AssertClaveValida(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave))
                throw new Exception("La contraseña no puede estar vacía.");

            const int minLength = 8;
            const int maxLength = 20;

            if (clave.Length < minLength)
                throw new Exception($"La contraseña debe tener al menos {minLength} caracteres.");

            if (clave.Length > maxLength)
                throw new Exception($"La contraseña no debe exceder de {maxLength} caracteres.");

            // Verificar que no tenga espacios (opcional, ya que la siguiente Regex lo cubre)
            if (clave.Contains(" "))
                throw new Exception("La contraseña no debe contener espacios.");

            // Verifica que todos los caracteres estén en el rango ASCII imprimible sin espacio.
            // El rango [!-~] coincide con los caracteres ASCII del 33 (!) al 126 (~).
            if (!Regex.IsMatch(clave, @"^[!-~]+$"))
                throw new Exception("La contraseña contiene caracteres no permitidos.");
        }

        private void AssertEspecilidadValida(DoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Especialidad))
                throw new Exception("La especialidad del doctor no puede estar vacío.");
        }

        private async Task AssertUserNameValido(DoctorRequest request)
        {
            AssertUserNameFormatValido(request);

            if (await _doctorRepository.DoctorExistsByUserName(request.Username))
                throw new Exception($"El nombre del doctor '{request.Username}' ya existe.");
        }

        private async Task AssertUserNameUpdateValido(UpdateDoctorRequest request)
        {
            AssertUserNameFormatValido(request);

            if (await _doctorRepository.DoctorExistsByUserName(request.Username) && (await _doctorRepository.GetDoctorByUserName(request.Username)).Id != request.Id)
                throw new Exception($"El nombre del doctor '{request.Username}' ya existe.");
        }

        private static void AssertUserNameFormatValido(DoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new Exception("El nombre del doctor no puede estar vacío.");

            if (request.Username.Contains(" "))
                throw new Exception("El nombre del doctor no debe contener espacios.");

            if (!Regex.IsMatch(request.Username, @"^[a-zA-Z0-9]+$"))
                throw new Exception("El nombre del doctor debe contener solamente caracteres alfanuméricos.");
        }

        private async Task AssertDniValido(DoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DNI))
                throw new Exception("El DNI es obligatorio.");

            if (await _doctorRepository.DoctorExistsByDniAsync(request.DNI))
                throw new Exception($"El DNI {request.DNI} ya existe.");
        }

        private async Task AssertDniUpdateValido(UpdateDoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DNI))
                throw new Exception("El DNI es obligatorio.");

            if ((await _doctorRepository.DoctorExistsByDniAsync(request.DNI) && (await _doctorRepository.GetDoctorByDniAsync(request.DNI)).Id != request.Id))
                throw new Exception($"El DNI {request.DNI} ya existe.");
        }

        private static void AssertNombreApellidoValidos(DoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new Exception("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.Apellido))
                throw new Exception("El apellido es obligatorio.");
        }

        private static void AssertNotNull(DoctorRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "El objeto request no puede ser nulo.");
        }

        private async Task AssertExisteDoctorById(int id)
        {
            if (!await _doctorRepository.DoctorExistsByIdAsync(id))
                throw new Exception("El doctor no existe.");
        }
        #endregion
    }
}
