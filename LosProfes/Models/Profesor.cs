using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosProfes.Models
{
    public class Profesor 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Imagen { get; set; }
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Clase> Clases { get; set; }
        public List<ProfesorFormacion> ProfesoresFormaciones { get; set; }
        public List<ProfesorIdioma> ProfesoresIdiomas { get; set; }
        public List<ProfesorMateria> ProfesoresMaterias { get; set; }
        



    }
}
