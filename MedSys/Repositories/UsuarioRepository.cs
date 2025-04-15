using MedSys.Data;
using MedSys.DTOs.Requests;
using MedSys.DTOs.Responses;
using MedSys.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MedSys.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponse> AddUsuarioAsync(NewUserRequest newUser)
        {
            Usuario usuario = newUser.ToUsuario();
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario.ToUserResponse();
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsuariosAsync()
        {
            return (await _context.Usuarios.ToListAsync()).Select(user => user.ToUserResponse());
        }

        public async Task<IEnumerable<UserResponse>> GetUsuariosPaginatedAsync(int page, int pageSize)
        {
            return (await _context.Usuarios
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync())
                                 .Select(user => user.ToUserResponse());
        }

        public async Task<UserResponse> GetUsuarioByIdAsync(int id)
        {
            // TODO : Tratar en caso de NULL
            return (await _context.Usuarios.FindAsync(id)).ToUserResponse();
        }

        public async Task<UserResponse> GetUsuarioByDniAsync(string dni)
        {
            // TODO : Tratar en caso de NULL
            return (await _context.Usuarios.FirstOrDefaultAsync(user => user.DNI == dni)).ToUserResponse();
        }

        public async Task<UserResponse> GetUsuarioByUserName(string userName)
        {
            // TODO : Tratar en caso de NULL
            return (await _context.Usuarios.FirstOrDefaultAsync(user => user.Username == userName)).ToUserResponse();
        }

        public async Task<bool> UsuarioExistsByIdAsync(int id)
        {
            return await _context.Usuarios.AnyAsync(usuario => usuario.Id == id);
        }

        public async Task<bool> UsuarioExistsByDniAsync(string dni)
        {
            return await _context.Usuarios.AnyAsync(usuario => usuario.DNI == dni);
        }

        public async Task<bool> UsuarioExistsByUserName(string userName)
        {
            return await _context.Usuarios.AnyAsync(usuario => usuario.Username == userName);
        }

        public async Task UpdateUsuarioAsync(UpdateUserRequest update)
        {
            Usuario? usuario = await _context.Usuarios.FindAsync(update.Id);

            update.DoUpdate(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            Usuario? usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
