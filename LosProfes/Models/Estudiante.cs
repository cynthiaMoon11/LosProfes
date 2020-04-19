using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LosProfes.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Display(Name = "Nombre del estudiante")]
        public string NombreEstudiante { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        public int ColegioId { get; set; }
        public Colegio Colegio { get; set; }
        public int IdiomaId { get; set; }
        public Idioma Idioma { get; set; }
        public int GradoId { get; set; }
        public Grado Grado { get; set; }

        public List<EstudianteMateria> EstudiantesMaterias { get; set; }
        public List<Clase> Clases { get; set; }
    }
}
