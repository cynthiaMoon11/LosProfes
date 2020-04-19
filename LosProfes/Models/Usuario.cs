using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LosProfes.Models
{
    public class Usuario: IdentityUser
    {
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }        

        public Profesor Profesor { get; set; }
        public List<Estudiante> Estudiantes { get; set; }

    }
}
