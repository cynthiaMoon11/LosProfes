using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosProfes.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ProfesorMateria> ProfesoreMaterias { get; set; }
    }
}
