using System;
using System.Collections.Generic;
using System.Linq;
using GestorEstudiantesLinq.Models;
using GestorEstudiantesLinq.Services;
using GestorEstudiantesLinq.Data;
using GestorEstudiantesLinq.Helpers;

namespace GestorEstudiantesLinq
{

    class Program
    {
        // Instancia del gestor que contiene toda la lógica del sistema (cargar, guardar, consultas LINQ, etc.)
        private static GestorEstudiantes objGestor = new GestorEstudiantes();
        private static List<Estudiante> estudiantes = new List<Estudiante>();

        static async Task Main(string[] args)
        {
            await MostrarMenuAsync();
        }

        private static async Task MostrarMenuAsync()
        {
            estudiantes = await objGestor.ObtenerEstudiantesAsync();

            while (true)
            {
                //esto es el menú, si no se selecciona nada el switch va a repetir el menú
                Console.Clear();
                Console.WriteLine("MENÚ PRINCIPAL - CONSULTAS LINQ");
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
                Console.WriteLine("12. Editar estudiante");
                Console.WriteLine("13. Eliminar estudiante");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                //se obtiene la respuesta
                string opcion = Console.ReadLine();

                //se gestionan las respuestas, si no se pide salir (opción 0), va a continuar ciclando
                switch (opcion)
                {
                    case "1":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.Where_Linq(estudiantes);
                        break;
                    case "2":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.Order_Linq(estudiantes);
                        break;
                    case "3":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.Select_Linq(estudiantes);
                        break;
                    case "4":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.GroupBy_Linq(estudiantes);
                        break;
                    case "5":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.Any_Linq(estudiantes);
                        break;
                    case "6":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.First_Linq(estudiantes);
                        break;
                    case "7":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.Average_Linq(estudiantes);
                        break;
                    case "8":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.SelectAnonimos_Linq(estudiantes);
                        break;
                    case "9":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.Resumen_Linq(estudiantes);
                        break;
                    case "10":
                        if (ConsoleHelper.HayElementos(estudiantes))
                            objGestor.ExportarEstudiantesAJson(estudiantes);
                        break;
                    case "11":
                        RegistrarEstudiantes();
                        break;
                    case "12":
                        EditarEstudiante();
                        break;
                    case "13":
                        EliminarEstudiante();
                        break;
                    case "0":
                        Console.WriteLine("Saliendo...");
                        //termina el método Main -------------
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        //repite el menú
                        break;
                }
                //en este punto va a volver a ciclar
                Console.WriteLine("\nPresione una tecla para volver al Menú");
                Console.ReadKey();
            }
        }

        private static async Task RegistrarEstudiantes()
        {
            // Captura el último ID disponible para evitar duplicados
            int ultimoId = estudiantes.Any() ? estudiantes.Max(e => e.Id) : 0;

            // Leer estudiantes desde la consola (validadLeerEstudiantesDesdeConsola por consola)
            var nuevosEstudiantes = objGestor.LeerEstudiantesDesdeConsola(ultimoId);

            // Guarda los nuevos registros en la base de datos
            await objGestor.GuardarEstudiantesEnBDAsync(nuevosEstudiantes);
            // Actualizamos la lista de estudiantes
            estudiantes = await objGestor.ObtenerEstudiantesAsync();
        }

        private static async Task EditarEstudiante()
        {
            int idEdit = ConsoleHelper.LeerEnteroSeguro("ID a editar: ");

            var paraEditar =
                estudiantes.FirstOrDefault(e => e.Id == idEdit);

            // Validar si se encontró el estudiante
            if (paraEditar == null)
            {
                Console.WriteLine("No existe");
                return;//origiannelmente era break eeeee
            }

            // Solicitar nuevos datos
            paraEditar.Nombre = ConsoleHelper.LeerTextoObligatorio("Nuevo nombre: ");

            paraEditar.Edad = ConsoleHelper.LeerEnteroSeguro("Nueva edad: ");

            paraEditar.Carrera = ConsoleHelper.LeerTextoObligatorio("Nueva carrera: ");

            // Actualizar en la base de datos
            await objGestor
                .ActualizarEstudianteAsync(paraEditar);

            // Actualizamos la lista de estudiantes
            estudiantes = await objGestor.ObtenerEstudiantesAsync();

            Console.WriteLine("Actualizado");
        }

        private static async Task EliminarEstudiante()
        {
            // 1️ Pedimos el ID de forma segura (sin errores)
            int idDel =
                ConsoleHelper.LeerEnteroSeguro("ID a eliminar: ");

            // 2️ Buscamos en la lista cargada en memoria
            var estudianteParaBorrar =
                estudiantes.FirstOrDefault(e => e.Id == idDel);

            // 3️ Si no existe, avisamos y salimos
            if (estudianteParaBorrar == null)
            {
                Console.WriteLine("No existe ese estudiante");
                return; // salimos del método
            }

            // 4️ Mostramos QUÉ se va a borrar (UX)
            Console.WriteLine($"Se eliminará: {estudianteParaBorrar.Nombre} ({estudianteParaBorrar.Carrera})");

            // 5️ Pedimos confirmación al usuario
            bool confirmar = ConsoleHelper.LeerConfirmacion("¿Seguro que desea eliminar? (s/n): ");

            // 6️ Si dice NO → salimos sin borrar
            if (!confirmar) return;

            // 7️ Llamamos al gestor para borrar en BD
            await objGestor.EliminarEstudianteAsync(idDel);

            // 8️ Recargamos la lista desde la BD
            estudiantes = await objGestor.ObtenerEstudiantesAsync();

            // 9️ Mensaje final
            Console.WriteLine("Eliminado");
        }

    }

}
