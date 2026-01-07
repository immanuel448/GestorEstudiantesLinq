using GestorEstudiantesLinq.Data;
using GestorEstudiantesLinq.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GestorEstudiantesLinq.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly AppDbContext _db;

        public EstudianteRepository()
        {
            _db = new AppDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Estudiante> ObtenerTodos()
        {
            return _db.Estudiantes.ToList();
        }

        public void Agregar(Estudiante estudiante)
        {
            _db.Estudiantes.Add(estudiante);
            _db.SaveChanges();
        }

        public void AgregarVarios(List<Estudiante> estudiantes)
        {
            _db.Estudiantes.AddRange(estudiantes);
            _db.SaveChanges();
        }
    }
}
