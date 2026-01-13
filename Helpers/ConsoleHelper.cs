using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEstudiantesLinq.Helpers
{
    internal static class ConsoleHelper
    {
        public static int LeerEnteroSeguro(string mensaje)
        {
            int valor;

            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor))
                    return valor;

                Console.WriteLine("❌ Ingrese un número válido");
            }
        }

        public static string LeerTextoNoVacio(string mensaje)
        {
            string valor;
            do
            {
                Console.Write(mensaje);
                valor = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(valor))
                    Console.WriteLine("❌ No puede estar vacío");

            } while (string.IsNullOrWhiteSpace(valor));

            return valor;
        }

        public static int LeerEnteroMin(string mensaje, int min)
        {
            int valor;

            while (true)
            {
                Console.Write(mensaje);
                // TryParse devuelve true si la conversión fue exitosa 
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= min)
                    return valor;

                Console.WriteLine($"❌ Ingrese un número >= {min}");
            }
        }

        // leer texto obligatorio
        public static string LeerTextoObligatorio(string mensaje)
        {
            string texto;

            while (true)
            {
                Console.Write(mensaje);
                texto = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(texto))
                    return texto;

                Console.WriteLine("❌ No puede estar vacío");
            }
        }

        public static bool LeerConfirmacion(string mensaje)
        {
            string resp;

            do
            {
                Console.Write(mensaje);
                resp = Console.ReadLine()?.Trim().ToLower();

            } while (resp != "s" && resp != "n");

            return resp == "s";
        }

        // Verifica si la lista tiene estudiantes, se usa activamente en el Menú
        public static bool HayElementos<T>(List<T> lista)
        {
            if (!lista.Any())
            {
                Console.WriteLine("⚠️ No hay registros cargados.");
                return false;
            }
            return true;
        }

    }
}
