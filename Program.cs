using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GestorEstudiantesLinq
{
    public class AppDbContext : DbContext
    {
        /*
        *se instalan estos paquetes para hacer uso de entity y de sqlite
        *dotnet add package Microsoft.EntityFrameworkCore
        *dotnet add package Microsoft.EntityFrameworkCore.Sqlite
        *dotnet add package Microsoft.EntityFrameworkCore.Tools
        */

        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=estudiantes.db");
        }
    }

    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Carrera { get; set; }
    }

    class Program
    {
        private static GestorEstudiantes objGestor;

        static void Main(string[] args)
        {

            objGestor = new GestorEstudiantes();

            // Lista de estudiantes (fuente de datos en memoria), también se podrían seguir ingresando datos por consola, mediante el método LeerEstudiantesDesdeConsola, que regresa una lista
            List<Estudiante> estudiantes = new List<Estudiante>
            {
                new Estudiante { Id = 1, Nombre = "Ana", Edad = 20, Carrera = "Ingeniería" },
                new Estudiante { Id = 2, Nombre = "Luis", Edad = 22, Carrera = "Derecho" },
                new Estudiante { Id = 3, Nombre = "María", Edad = 19, Carrera = "Medicina" },
                new Estudiante { Id = 4, Nombre = "Pedro", Edad = 21, Carrera = "Ingeniería" },
            };

            while (true)
            {
                //esto es el menú
                Console.Clear();
                Console.WriteLine("📋 MENÚ PRINCIPAL - LINQ");
                Console.WriteLine("1. Estudiantes de ingeniería");
                Console.WriteLine("2. Ordenar por edad descendente");
                Console.WriteLine("3. Mostrar solo nombres");
                Console.WriteLine("4. Cantidad por carrera");
                Console.WriteLine("5. ¿Hay alguien mayor de 20?");
                Console.WriteLine("6. Mostrar el más joven");
                Console.WriteLine("7. Edad promedio");
                Console.WriteLine("8. Proyección con objetos anónimos");
                Console.WriteLine("9. Resumen transformado");
                Console.WriteLine("10. Guardar en un archivo JSON");
                Console.WriteLine("11. Cargar desde un archivo JSON");
                Console.WriteLine("12. Ingresar los estudientes desde consola");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                //se obtiene la respuesta
                string opcion = Console.ReadLine();

                //se gestionan las respuestas, si no se pide salir (opción 0), va a continuar ciclando
                switch (opcion)
                {
                    case "1":
                        objGestor.Where_Linq(estudiantes);
                        break;
                    case "2":
                        objGestor.OrderByDescending_Linq(estudiantes);
                        break;
                    case "3":
                        objGestor.Select_Linq(estudiantes);
                        break;
                    case "4":
                        objGestor.GroupBy_Linq(estudiantes);
                        break;
                    case "5":
                        objGestor.Any_Linq(estudiantes);
                        break;
                    case "6":
                        objGestor.First_Linq(estudiantes);
                        break;
                    case "7":
                        objGestor.Average_Linq(estudiantes);
                        break;
                    case "8":
                        objGestor.SelectAnonimos_Linq(estudiantes);
                        break;
                    case "9":
                        objGestor.Resumen_Linq(estudiantes);
                        break;
                    case "10":
                        objGestor.GuardarEstudiantesEnJson(estudiantes);
                        break;
                    case "11":
                        estudiantes = objGestor.CargarEstudiantesDesdeJson();
                        break;
                    case "12":
                        //se continúa con el id subsecuente
                        int ultimoId = estudiantes.Any() ? estudiantes.Max(e => e.Id) : 0;
                        var nuevosEstudiantes = objGestor.LeerEstudiantesDesdeConsola(ultimoId);
                        //se añaden los estudiantes ingresados desde consola a los anteriores
                        estudiantes.AddRange(nuevosEstudiantes);
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
