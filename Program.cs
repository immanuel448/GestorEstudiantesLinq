using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;



namespace GestorEstudiantesLinq
{
    //dotnet nuget list source
    //dotnet nuget remove source packagesource1q

    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Carrera { get; set; }
    }

    public class AppDbContext : DbContext
    {
        /*
         * Requiere los siguientes paquetes para funcionar con Entity Framework Core y SQLite:
         * dotnet add package Microsoft.EntityFrameworkCore
         * dotnet add package Microsoft.EntityFrameworkCore.Sqlite
         * dotnet add package Microsoft.EntityFrameworkCore.Tools
         */

        // Representa la tabla Estudiantes en la base de datos
        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configura la conexión a la base de datos SQLite.
            // Si el archivo 'estudiantes.db' no existe, se creará automáticamente.
            optionsBuilder.UseSqlite("Data Source=estudiantes.db");
        }
    }

    class Program
    {
        //para usar los métodos de esta clase
        private static GestorEstudiantes objGestor;

        static void Main(string[] args)
        {
            objGestor = new GestorEstudiantes();

            // ya se toman los datos desde la BD
            using var db = new AppDbContext();
            List<Estudiante> estudiantes = db.Estudiantes.ToList();


            while (true)
            {
                //esto es el menú
                Console.Clear();
                Console.WriteLine("📋 MENÚ PRINCIPAL - CONSULTAS LINQ");
                Console.WriteLine("1. Filtrar por carrera");
                Console.WriteLine("2. Ordenar por edad");
                Console.WriteLine("3. Mostrar nombres de estudiantes");
                Console.WriteLine("4. Cantidad de estudiantes por carrera");
                Console.WriteLine("5. ¿Hay algún estudiante mayor de cierta edad?");
                Console.WriteLine("6. Estudiante más joven");
                Console.WriteLine("7. Edad promedio");
                Console.WriteLine("8. Proyección resumida (nombre, carrera)");
                Console.WriteLine("9. Resumen personalizado");
                Console.WriteLine("10. Exportar estudiantes a JSON");
                Console.WriteLine("11. Agregar estudiantes desde consola");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                //se obtiene la respuesta
                string opcion = Console.ReadLine();

                //se gestionan las respuestas, si no se pide salir (opción 0), va a continuar ciclando
                switch (opcion)
                {
                    case "1":
                        if(objGestor.HayEstudiantes(estudiantes))
                            objGestor.Where_Linq(estudiantes);
                        break;
                    case "2":
                        if (objGestor.HayEstudiantes(estudiantes))
                            objGestor.Order_Linq(estudiantes);
                        break;
                    case "3":
                        if (objGestor.HayEstudiantes(estudiantes))
                            objGestor.Select_Linq(estudiantes);
                        break;
                    case "4":
                        if (objGestor.HayEstudiantes(estudiantes))
                            objGestor.GroupBy_Linq(estudiantes);
                        break;
                    case "5":
                        if (objGestor.HayEstudiantes(estudiantes))
                            objGestor.Any_Linq(estudiantes);
                        break;
                    case "6":
                        if (objGestor.HayEstudiantes(estudiantes))
                            objGestor.First_Linq(estudiantes);
                        break;
                    case "7":
                        if (objGestor.HayEstudiantes(estudiantes))
                            objGestor.Average_Linq(estudiantes);
                        break;
                    case "8":
                        if (objGestor.HayEstudiantes(estudiantes))
                            objGestor.SelectAnonimos_Linq(estudiantes);
                        break;
                    case "9":
                        if (objGestor.HayEstudiantes(estudiantes))
                            objGestor.Resumen_Linq(estudiantes);
                        break;
                    case "10":
                        if (objGestor.HayEstudiantes(estudiantes))
                            objGestor.ExportarEstudiantesAJson(estudiantes);
                        break;
                    case "11":
                        //se ingresan estudiantes desde la consola, se continúa con el id subsecuente
                        int ultimoId = estudiantes.Any() ? estudiantes.Max(e => e.Id) : 0;
                        var nuevosEstudiantes = objGestor.LeerEstudiantesDesdeConsola(ultimoId);
                        //se añaden los estudiantes ingresados desde consola a los anteriores
                        estudiantes.AddRange(nuevosEstudiantes);//no sobreescribe sino que los añade
                        objGestor.GuardarEstudiantesEnBD(nuevosEstudiantes);
                        break;
                    case "0":
                        Console.WriteLine("👋 Saliendo...");
                        //termina el método Main -------------
                        return;
                    default:
                        Console.WriteLine("❌ Opción no válida. Intente de nuevo.");
                        break;
                }
                //en este punto va a volver a ciclar
                Console.WriteLine("\nPresione una tecla para volver al Menú");
                Console.ReadKey();
            }
        }
    }
}
