using System;
using System.Collections.Generic;
using System.Text;
using LosProfes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LosProfes.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LosProfes.Models.Estudiante> Estudiante { get; set; }
        public DbSet<LosProfes.Models.Profesor> Profesor { get; set; }
        public DbSet<LosProfes.Models.Clase> Clase { get; set; }
        public DbSet<LosProfes.Models.Colegio> Colegio { get; set; }
        public DbSet<LosProfes.Models.EstudianteMateria> EstudianteMateria { get; set; }
        public DbSet<LosProfes.Models.Formacion> Formacion { get; set; }
        public DbSet<LosProfes.Models.Grado> Grado { get; set; }
        public DbSet<LosProfes.Models.Idioma> Idioma { get; set; }
        public DbSet<LosProfes.Models.Materia> Materia { get; set; }
        public DbSet<LosProfes.Models.Precio> Precio { get; set; }
        public DbSet<LosProfes.Models.ProfesorFormacion> ProfesorFormacion { get; set; }
        public DbSet<LosProfes.Models.ProfesorIdioma> ProfesorIdioma { get; set; }
        public DbSet<LosProfes.Models.ProfesorMateria> ProfesorMateria { get; set; }
        public DbSet<LosProfes.Models.Genero> Genero { get; set; }
    }
}
