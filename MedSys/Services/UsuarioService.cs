using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Models;
using MedSys.Repositories;
using System.Security.Cryptography;

namespace MedSys.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UserResponse> CrearUsuarioAsync(NewUserRequest request)
        {
            // Validación básica de datos (puede complementarse con DataAnnotations en el modelo)
            AssertNotNull(request);
            AssertNombreApellidoValidos(request);
            await AssertDniValido(request);
            await AssertUserNameValido(request);
            AssertClaveValida(request.Clave);
            AssertRolValido(request);

            request.Clave = EncriptarClave(request.Clave);

            return (await _usuarioRepository.AddUsuarioAsync(request));
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsuariosAsync()
        {
            return await _usuarioRepository.GetAllUsuariosAsync();
        }

        public async Task<IEnumerable<UserResponse>> GetUsuariosPaginatedAsync(int page, int pageSize)
        {
            return await _usuarioRepository.GetUsuariosPaginatedAsync(page, pageSize);
        }

        public async Task<UserResponse> GetUsuarioByIdAsync(int id)
        {
            await AssertExisteUsuarioById(id);
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            return usuario;
        }

        public async Task UpdateUsuarioAsync(UpdateUserRequest request)
        {
            await AssertExisteUsuarioById(request.Id);
            AssertNotNull(request);
            AssertNombreApellidoValidos(request);
            await AssertDniUpdateValido(request);
            await AssertUserNameUpdateValido(request);
            AssertRolValido(request);

            await _usuarioRepository.UpdateUsuarioAsync(request);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            // Verifica la existencia antes de eliminar
            await AssertExisteUsuarioById(id);

            await _usuarioRepository.DeleteUsuarioAsync(id);
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
        private static void AssertRolValido(UserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Rol))
                throw new Exception("El rol es obligatorio.");

            if (request.Rol != "ADMIN" && request.Rol != "USER")
                throw new Exception("El rol del usuario solo pueden ser 'ADMIN' o 'USER'.");
        }

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

        private async Task AssertUserNameValido(UserRequest request)
        {
            AssertUserNameFormatValido(request);

            if (await _usuarioRepository.UsuarioExistsByUserName(request.Username))
                throw new Exception($"El nombre de usuario '{request.Username}' ya existe.");
        }

        private async Task AssertUserNameUpdateValido(UpdateUserRequest request)
        {
            AssertUserNameFormatValido(request);

            if (await _usuarioRepository.UsuarioExistsByUserName(request.Username) && (await _usuarioRepository.GetUsuarioByUserName(request.Username)).Id != request.Id)
                throw new Exception($"El nombre de usuario '{request.Username}' ya existe.");
        }

        private static void AssertUserNameFormatValido(UserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new Exception("El nombre de usuario no puede estar vacío.");

            if (request.Username.Contains(" "))
                throw new Exception("El nombre de usuario no debe contener espacios.");

            if (!Regex.IsMatch(request.Username, @"^[a-zA-Z0-9]+$"))
                throw new Exception("El nombre de usuario debe contener solamente caracteres alfanuméricos.");
        }

        private async Task AssertDniValido(UserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DNI))
                throw new Exception("El DNI es obligatorio.");

            if (await _usuarioRepository.UsuarioExistsByDniAsync(request.DNI))
                throw new Exception($"El DNI {request.DNI} ya existe.");
        }

        private async Task AssertDniUpdateValido(UpdateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.DNI))
                throw new Exception("El DNI es obligatorio.");

            if ((await _usuarioRepository.UsuarioExistsByDniAsync(request.DNI) && (await _usuarioRepository.GetUsuarioByDniAsync(request.DNI)).Id != request.Id))
                throw new Exception($"El DNI {request.DNI} ya existe.");
        }

        private static void AssertNombreApellidoValidos(UserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                throw new Exception("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.Apellido))
                throw new Exception("El apellido es obligatorio.");
        }

        private static void AssertNotNull(UserRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "El objeto request no puede ser nulo.");
        }

        private async Task AssertExisteUsuarioById(int id)
        {
            if (!await _usuarioRepository.UsuarioExistsByIdAsync(id))
                throw new Exception("El usuario no existe.");
        }
        #endregion
    }
}
