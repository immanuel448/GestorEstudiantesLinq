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
            string texto;

            do
            {
                Console.Write(mensaje);
                texto = Console.ReadLine()?.Trim();
            } while (string.IsNullOrWhiteSpace(texto));

            return texto;
        }
    }
}
