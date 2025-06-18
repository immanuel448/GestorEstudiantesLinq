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
        public void InsertarEstudiantesEnBD()
        {
            using (var db = new AppDbContext())
            {
                // Verifica si ya hay datos
                if (db.Estudiantes.Any())
                {
                    Console.WriteLine("⚠️ Ya existen estudiantes en la base de datos. No se insertarán duplicados.");
                    return;
                }

                var estudiantes = new List<Estudiante>
        {
            new Estudiante { Nombre = "Ana", Edad = 20, Carrera = "Ingeniería" },
            new Estudiante { Nombre = "Luis", Edad = 22, Carrera = "Derecho" },
            new Estudiante { Nombre = "María", Edad = 19, Carrera = "Medicina" },
            new Estudiante { Nombre = "Pedro", Edad = 21, Carrera = "Ingeniería" },
        };

                db.Estudiantes.AddRange(estudiantes);
                db.SaveChanges();
                Console.WriteLine("✅ Estudiantes de prueba insertados correctamente en la base de datos.");
            }
        }

        public void GuardarEstudiantesEnJson()
        {
            string ruta = "estudiantes.json";

            try
            {
                using var db = new AppDbContext();
                var estudiantes = db.Estudiantes.ToList();

                string json = JsonSerializer.Serialize(estudiantes, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ruta, json);
                Console.WriteLine($"✅ Estudiantes guardados correctamente en '{ruta}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al guardar: {ex.Message}");
            }
        }

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
                // Nombre
                string nombre;
                //sólo pide que no esté vacío
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
                //debe ser un número mayo a cero
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

                // ¿Desea continuar? -----------------------------
                string respuesta = "";
                while(respuesta != "s" && respuesta != "n")
                {
                    //si es "s" vuelve a repetir todo
                    //si es "n" sale de este método
                    //si es diferente "s", "n", se repite este ciclo
                    Console.Write("\n¿Desea agregar otro estudiante? (s/n): ");
                    respuesta = Console.ReadLine().Trim().ToLower();
                }
                if (respuesta == "n")
                    //termina este método
                    break;
            }
            return estudiantes;
        }

        public void Where_Linq()
        {
            //ya se toman los datos directamente desde la bd
            using (var db = new AppDbContext())
            {
                var ingenieria = db.Estudiantes
                                   .Where(e => e.Carrera == "Ingeniería")
                                   .ToList();

                Console.WriteLine("\n1. Estudiantes de ingeniería:");
                int apoyo = 1;
                foreach (var unaFila in ingenieria)
                {
                    Console.WriteLine($"{apoyo++}.- {unaFila.Nombre}");
                }
            }
        }

        public void OrderByDescending_Linq()
        {
            //ya se toman los datos directamente desde la bd
            using (var db = new AppDbContext())
            {
                // 2. Ordenar por edad descendente
                var ordenados = db.Estudiantes.OrderByDescending(e => e.Edad).ToList();

                Console.WriteLine("\n2. Estudiantes ordenados por edad (descendiente):");
                foreach (var e in ordenados)
                {
                    Console.WriteLine($"-{e.Nombre}, carrera: {e.Carrera}, {e.Edad} años");
                }
            }
        }

        public void Select_Linq()
        {
            //ya se toman los datos directamente desde la bd
            using (var db = new AppDbContext())
            {
                // 3. Proyección: solo nombres Select
                var nombres = db.Estudiantes.Select(e => e.Nombre).ToList();

                Console.WriteLine("\n3. Nombres de todos los estudiantes:");
                foreach (var nombre in nombres)
                {
                    Console.WriteLine($"-{nombre}");
                }  
            }
        }

        public void GroupBy_Linq()
        {
            //ya se toman los datos directamente desde la bd
            using (var db = new AppDbContext())
            {
                // 4. Contar estudiantes por carrera, GroupBy - Select
                var conteoPorCarrera = db.Estudiantes
                    .GroupBy(e => e.Carrera)
                    .Select(grupo => new
                    {
                        carrera = grupo.Key,
                        cantidad = grupo.Count()
                    }).ToList();
                Console.WriteLine("\n4. Cantidad de estudiantes por Carrera:");
                foreach (var e in conteoPorCarrera)
                {
                    Console.WriteLine($"La carrera de {e.carrera} tiene {e.cantidad} estudiante(s).");
                }
            }
        }

        public void Any_Linq()
        {
            //ya se toman los datos directamente desde la bd
            using (var db = new AppDbContext())
            {
                // 5. Algún estudiante mayor a 20 años, Any
                string resultado = db.Estudiantes.Any(e => e.Edad > 20) ? "Sí existe mínimo un estudiante mayor de 20 años" : "No hay ningún estudiante mayor de 20 años";

                Console.WriteLine("\n5. Estudiantes mayores de 20 años.");
                Console.WriteLine(resultado);
            }
        }

        public void First_Linq()
        {
            using (var db = new AppDbContext())
            {
                var masJoven = db.Estudiantes
                    .OrderBy(e => e.Edad)
                    .FirstOrDefault();

                if (masJoven != null)
                {
                    Console.WriteLine("\n6. El estudiante más joven es:");
                    Console.WriteLine($"{masJoven.Nombre} con {masJoven.Edad} años.");
                }
                else
                {
                    Console.WriteLine("No hay estudiantes registrados.");
                }
            }
        }


        public void Average_Linq()
        {
            //ya se toman los datos directamente desde la bd
            using (var db = new AppDbContext())
            {
                // 7: Average – Calcular la edad promedio
                if (db.Estudiantes.Any())
                //se realiza una verificación para que la colección no esté vacía
                {
                    var promedio = db.Estudiantes.Average(e => e.Edad);
                    Console.WriteLine("\n7. La edad promedio es de:");
                    Console.WriteLine($"{promedio} años");
                }
                else
                {
                    Console.WriteLine("No hay estudiantes para realizar el cálculo");
                }
            }
        }

        public void SelectAnonimos_Linq()
        {
            //ya se toman los datos directamente desde la bd
            using (var db = new AppDbContext())
            {
                //8: Select con objetos anónimos
                var sintesis = db.Estudiantes.Select(e => new
                {
                    //se accede con el nombre personalizado "enombre"
                    enombre = e.Nombre,
                    //se accede mediante "la propiedad Carrera"
                    e.Carrera
                }).ToList();
                Console.WriteLine("\n8. Seleccion con objetos anónimos:");
                int apoyo = 1;
                foreach (var item in sintesis)
                {
                    Console.WriteLine($"{apoyo++}.- {item.enombre}, con la carrera de {item.Carrera}");
                }
            }
        }

        public void Resumen_Linq()
        {
            /*
                RESUMEN, hasta ahora
                Ahora queremos crear una nueva proyección con esta información:
                Nombre
                Edad
                ¿Es mayor de edad? (Edad >= 18
                Carrera en mayúsculas
            */

            //ya se toman los datos directamente desde la bd
            using (var db = new AppDbContext())
            {
                Console.WriteLine("\nRESUMEN:");
                var resultados = db.Estudiantes.Select(e => new
                {
                    e.Nombre,
                    e.Carrera,
                    mayorEdad = e.Edad >= 18,
                    carreraMayuscula = e.Carrera.ToUpper()
                }).ToList();

                foreach (var item in resultados)
                {
                    Console.WriteLine($"Nombre {item.Nombre}, carrera {item.Carrera}, es mayor de edad: {item.mayorEdad}, {item.carreraMayuscula}");

                }
            }
        }
    }
}
