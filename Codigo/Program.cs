

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

            // Mostrar el catálogo antes de los préstamos
            Console.WriteLine("===== CATALOGO ANTES DE LOS PRESTAMOS =====\n");

            foreach (Titulo titulo in catalogo)
            {
                Console.WriteLine("Titulo: " + titulo.Nombre);
                Console.WriteLine("ISBN: " + titulo.ISBN);
                Console.WriteLine("Ejemplares totales: " + titulo.Ejemplares.Count);
                Console.WriteLine("Disponibles: " + titulo.ContarDisponibles());
                Console.WriteLine("Prestamos historicos: " + titulo.PrestamosHistoricos);
                Console.WriteLine("-------------------------------------------");
            }

            // Seleccionar tres títulos para simular préstamos
            Console.WriteLine("\n===== REGISTRO DE TRES PRESTAMOS =====\n");

            Titulo libro1 = catalogo[0];
            Titulo libro2 = catalogo[2];
            Titulo libro3 = catalogo[6];

            // Primer préstamo
            Console.WriteLine(
                libro1.Nombre + ": " +
                (libro1.RegistrarPrestamo()
                    ? "Prestamo registrado"
                    : "No hay ejemplares disponibles")
            );

            // Segundo préstamo
            Console.WriteLine(
                libro2.Nombre + ": " +
                (libro2.RegistrarPrestamo()
                    ? "Prestamo registrado"
                    : "No hay ejemplares disponibles")
            );

            // Tercer préstamo
            Console.WriteLine(
                libro3.Nombre + ": " +
                (libro3.RegistrarPrestamo()
                    ? "Prestamo registrado"
                    : "No hay ejemplares disponibles")
            );

            // Mostrar el catálogo después de los préstamos
            Console.WriteLine("\n===== CATALOGO DESPUES DE LOS PRESTAMOS =====\n");

            foreach (Titulo titulo in catalogo)
            {
                Console.WriteLine("Titulo: " + titulo.Nombre);
                Console.WriteLine("ISBN: " + titulo.ISBN);
                Console.WriteLine("Ejemplares totales: " + titulo.Ejemplares.Count);
                Console.WriteLine("Disponibles: " + titulo.ContarDisponibles());
                Console.WriteLine("Prestamos historicos: " + titulo.PrestamosHistoricos);
                Console.WriteLine("-------------------------------------------");
            }

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }
    }
}
