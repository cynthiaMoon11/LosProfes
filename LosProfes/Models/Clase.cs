using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LosProfes.Models
{
    public class Clase
    {
        public int Id { get; set; }

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Clase")]
        public DateTime FechaClase { get; set; }

    }
}
