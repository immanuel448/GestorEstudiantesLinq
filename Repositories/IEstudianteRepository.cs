using GestorEstudiantesLinq.Models;
using System.Collections.Generic;

namespace GestorEstudiantesLinq.Repositories
{
    public interface IEstudianteRepository
    {
        Task<List<Estudiante>> ObtenerTodosAsync();
        Task AgregarAsync(Estudiante estudiante);
        Task ActualizarAsync(Estudiante estudiante);
        Task EliminarAsync(int id);


    }
}
