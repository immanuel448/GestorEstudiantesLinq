using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GestorEstudiantesLinq;
using Microsoft.EntityFrameworkCore;

namespace GestorEstudiantesLinq
{
    internal class GestorEstudiantes
    {
        // Guarda una lista de estudiantes en la base de datos usando EF Core
        public void GuardarEstudiantesEnBD(List<Estudiante> estudiantes)
        {
            try
            {
                using var db = new AppDbContext();
                db.Database.EnsureCreated();
                db.Estudiantes.AddRange(estudiantes);
                db.SaveChanges();
                Console.WriteLine("✅ Estudiantes guardados correctamente en la base de datos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al guardar en la base de datos: {ex.Message}");
            }
        }

        //Guarda estudiantes en un archivo externo del tipo JSON
        public void ExportarEstudiantesAJson(List<Estudiante> estudiantes)
        {
            string ruta = "guardarEstudiantes.json";

            try
            {
                //se convierte a JSON; el segundo parámetro sirve para que el JSON se genere con formato legible (sangría)
                string json = JsonSerializer.Serialize(estudiantes, new JsonSerializerOptions { WriteIndented = true });
                //escribir o crear y escribir en un archivo
                File.WriteAllText(ruta, json);
                Console.WriteLine($"✅ Estudiantes exportados correctamente en '{ruta}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al guardar: {ex.Message}");
            }
        }
        
        public List<Estudiante> LeerEstudiantesDesdeConsola(int ultimoId)
        {
            List<Estudiante> estudiantes = new List<Estudiante>();
            //se parte desde el último id existente
            int id = ultimoId + 1;

            while (true)
            {
                Console.WriteLine($"\nEstudiante #{id}");

                //los ciclos en general no permiten salir, mientras no se entregue lo deseado
                // Nombre: no puede estar vacío
                string nombre;
                do
                {
                    Console.Write("Ingrese el nombre: ");
                    nombre = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(nombre))
                        Console.WriteLine("❌ El nombre no puede estar vacío.");
                    //se repite mientras esté vacío o solo contenga espacios
                } while (string.IsNullOrWhiteSpace(nombre));

                // Edad : debe ser un número mayor a cero
                int edad;
                while (true)
                {
                    Console.Write("Ingrese la edad: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out edad) && edad > 0)
                        break;//sale cuando obtengo lo deseado
                    Console.WriteLine("❌ Edad inválida. Ingrese un número mayor a 0.");
                }

                // Carrera: no puede estar vacía
                string carrera;
                do
                {
                    Console.Write("Ingrese la carrera: ");
                    carrera = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(carrera))
                        Console.WriteLine("❌ La carrera no puede estar vacía.");
                } while (string.IsNullOrWhiteSpace(carrera));

                // se agrega a la lista -----------------------------
                estudiantes.Add(new Estudiante
                {
                    Id = id++,
                    Nombre = nombre,
                    Edad = edad,
                    Carrera = carrera
                });

                // ¿Desea continuar? (ingresar más estudiantes)-----------------------------
                string respuesta = "";
                while (respuesta != "s" && respuesta != "n")
                {
                    //si es "s" vuelve a repetir todo
                    //si es "n" sale de este método
                    //si es diferente "s", "n", se repite este ciclo
                    Console.Write("\n¿Desea agregar otro estudiante? (s/n): ");
                    respuesta = Console.ReadLine().Trim().ToLower();
                }
                if (respuesta == "n")
                    //termina este método "LeerEstudiantesDesdeConsola"
                    break;
            }
            return estudiantes;
        }

        // Verifica si la lista tiene estudiantes, se usa activamente en el Menú
        public bool HayEstudiantes(List<Estudiante> lista)
        {
            if (!lista.Any())//lista vacía
            {
                Console.WriteLine("⚠️ No hay estudiantes cargados. Ingrese datos primero (opción 11).");
                return false;
            }
            return true;
        }

