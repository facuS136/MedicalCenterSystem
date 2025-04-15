using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedSys.Models
{
    public class HistorialMedico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Id del paciente es obligatorio.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "El Id del doctor es obligatorio.")]
        public int IdDoctor { get; set; }

        [Required(ErrorMessage = "El diagnóstico es obligatorio.")]
        [StringLength(500)]
        public required string Diagnostico { get; set; }

        [Required(ErrorMessage = "El tratamiento es obligatorio.")]
        [StringLength(500)]
        public required string Tratamiento { get; set; }

        [Required(ErrorMessage = "La fecha de la consulta es obligatoria.")]
        public required DateTime Fecha { get; set; }

        // Opcional: observaciones adicionales
        [StringLength(1000)]
        public required string Observaciones { get; set; }
    }
}
