using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosProfes.Models
{
    public class Grado
    {
        public int Id { get; set; }
        public string Anio { get; set; }
        public int PrecioId { get; set; }
        public Precio Precio { get; set; }
    }
}
