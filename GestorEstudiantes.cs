using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorEstudiantesLinq;

namespace GestorEstudiantesLinq
{
    internal class GestorEstudiantes
    {

        public List<Estudiante> LeerEstudiantesDesdeConsola()
        {
            List<Estudiante> estudiantes = new List<Estudiante>();
            int id = 1;

            while (true)
            {
                Console.WriteLine($"\nEstudiante #{id}");

                //los ciclos en general, o permiten salir hasta que se entregue lo deseado
                // Nombre
                string nombre;
                do
                {
                    Console.Write("Ingrese el nombre: ");
                    nombre = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(nombre))
                        Console.WriteLine("❌ El nombre no puede estar vacío.");
                    //se repite mientras este vacío o solo con espacios
                } while (string.IsNullOrWhiteSpace(nombre));

                // Edad
                int edad;
                while (true)
                {
                    Console.Write("Ingrese la edad: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out edad) && edad > 0)
                        break;
                    Console.WriteLine("❌ Edad inválida. Ingrese un número mayor a 0.");
                }

                // Carrera
                string carrera;
                do
                {
                    Console.Write("Ingrese la carrera: ");
                    carrera = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(carrera))
                        Console.WriteLine("❌ La carrera no puede estar vacía.");
                } while (string.IsNullOrWhiteSpace(carrera));

                // Crear objeto y agregar a la lista
                estudiantes.Add(new Estudiante
                {
                    Id = id++,
                    Nombre = nombre,
                    Edad = edad,
                    Carrera = carrera
                });

                // ¿Desea continuar?
                string respuesta = "";
                while(respuesta != "s" && respuesta != "n")
                {
                    //si es "s" vuelve a repetir todo
                    //si es "n" sale de este método
                    //si es diferente "s", "n", se repite este ciclo
                    Console.Write("¿Desea agregar otro estudiante? (s/n): ");
                    respuesta = Console.ReadLine().Trim().ToLower();
                }
                if (respuesta == "n")
                    //termina este método
                    break;
            }
            return estudiantes;
        }


        public void Where_Linq(List<Estudiante> estudiantes)
        {
            // 1. Filtrar estudiantes de Ingeniería, sintaxis métodos, Where
            var ingenieria = estudiantes.Where(e => e.Carrera == "Ingeniería");
            Console.WriteLine("\n1. Estudiantes de ingeniería:");
            int apoyo = 1;
            foreach (var unaFila in ingenieria)
            {
                Console.WriteLine($"{apoyo++}.- {unaFila.Nombre}");
            }
        }

        public void OrderByDescending_Linq(List<Estudiante> estudiantes)
        {
            // 2. Ordenar por edad descendente
            var ordenados = estudiantes.OrderByDescending(e => e.Edad);

            Console.WriteLine("\n2. Estudiantes ordenados por edad:");
            foreach (var e in ordenados)
            {
                Console.WriteLine($"-{e.Nombre}, carrera: {e.Carrera}, {e.Edad} años");
            }
        }

        public void Select_Linq(List<Estudiante> estudiantes)
        {
            // 3. Proyección: solo nombres. Select
            var nombres = estudiantes.Select(e => e.Nombre);

            Console.WriteLine("\n3. Nombres de todos los estudiantes:");
            foreach (var nombre in nombres)
            {
                Console.WriteLine($"-{nombre}");
            }
        }

        public void GroupBy_Linq(List<Estudiante> estudiantes)
        {
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
        }

        public void Any_Linq(List<Estudiante> estudiantes)
        {
            // 5. Algún estudiante mayor a 20 años, Any
            string resultado = estudiantes.Any(e => e.Edad > 20) ? "Sí existe mínimo un estudiante mayor de 20 años" : "No hay ningún estudiante mayor de 20 años";

            Console.WriteLine("\n5. Estudiantes mayores de 20 años.");
            Console.WriteLine(resultado);
        }

        public void First_Linq(List<Estudiante> estudiantes)
        {
            // 6. Obtener el estudiante más joven, OrderBy - First()
            var masJoven = estudiantes
                .OrderBy(e => e.Edad)
                .First();
            Console.WriteLine("\n6. El estudiante más joven es:");
            Console.WriteLine($"{masJoven.Nombre} con {masJoven.Edad} años.");
        }

        public void Average_Linq(List<Estudiante> estudiantes)
        {
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
        }

        public void SelectAnonimos_Linq(List<Estudiante> estudiantes)
        {
            //8: Select con objetos anónimos
            var sintesis = estudiantes.Select(e => new
            {
                //se accede con el nombre personalizado "enombre"
                enombre = e.Nombre,
                //se accede mediante "la propiedad Carrera"
                e.Carrera
            });
            Console.WriteLine("\n8. Seleccion con objetos anónimos:");
            int apoyo = 1;
            foreach (var item in sintesis)
            {
                Console.WriteLine($"{apoyo++}.- {item.enombre}, con la carrera de {item.Carrera}");
            }
        }

        public void Resumen_Linq(List<Estudiante> estudiantes)
        {
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
                Console.WriteLine($"Nombre {item.Nombre}, carrera {item.Carrera}, es mayor de edad: {item.mayorEdad}, {item.carreraMayuscula}");

            }
        }
    }
}
