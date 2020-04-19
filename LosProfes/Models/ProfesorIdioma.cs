using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosProfes.Models
{
    public class ProfesorIdioma
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }
        public int IdiomaId { get; set; }
        public Idioma Idioma { get; set; }

    }
}
