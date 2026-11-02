
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

        // Contar los ejemplares disponibles
        public int ContarDisponibles()
        {
            int disponibles = 0;

            foreach (string ejemplar in Ejemplares)
            {
                if (ejemplar == "Disponible")
                {
                    disponibles++;
                }
            }

            return disponibles;
        }

        // Registrar un préstamo
        public bool RegistrarPrestamo()
        {
            for (int i = 0; i < Ejemplares.Count; i++)
            {
                if (Ejemplares[i] == "Disponible")
                {
                    Ejemplares[i] = "Prestado";
                    PrestamosHistoricos++;
                    return true;
                }
            }

            return false;
        }
    }

    class Program
    {
        // Ordenar por préstamos históricos de mayor a menor.
        // En caso de empate, ordenar alfabéticamente.
        static void OrdenarPorPrestamos(List<Titulo> catalogo)
        {
            for (int i = 1; i < catalogo.Count; i++)
            {
                Titulo actual = catalogo[i];
                int j = i - 1;

                while (j >= 0 &&
                    (catalogo[j].PrestamosHistoricos < actual.PrestamosHistoricos ||
                    (catalogo[j].PrestamosHistoricos == actual.PrestamosHistoricos &&
                    string.Compare(catalogo[j].Nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase) > 0)))
                {
                    catalogo[j + 1] = catalogo[j];
                    j--;
                }

                catalogo[j + 1] = actual;
            }
        }

        // Mostrar el catálogo
        static void MostrarCatalogo(List<Titulo> catalogo)
        {
            foreach (Titulo titulo in catalogo)
            {
                Console.WriteLine("Titulo: " + titulo.Nombre);
                Console.WriteLine("ISBN: " + titulo.ISBN);
                Console.WriteLine("Ejemplares totales: " + titulo.Ejemplares.Count);
                Console.WriteLine("Disponibles: " + titulo.ContarDisponibles());
                Console.WriteLine("Prestamos historicos: " + titulo.PrestamosHistoricos);
                Console.WriteLine("-------------------------------------------");
            }
        }

        static void Main(string[] args)
        {
            // Catálogo con ocho títulos y sus ejemplares
            List<Titulo> catalogo = new List<Titulo>
            {
                new Titulo("Cien años de soledad", "9780307474728", 25,
                    new List<string> { "Disponible", "Prestado", "Disponible" }),

                new Titulo("Don Quijote de la Mancha", "9788420412146", 18,
                    new List<string> { "Disponible", "Disponible" }),

                new Titulo("El principito", "9780156012195", 30,
                    new List<string> { "Disponible" }),

                new Titulo("Harry Potter y la piedra filosofal", "9788478884452", 20,
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

            // Mostrar catálogo antes de los préstamos
            Console.WriteLine("===== CATALOGO ANTES DE LOS PRESTAMOS =====\n");
            MostrarCatalogo(catalogo);

            // Simular tres préstamos
            Console.WriteLine("\n===== REGISTRO DE TRES PRESTAMOS =====\n");

            Titulo libro1 = catalogo[0];
            Titulo libro2 = catalogo[2];
            Titulo libro3 = catalogo[6];

            Console.WriteLine(
                libro1.Nombre + ": " +
                (libro1.RegistrarPrestamo() ? "Prestamo registrado" : "Sin ejemplares disponibles")
            );

            Console.WriteLine(
                libro2.Nombre + ": " +
                (libro2.RegistrarPrestamo() ? "Prestamo registrado" : "Sin ejemplares disponibles")
            );

            Console.WriteLine(
                libro3.Nombre + ": " +
                (libro3.RegistrarPrestamo() ? "Prestamo registrado" : "Sin ejemplares disponibles")
            );

            // Ordenar el catálogo manualmente
            OrdenarPorPrestamos(catalogo);

            // Mostrar el catálogo ordenado
            Console.WriteLine("\n===== CATALOGO ORDENADO POR PRESTAMOS =====\n");
            MostrarCatalogo(catalogo);

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }
    }
}

