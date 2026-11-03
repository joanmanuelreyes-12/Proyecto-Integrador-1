
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

                new Titulo("Harry Potter y la piedra filosofal",
                    "9788478884452", 20,
                    new List<string> { "Disponible", "Prestado",
                        "Disponible", "Disponible" }),

                new Titulo("La Odisea", "9788420634891", 12,
                    new List<string> { "Prestado", "Disponible" }),

                new Titulo("Los juegos del hambre", "9780439023528", 15,
                    new List<string> { "Disponible", "Disponible", "Prestado" }),

                new Titulo("Orgullo y prejuicio", "9780141439518", 10,
                    new List<string> { "Disponible" }),

                new Titulo("Romeo y Julieta", "9780743477116", 20,
                    new List<string> { "Disponible", "Prestado" })
            };

            // Orden inicial del catalogo.
            OrdenarPorPrestamos(catalogo);

            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\n==================================");
                Console.WriteLine("       BIBLIOTECA ESCOLAR");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Mostrar catalogo");
                Console.WriteLine("2. Buscar libro por titulo");
                Console.WriteLine("3. Buscar libro por ISBN");
                Console.WriteLine("4. Registrar prestamo");
                Console.WriteLine("5. Salir");
                Console.Write("\nSelecciona una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        OrdenarPorPrestamos(catalogo);
                        MostrarCatalogo(catalogo);
                        break;

                    case "2":
                        Console.Write("\nEscribe el titulo del libro: ");
                        string nombre = Console.ReadLine();

                        BuscarPorTitulo(catalogo, nombre);
                        break;

                    case "3":
                        Console.Write("\nEscribe el ISBN del libro: ");
                        string isbn = Console.ReadLine();

                        BuscarPorISBN(catalogo, isbn);
                        break;

                    case "4":
                        Console.Write("\nEscribe el titulo que deseas prestar: ");
                        string tituloPrestamo = Console.ReadLine();

                        Titulo libro = BuscarTitulo(catalogo, tituloPrestamo);

                        if (libro == null)
                        {
                            Console.WriteLine("El libro no existe.");
                        }
                        else if (libro.RegistrarPrestamo())
                        {
                            Console.WriteLine("Prestamo registrado correctamente.");
                            Console.WriteLine("Disponibles: " +
                                libro.ContarDisponibles());
                            Console.WriteLine("Prestamos historicos: " +
                                libro.PrestamosHistoricos);

                            OrdenarPorPrestamos(catalogo);
                        }
                        else
                        {
                            Console.WriteLine(
                                "No hay ejemplares disponibles para prestar.");
                        }

                        break;

                    case "5":
                        continuar = false;
                        Console.WriteLine("Hasta luego.");
                        break;

                    default:
                        Console.WriteLine("Opcion no valida. Intenta de nuevo.");
                        break;
                }
            }
        }

        // Ordenamiento por insercion:
        // prestamos de mayor a menor y titulo alfabetico en empates.
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

        // Muestra el catalogo ordenado.
        static void MostrarCatalogo(List<Titulo> catalogo)
        {
            Console.WriteLine("\n========== CATALOGO ==========");

            foreach (Titulo titulo in catalogo)
            {
                MostrarInformacion(titulo);
                Console.WriteLine("------------------------------");
            }
        }

        // Busca un titulo recorriendo la lista.
        static Titulo BuscarTitulo(List<Titulo> catalogo, string nombre)
        {
            foreach (Titulo titulo in catalogo)
            {
                if (string.Equals(titulo.Nombre, nombre,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return titulo;
                }
            }

            return null;
        }

        static void BuscarPorTitulo(List<Titulo> catalogo, string nombre)
        {
            Titulo titulo = BuscarTitulo(catalogo, nombre);

            if (titulo != null)
            {
                Console.WriteLine("\nLibro encontrado:");
                MostrarInformacion(titulo);
            }
            else
            {
                Console.WriteLine("No se encontro el titulo: " + nombre);
            }
        }

        // Crea una copia del catalogo ordenada por ISBN.
        static List<Titulo> CrearCatalogoOrdenadoPorISBN(
            List<Titulo> catalogo)
        {
            List<Titulo> copia = new List<Titulo>(catalogo);

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
            List<Titulo> copia = CrearCatalogoOrdenadoPorISBN(catalogo);

            int inicio = 0;
            int fin = copia.Count - 1;

            while (inicio <= fin)
            {
                int medio = inicio + (fin - inicio) / 2;

                int comparacion = string.Compare(
                    copia[medio].ISBN,
                    isbn,
                    StringComparison.Ordinal);

                if (comparacion == 0)
                {
                    Console.WriteLine("\nLibro encontrado:");
                    MostrarInformacion(copia[medio]);
                    return;
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

            Console.WriteLine("No se encontro el ISBN: " + isbn);
        }

        // Muestra la informacion de un titulo.
        static void MostrarInformacion(Titulo titulo)
        {
            Console.WriteLine("Titulo: " + titulo.Nombre);
            Console.WriteLine("ISBN: " + titulo.ISBN);
            Console.WriteLine("Total de ejemplares: " +
                titulo.Ejemplares.Count);
            Console.WriteLine("Disponibles: " +
                titulo.ContarDisponibles());
            Console.WriteLine("Prestamos historicos: " +
                titulo.PrestamosHistoricos);
        }
    }
}
