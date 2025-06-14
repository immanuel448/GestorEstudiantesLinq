using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            // 6. Obtener el estudiante más joven, OrderBy - First()
            var masJoven = estudiantes
                .OrderBy(e => e.Edad)
                .First();
            Console.WriteLine("\n6. El estudiante más joven es:");
            Console.WriteLine($"{masJoven.Nombre} con {masJoven.Edad} años.");

            // 7: Average – Calcular la edad promedio
            if (estudiantes.Any())
                //se realiza una verificaciòn para que la colección no esté vacía
            {
                var promedio = estudiantes.Average(e => e.Edad);
                Console.WriteLine("\n7. La edad promedio es de:");
                Console.WriteLine($"{promedio} años");
            }
            else
            {
                Console.WriteLine("No hay estudiantes para realizar el cálculo");
            }

            //8: Select con objetos anónimos
            var sintesis = estudiantes.Select(e => new
            {
                //se accede con el nombre personalizado "enombre"
                enombre = e.Nombre,
                //se accede mediante "la propiedad Carrera"
                e.Carrera
            });
            Console.WriteLine("\n8. Seleccion con objetos anónimos:");
            apoyo = 1;
            foreach (var item in sintesis)
            {
                Console.WriteLine($"{apoyo++}.- {item.enombre}, con la carrera de {item.Carrera}");
            }

            /*
                RESUMEN, hasta ahora
                Ahora queremos crear una nueva proyección con esta información:
                Nombre
                Edad
                ¿Es mayor de edad? (Edad >= 18
                Carrera en mayúsculas
            */

            Console.WriteLine("\nRESUMEN:");
            var resultados = estudiantes.Select(e => new
            {
                e.Nombre,
                e.Carrera,
                mayorEdad = e.Edad >= 18,
                carreraMayuscula = e.Carrera.ToUpper()
            });

            foreach (var item in resultados)
            {
                Console.WriteLine($"Nombre {item.Nombre}, carrera {item.Carrera}, es mayor de edad? {item.mayorEdad}, {item.carreraMayuscula}");

            }


            Console.ReadKey();
        }
    }
}
 