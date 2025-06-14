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
        private static GestorEstudiantes objGestor;

        static void Main(string[] args)
        {
            objGestor = new GestorEstudiantes();
            // Lista de estudiantes (fuente de datos en memoria)
            List<Estudiante> estudiantes = new List<Estudiante>
            {
                new Estudiante { Id = 1, Nombre = "Ana", Edad = 20, Carrera = "Ingeniería" },
                new Estudiante { Id = 2, Nombre = "Luis", Edad = 22, Carrera = "Derecho" },
                new Estudiante { Id = 3, Nombre = "María", Edad = 19, Carrera = "Medicina" },
                new Estudiante { Id = 4, Nombre = "Pedro", Edad = 21, Carrera = "Ingeniería" },
            };

            objGestor.Where_Linq(estudiantes);
            objGestor.OrderByDescending_Linq(estudiantes);
            objGestor.Select_Linq(estudiantes);
            objGestor.GroupBy_Linq(estudiantes);
            objGestor.Any_Linq(estudiantes);
            objGestor.First_Linq(estudiantes);
            objGestor.Average_Linq(estudiantes);
            objGestor.SelectAnonimos_Linq(estudiantes);
            objGestor.Resumen_Linq(estudiantes);
            Console.ReadKey();
        }
    }
}
 