        // 1. Where: filtra por carrera
        //MÉTODOS DE LINQ ---------------------------
        public void Where_Linq(List<Estudiante> estudiantes)
        {
            Console.Write("🔍 Ingrese la carrera a filtrar: ");
            string carrera = Console.ReadLine()?.Trim();

            var resultado = estudiantes
                .Where(e => e.Carrera.Equals(carrera, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (resultado.Any())
            {
                Console.WriteLine($"\n🎓 Estudiantes de la carrera '{carrera}':");
                foreach (var e in resultado)
                    Console.WriteLine($"- {e.Nombre} ({e.Edad} años)");
            }
            else
            {
                Console.WriteLine($"❌ No se encontraron estudiantes de la carrera '{carrera}'");
            }
        }
        
        // 2. OrderBy/OrderByDescending: ordenar por edad
        public void Order_Linq(List<Estudiante> estudiantes)
        {
            var resultado = "";

            do
            {
                Console.WriteLine("\nSELECCIONE PARA ORDENAR EN BASE A LA EDAD:");
                Console.WriteLine("(1) Para Descendente");
                Console.WriteLine("(2) Para Ascendente");
                resultado = Console.ReadLine();

            } while (resultado != "1" && resultado != "2");

            IEnumerable<Estudiante> ordenados; // Tipo explícito aquí

            if (resultado == "1")
            {
                //(1) descendente
                ordenados = estudiantes.OrderByDescending(e => e.Edad);
                Console.WriteLine("\n2. Estudiantes ordenados por edad (descendente):");
            }else
            {
                //ascendente
                ordenados = estudiantes.OrderBy(e => e.Edad);
                Console.WriteLine("\n2. Estudiantes ordenados por edad (ascendente):");
            }

            foreach (var e in ordenados)
            {
                Console.WriteLine($"-{e.Nombre}, carrera: {e.Carrera}, {e.Edad} años");
            }
        }
        
        // 3. Select: mostrar solo los nombres
        public void Select_Linq(List<Estudiante> estudiantes)
        {
            var nombres = estudiantes.Select(e => e.Nombre);

            Console.WriteLine("\n3. Nombres de todos los estudiantes:");
            foreach (var nombre in nombres)
            {
                Console.WriteLine($"-{nombre}");
            }
        }

        // 4. GroupBy + Count: cantidad de estudiantes por carrera
        public void GroupBy_Linq(List<Estudiante> estudiantes)
        {
            var conteoPorCarrera = estudiantes
                .GroupBy(e => e.Carrera);
            Console.WriteLine("\n4. Cantidad de estudiantes por Carrera:");
            foreach (var grupo in conteoPorCarrera)

            {
                Console.WriteLine($"La carrera de {grupo.Key} tiene {grupo.Count()} estudiante(s).");
            }
        }

        // 5. Any: verificar si hay estudiantes mayores de cierta edad
        public void Any_Linq(List<Estudiante> estudiantes)
        {
            Console.Write("\n🔍 Ingrese la edad mínima a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int edadMinima))
            {
                bool hay = estudiantes.Any(e => e.Edad > edadMinima);
                string mensaje = hay
                    ? $"✅ Sí existe al menos un estudiante mayor de {edadMinima} años."
                    : $"❌ No hay estudiantes mayores de {edadMinima} años.";
                Console.WriteLine(mensaje);
            }
            else
            {
                Console.WriteLine("❌ Edad inválida.");
            }
        }

        // 6. OrderBy + FirstOrDefault: encontrar al estudiante más joven
        public void First_Linq(List<Estudiante> estudiantes)
        {
            var masJoven = estudiantes.OrderBy(e => e.Edad).FirstOrDefault();
            if (masJoven != null)
            {
                Console.WriteLine($"{masJoven.Nombre} con {masJoven.Edad} años.");
            }
            else
            {
                Console.WriteLine("⚠️ No hay estudiantes cargados.");
            }
        }

        // 7. Average: calcular edad promedio
        public void Average_Linq(List<Estudiante> estudiantes)
        {
            var promedio = estudiantes.Average(e => e.Edad);
            Console.WriteLine("\n7. La edad promedio es de:");
            Console.WriteLine($"{promedio} años");
        }

        // 8. Select con objetos anónimos: nombre + carrera
        public void SelectAnonimos_Linq(List<Estudiante> estudiantes)
        {
            var sintesis = estudiantes.Select(e => new
            {
                //se accede con el nombre personalizado "enombre"
                nombrePersonalizado = e.Nombre,
                //se accede mediante "la propiedad Carrera"
                e.Carrera
            });
            Console.WriteLine("\n8. Seleccion con objetos anónimos (nombre, carrera):");
            int apoyo = 1;
            foreach (var item in sintesis)
            {
                Console.WriteLine($"{apoyo++}.- {item.nombrePersonalizado}, con la carrera de {item.Carrera}");
            }
        }

        // 9. Select personalizado: resumen extendido por estudiante
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
