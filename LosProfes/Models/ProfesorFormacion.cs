using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosProfes.Models
{
    public class ProfesorFormacion
    {
        public int Id { get; set; }
        public int FormacionId { get; set; }
        public Formacion Formacion { get; set; }
        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

    }
}
