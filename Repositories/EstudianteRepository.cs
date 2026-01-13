using GestorEstudiantesLinq.Data;
using GestorEstudiantesLinq.Helpers;
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

        public async Task ActualizarAsync(Estudiante estudiante)
        {
            _db.Estudiantes.Update(estudiante);
            await _db.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            // Buscar el estudiante por ID
            var est = await _db.Estudiantes.FindAsync(id);
            // Si se encuentra, eliminarlo
            if (est != null)
            {
                _db.Estudiantes.Remove(est);
                await _db.SaveChangesAsync();
            }
        }

    }
}
