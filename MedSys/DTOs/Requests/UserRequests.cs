using MedSys.Models;

namespace MedSys.DTOs.Requests
{
    public class UserRequest
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? DNI { get; set; }
        public string? Username { get; set; }
        public string? Rol { get; set; }
    }

    public class NewUserRequest : UserRequest
    {
        public string? Clave { get; set; }
        public Usuario ToUsuario()
        {
            return new Usuario
            {
                // Puede ser NULL
                Nombre = this.Nombre,
                Apellido = this.Apellido,
                DNI = this.DNI,
                Username = this.Username,
                Clave = this.Clave,
                Rol = this.Rol
            };
        }
    }

    public class UpdateUserRequest : UserRequest
    {
        public int Id { get; set; }

        internal void DoUpdate(Usuario usuario)
        {
            usuario.Nombre = this.Nombre;
            usuario.Apellido = this.Apellido;
            usuario.DNI = this.DNI;
            usuario.Username = this.Username;
            usuario.Rol = this.Rol;
        }
    }

}
