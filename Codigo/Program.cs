
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

        public Titulo(string nombre, string isbn, int prestamosHistoricos,
            List<string> ejemplares)
        {
            Nombre = nombre;
            ISBN = isbn;
            PrestamosHistoricos = prestamosHistoricos;
            Ejemplares = ejemplares;
        }

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

            // Registrar tres prestamos de prueba.
            catalogo[0].RegistrarPrestamo();
            catalogo[2].RegistrarPrestamo();
            catalogo[6].RegistrarPrestamo();

            // Ordenar el catalogo por prestamos historicos.
            OrdenarPorPrestamos(catalogo);

            Console.WriteLine("==================================");
            Console.WriteLine("     BIBLIOTECA ESCOLAR");
            Console.WriteLine("==================================");

            Console.WriteLine("\n1. BUSQUEDA SECUENCIAL POR TITULO");

            BuscarPorTitulo(catalogo, "El principito");
            BuscarPorTitulo(catalogo, "Libro inexistente");

            Console.WriteLine("\n2. BUSQUEDA BINARIA POR ISBN");

            BuscarPorISBN(catalogo, "9780307474728");
            BuscarPorISBN(catalogo, "0000000000000");

            Console.WriteLine("\nPresiona una tecla para salir.");
            Console.ReadKey();
        }

        // Ordenamiento por insercion:
        // prestamos historicos de mayor a menor.
        // Si hay empate, orden alfabetico por titulo.
        static void OrdenarPorPrestamos(List<Titulo> catalogo)
        {
            for (int i = 1; i < catalogo.Count; i++)
            {
                Titulo actual = catalogo[i];
                int j = i - 1;

                while (j >= 0 &&
                    (catalogo[j].PrestamosHistoricos <
                     actual.PrestamosHistoricos ||
                    (catalogo[j].PrestamosHistoricos ==
                     actual.PrestamosHistoricos &&
                     string.Compare(catalogo[j].Nombre,
                     actual.Nombre,
                     StringComparison.OrdinalIgnoreCase) > 0)))
                {
                    catalogo[j + 1] = catalogo[j];
                    j--;
                }

                catalogo[j + 1] = actual;
            }
        }

        // Busqueda secuencial por titulo.
        static void BuscarPorTitulo(List<Titulo> catalogo, string nombre)
        {
            bool encontrado = false;

            for (int i = 0; i < catalogo.Count; i++)
            {
                if (string.Equals(catalogo[i].Nombre, nombre,
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nLibro encontrado:");
                    MostrarInformacion(catalogo[i]);
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("\nNo se encontro el titulo: " + nombre);
            }
        }

        // Crea una copia del catalogo y la ordena por ISBN.
        // El catalogo original conserva su orden por prestamos.
        static List<Titulo> CrearCatalogoOrdenadoPorISBN(
            List<Titulo> catalogo)
        {
            List<Titulo> copia = new List<Titulo>(catalogo);

            // Ordenamiento por insercion usando ISBN.
            for (int i = 1; i < copia.Count; i++)
            {
                Titulo actual = copia[i];
                int j = i - 1;

                while (j >= 0 &&
                    string.Compare(copia[j].ISBN, actual.ISBN,
                    StringComparison.Ordinal) > 0)
                {
                    copia[j + 1] = copia[j];
                    j--;
                }

                copia[j + 1] = actual;
            }

            return copia;
        }

        // Busqueda binaria propia por ISBN.
        static void BuscarPorISBN(List<Titulo> catalogo, string isbn)
        {
            List<Titulo> catalogoISBN =
                CrearCatalogoOrdenadoPorISBN(catalogo);

            int inicio = 0;
            int fin = catalogoISBN.Count - 1;
            bool encontrado = false;

            while (inicio <= fin)
            {
                int medio = inicio + (fin - inicio) / 2;

                int comparacion = string.Compare(
                    catalogoISBN[medio].ISBN,
                    isbn,
                    StringComparison.Ordinal);

                if (comparacion == 0)
                {
                    Console.WriteLine("\nISBN encontrado:");
                    MostrarInformacion(catalogoISBN[medio]);
                    encontrado = true;
                    break;
                }
                else if (comparacion < 0)
                {
                    inicio = medio + 1;
                }
                else
                {
                    fin = medio - 1;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("\nNo se encontro el ISBN: " + isbn);
            }
        }

        // Muestra los datos del libro.
        static void MostrarInformacion(Titulo titulo)
        {
            Console.WriteLine("Titulo: " + titulo.Nombre);
            Console.WriteLine("ISBN: " + titulo.ISBN);
            Console.WriteLine("Total de ejemplares: " +
                titulo.Ejemplares.Count);
            Console.WriteLine("Ejemplares disponibles: " +
                titulo.ContarDisponibles());
            Console.WriteLine("Prestamos historicos: " +
                titulo.PrestamosHistoricos);
        }
    }
}


