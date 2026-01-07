using Microsoft.EntityFrameworkCore;
using GestorEstudiantesLinq.Models;

namespace GestorEstudiantesLinq.Data
{
    // Contexto de Entity Framework Core para manejar la conexión y las operaciones con SQLite
    public class AppDbContext : DbContext
    {
        /*
         * Paquetes NuGet requeridos para funcionar:
         * - Microsoft.EntityFrameworkCore
         * - Microsoft.EntityFrameworkCore.Sqlite
         * - Microsoft.EntityFrameworkCore.Tools
         */

        // Representa la tabla 'Estudiantes' en la base de datos
        public DbSet<Estudiante> Estudiantes { get; set; }

        // Configura la conexión con la base de datos SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Si el archivo 'estudiantes.db' no existe, se crea automáticamente al ejecutarse
            optionsBuilder.UseSqlite("Data Source=estudiantes.db");
        }
    }
}
