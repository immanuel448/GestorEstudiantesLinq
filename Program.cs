using System;
using System.Collections.Generic;
//using System.Linq;

namespace GestorEstudiantesLinq
{
    class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Carrera { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Lista de estudiantes (fuente de datos en memoria)
            List<Estudiante> estudiantes = new List<Estudiante>
            {
                new Estudiante { Id = 1, Nombre = "Ana", Edad = 20, Carrera = "Ingeniería" },
                new Estudiante { Id = 2, Nombre = "Luis", Edad = 22, Carrera = "Derecho" },
                new Estudiante { Id = 3, Nombre = "María", Edad = 19, Carrera = "Medicina" },
                new Estudiante { Id = 4, Nombre = "Pedro", Edad = 21, Carrera = "Ingeniería" },
            };

            // 1. Filtrar estudiantes de Ingeniería
            var ingenieria = estudiantes.Where(e => e.Carrera == "Ingeniería");

            Console.WriteLine("Estudiantes de Ingeniería:");
            foreach (var e in ingenieria)
            {
                Console.WriteLine($"- {e.Nombre}, {e.Edad} años");
            }

            // 2. Ordenar por edad descendente
            var ordenados = estudiantes.OrderByDescending(e => e.Edad);

            Console.WriteLine("\nEstudiantes ordenados por edad:");
            foreach (var e in ordenados)
            {
                Console.WriteLine($"- {e.Nombre}, {e.Edad} años");
            }

            // 3. Proyección: solo nombres
            var nombres = estudiantes.Select(e => e.Nombre);

            Console.WriteLine("\nNombres de todos los estudiantes:");
            foreach (var nombre in nombres)
            {
                Console.WriteLine($"- {nombre}");
            }
        }
    }
}
