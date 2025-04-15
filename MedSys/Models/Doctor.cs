using MedSys.DTOs.Responses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedSys.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50)]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50)]
        public required string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [StringLength(20)]
        public required string DNI { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        [StringLength(100)]
        public required string Especialidad { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [StringLength(100)]
        public required string Clave { get; set; }

        internal DoctorResponse ToDoctorResponse()
        {
            return new DoctorResponse(this);
        }
    }
}
