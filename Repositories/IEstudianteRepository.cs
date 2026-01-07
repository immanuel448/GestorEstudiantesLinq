using GestorEstudiantesLinq.Models;
using System.Collections.Generic;

namespace GestorEstudiantesLinq.Repositories
{
    public interface IEstudianteRepository
    {
        List<Estudiante> ObtenerTodos();
        void Agregar(Estudiante estudiante);
        void AgregarVarios(List<Estudiante> estudiantes);
    }
}
