using MedSys.Models;
using System.Security.Cryptography;

namespace MedSys.DTOs.Requests
{
    public class TurnoRequest
    {
        public int? IdUser { get; set; }
        public int? IdDoctor { get; set; }
        public int? IdPaciente { get; set; }
        public DateOnly? Dia { get; set; }
        public TimeSpan? Hora { get; set; }
    }

    public class NewTurnoRequest : TurnoRequest
    {
        public Turno ToTurno()
        {
            return new Turno
            {
                IdUser = (int)this.IdUser,
                IdDoctor = (int)this.IdDoctor,
                IdPaciente = (int)this.IdPaciente,
                Dia = (DateOnly)this.Dia,
                Hora = (TimeSpan)this.Hora
            };
        }
    }

    public class GetTurnoRequest : TurnoRequest
    {
        public bool Coincide(Turno turno)
        {
            bool coincide = true;

            if (IdUser.HasValue)
            {
                coincide = coincide && turno.IdUser == this.IdUser;
            }

            if (IdDoctor.HasValue)
            {
                coincide = coincide && turno.IdDoctor == this.IdDoctor;
            }

            if (IdPaciente.HasValue)
            {
                coincide = coincide && turno.IdPaciente == this.IdPaciente;
            }

            if (Dia.HasValue)
            {
                coincide = coincide && turno.Dia == this.Dia;
            }

            if (Hora.HasValue)
            {
                coincide = coincide && turno.Hora == this.Hora;
            }

            return coincide;
        }
    }

    public class UpdateTurnoRequest : TurnoRequest
    {
        public int Id { get; set; }

        public void DoUpdate(Turno? turno)
        {
            turno.IdUser = (int)this.IdUser;
            turno.IdDoctor = (int)this.IdDoctor;
            turno.IdPaciente = (int)this.IdPaciente;
            turno.Dia = (DateOnly)this.Dia;
            turno.Hora = (TimeSpan)this.Hora;
        }
    }
}
