using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosProfes.Models
{
    public class Idioma
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ProfesorIdioma> ProfesoresIdiomas { get; set; }
        public List<Estudiante> Estudiantes { get; set; }

    }
}
