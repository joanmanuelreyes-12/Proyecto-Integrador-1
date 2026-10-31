
using System;
using System.Collections.Generic;

namespace BibliotecaEscolar
{
    class Titulo
    {
        public string Nombre;
        public string ISBN;
        public int PrestamosHistoricos;
        public List<string> Ejemplares;

        public Titulo(string nombre, string isbn, int prestamosHistoricos, List<string> ejemplares)
        {
            Nombre = nombre;
            ISBN = isbn;
            PrestamosHistoricos = prestamosHistoricos;
            Ejemplares = ejemplares;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Titulo> catalogo = new List<Titulo>
            {
                new Titulo("Cien años de soledad", "9780307474728", 25,
                    new List<string> { "Disponible", "Prestado", "Disponible" }),

                new Titulo("Don Quijote de la Mancha", "9788420412146", 18,
                    new List<string> { "Disponible", "Disponible" }),

                new Titulo("El principito", "9780156012195", 30,
                    new List<string> { "Disponible" }),

                new Titulo("Harry Potter y la piedra filosofal", "9788478884452", 22,
                    new List<string> { "Disponible", "Prestado", "Disponible", "Disponible" }),

                new Titulo("La Odisea", "9788420634891", 12,
                    new List<string> { "Prestado", "Disponible" }),

                new Titulo("Los juegos del hambre", "9780439023528", 15,
                    new List<string> { "Disponible", "Disponible", "Prestado" }),

                new Titulo("Orgullo y prejuicio", "9780141439518", 10,
                    new List<string> { "Disponible" }),

                new Titulo("Romeo y Julieta", "9780743477116", 20,
                    new List<string> { "Disponible", "Prestado" })
            };

            Console.WriteLine("===== BIBLIOTECA ESCOLAR =====\n");

            foreach (Titulo titulo in catalogo)
            {
                int disponibles = 0;

                foreach (string ejemplar in titulo.Ejemplares)
                {
                    if (ejemplar == "Disponible")
                    {
                        disponibles++;
                    }
                }

                Console.WriteLine("Título: " + titulo.Nombre);
                Console.WriteLine("ISBN: " + titulo.ISBN);
                Console.WriteLine("Préstamos históricos: " + titulo.PrestamosHistoricos);
                Console.WriteLine("Total de ejemplares: " + titulo.Ejemplares.Count);
                Console.WriteLine("Disponibles: " + disponibles);
                Console.WriteLine("--------------------------------");
            }

            Console.WriteLine("\nTotal de títulos: " + catalogo.Count);

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }
    }
}
