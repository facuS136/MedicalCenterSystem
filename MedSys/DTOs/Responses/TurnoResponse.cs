using MedSys.Models;

namespace MedSys.DTOs.Responses
{
    public class TurnoResponse
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdDoctor { get; set; }
        public int IdPaciente { get; set; }
        public DateOnly Dia { get; set; }
        public TimeSpan Hora { get; set; }

        public TurnoResponse(Turno turno)
        {
            Id = turno.Id;
            IdUser = turno.IdUser;
            IdDoctor = turno.IdDoctor;
            IdPaciente = turno.IdPaciente;
            Dia = turno.Dia;
            Hora = turno.Hora;

        }
    }
}
