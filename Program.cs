using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Channels;

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

            // 1. Filtrar estudiantes de Ingeniería, sintaxis métodos, Where
            var ingenieria = estudiantes.Where(e => e.Carrera == "Ingeniería");
            Console.WriteLine("\n1. Estudiantes de ingeniería:");
            int apoyo = 1;
            foreach (var unaFila in ingenieria)
            {
                Console.WriteLine($"{apoyo++}.- {unaFila.Nombre}");
            }


            // 2. Ordenar por edad descendente
            var ordenados = estudiantes.OrderByDescending(e => e.Edad);

            Console.WriteLine("\n2. Estudiantes ordenados por edad:");
            foreach (var e in ordenados)
            {
                Console.WriteLine($"-{e.Nombre}, carrera: {e.Carrera}, {e.Edad} años");
            }

            // 3. Proyección: solo nombres. Select
            var nombres = estudiantes.Select(e => e.Nombre);

            Console.WriteLine("\n3. Nombres de todos los estudiantes:");
            foreach (var nombre in nombres)
            {
                Console.WriteLine($"-{nombre}");
            }

            // 4. Contar estudiantes por carrera, GroupBy - Select
            var conteoPorCarrera = estudiantes
                .GroupBy(e => e.Carrera)
                .Select(grupo => new
                {
                    carrera = grupo.Key,
                    cantidad = grupo.Count()
                });
            Console.WriteLine("\n4. Cantidad de estudiantes por Carrera:");
            foreach (var e in conteoPorCarrera)
            {
                Console.WriteLine($"La carrera de {e.carrera} tiene {e.cantidad} estudiante(s).");
            }

            // 5. Algún estudiante mayor a 20 años, Any
            string resultado = estudiantes.Any(e => e.Edad > 20) ? "Sí existe mínimo un estudiante mayor de 20 años" : "No hay ningún estudiante mayor de 20 años";

            Console.WriteLine("\n5. Estudiantes mayores de 20 años.");
            Console.WriteLine(resultado);


            Console.ReadKey();
        }
    }
}
