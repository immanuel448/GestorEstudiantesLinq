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
        public void GuardarEstudiantesEnBD(List<Estudiante> estudiantes)
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated(); // crea la BD y la tabla si no existe (el archivo en si)
            db.Estudiantes.AddRange(estudiantes);
            db.SaveChanges();
        }

        //no se usa
        public List<Estudiante> CargarEstudiantesDesdeBD()
        {
            using var db = new AppDbContext();
            return db.Estudiantes.ToList();
        }

        //antes: GuardarEstudiantesEnJson
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
        
        //ya no se usa7
        public List<Estudiante> CargarEstudiantesDesdeJson()
        {
            string ruta = "estudiantes.json";

            try
            {
                //se asegura que el archivo exista, IMPORTANTE
                if (!File.Exists(ruta))
                {
                    Console.WriteLine("⚠️ El archivo aún no existe.");
                    //se regresa una lista vacía
                    return new List<Estudiante>();  
                }

                //se obtienen datos deL archivo
                string json = File.ReadAllText(ruta);
                //se parsea el Json a una lista de objetos de la clase Estudiante
                var estudiantes = JsonSerializer.Deserialize<List<Estudiante>>(json);
                Console.WriteLine($"✅ Estudiantes cargados correctamente desde '{ruta}'");
                //el operador ?? verifica si 'estudiantes' es null; si lo es, devuelve una nueva lista vacía
                return estudiantes ?? new List<Estudiante>();
            }
            catch (Exception ex)
            {
                //con errores
                Console.WriteLine($"❌ Error al cargar: {ex.Message}");
                return new List<Estudiante>();
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
                //Nombre
                string nombre;
                //sólo pide que no esté vacío
                do
                {
                    Console.Write("Ingrese el nombre: ");
                    nombre = Console.ReadLine().Trim();
                    if (string.IsNullOrWhiteSpace(nombre))
                        Console.WriteLine("❌ El nombre no puede estar vacío.");
                    //se repite mientras esté vacío o solo contenga espacios
                } while (string.IsNullOrWhiteSpace(nombre));

                // Edad
                int edad;
                //debe ser un número mayor a cero
                while (true)
                {
                    Console.Write("Ingrese la edad: ");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out edad) && edad > 0)
                        break;//sale cuando obtengo lo deseado
                    Console.WriteLine("❌ Edad inválida. Ingrese un número mayor a 0.");
                }

                // Carrera
                string carrera;
                //sólo pide que no esté vacío
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

        public bool HayEstudiantes(List<Estudiante> lista)
        {
            if (!lista.Any())//lista vacía
            {
                Console.WriteLine("⚠️ No hay estudiantes cargados. Ingrese datos primero (opción 11).");
                return false;
            }
            return true;
        }

        public void Where_Linq(List<Estudiante> estudiantes)
        {
            // 1. Filtrar estudiantes por carrera, Where
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

        public void Order_Linq(List<Estudiante> estudiantes)
        {
            // 2. Ordenar por edad
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
                ordenados = estudiantes.OrderByDescending(e => e.Edad);
                Console.WriteLine("\n2. Estudiantes ordenados por edad (descendente):");
            }else
            {
                //(2) ascendente
                ordenados = estudiantes.OrderBy(e => e.Edad);
                Console.WriteLine("\n2. Estudiantes ordenados por edad (ascendente):");
            }

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
            // 4. Agrupar estudientes por carrera y saber su cantidad, GroupBy
            var conteoPorCarrera = estudiantes
                .GroupBy(e => e.Carrera);
            Console.WriteLine("\n4. Cantidad de estudiantes por Carrera:");
            foreach (var grupo in conteoPorCarrera)

            {
                Console.WriteLine($"La carrera de {grupo.Key} tiene {grupo.Count()} estudiante(s).");
            }
        }

        public void Any_Linq(List<Estudiante> estudiantes)
        {
            // 5. Algún estudiante mayor a 20 años, Any
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
            var promedio = estudiantes.Average(e => e.Edad);
            Console.WriteLine("\n7. La edad promedio es de:");
            Console.WriteLine($"{promedio} años");
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
            Console.WriteLine("\n8. Seleccion con objetos anónimos (nombre, carrera):");
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
