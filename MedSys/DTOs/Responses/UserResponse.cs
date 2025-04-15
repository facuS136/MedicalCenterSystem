using MedSys.Models;

namespace MedSys.DTOs.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Username { get; set; }
        public string Rol { get; set; }
        public UserResponse(Usuario usuario)
        {
            Id = usuario.Id;
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            DNI = usuario.DNI;
            Username = usuario.Username;
            Rol = usuario.Rol;
        }
    }
}
