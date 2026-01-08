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

        public async Task<List<Estudiante>> ObtenerTodosAsync()
        {
            return await _db.Estudiantes.ToListAsync();
        }

        public async Task AgregarAsync(Estudiante estudiante)
        {
            _db.Estudiantes.Add(estudiante);
            await _db.SaveChangesAsync();
        }

    }
}
