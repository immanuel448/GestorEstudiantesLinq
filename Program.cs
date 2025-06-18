using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GestorEstudiantesLinq
{
    public class AppDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "estudiantes.db");
                optionsBuilder.UseSqlite($"Data Source={path}");
            }
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

            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated(); // Crea la BD si no existe

                if (!db.Estudiantes.Any())
                {
                    var estudiantesIniciales = new List<Estudiante>
                    {
                        new Estudiante { Nombre = "Ana", Edad = 20, Carrera = "Ingeniería" },
                        new Estudiante { Nombre = "Luis", Edad = 22, Carrera = "Derecho" },
                        new Estudiante { Nombre = "María", Edad = 19, Carrera = "Medicina" },
                        new Estudiante { Nombre = "Pedro", Edad = 21, Carrera = "Ingeniería" },
                    };
                    db.Estudiantes.AddRange(estudiantesIniciales);
                    db.SaveChanges();
                }
            }

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
                Console.WriteLine("13. Insertar datos de prueba en la base de datos");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                //se obtiene la respuesta
                string opcion = Console.ReadLine();

                //se gestionan las respuestas, si no se pide salir (opción 0), va a continuar ciclando
                switch (opcion)
                {
                    case "1":
                        objGestor.Where_Linq();
                        break;
                    case "2":
                        objGestor.OrderByDescending_Linq();
                        break;
                    case "3":
                        objGestor.Select_Linq();
                        break;
                    case "4":
                        objGestor.GroupBy_Linq();
                        break;
                    case "5":
                        objGestor.Any_Linq();
                        break;
                    case "6":
                        objGestor.First_Linq();
                        break;
                    case "7":
                        objGestor.Average_Linq();
                        break;
                    case "8":
                        objGestor.SelectAnonimos_Linq();
                        break;
                    case "9":
                        objGestor.Resumen_Linq();
                        break;
                    case "10":
                        objGestor.GuardarEstudiantesEnJson();
                        break;
                    case "11":
                        var estudiantesDesdeJson = objGestor.CargarEstudiantesDesdeJson();
                        using (var db = new AppDbContext())
                        {
                            db.Estudiantes.AddRange(estudiantesDesdeJson);
                            db.SaveChanges();
                        }
                        break;
                    case "12":
                        var nuevosEstudiantes = objGestor.LeerEstudiantesDesdeConsola(ultimoId: 0); // Ya no se necesita últimoId eeeee
                        using (var db = new AppDbContext())
                        {
                            db.Estudiantes.AddRange(nuevosEstudiantes);
                            db.SaveChanges();//ee error al ingresar desde consola
                        }
                        break;
                    case "13":
                        objGestor.InsertarEstudiantesEnBD();
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



