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

        // Cuenta cuantos ejemplares estan disponibles.
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

        // Registra un prestamo si existe un ejemplar disponible.
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
            // ==========================================
            // CARGA DE LOS OCHO TITULOS
            // ==========================================

            List<Titulo> catalogo = new List<Titulo>
            {
                new Titulo(
                    "Cien años de soledad",
                    "9780307474728",
                    25,
                    new List<string>
                    {
                        "Disponible",
                        "Prestado",
                        "Disponible"
                    }),

                new Titulo(
                    "Don Quijote de la Mancha",
                    "9788420412146",
                    18,
                    new List<string>
                    {
                        "Disponible",
                        "Disponible"
                    }),

                new Titulo(
                    "El principito",
                    "9780156012195",
                    30,
                    new List<string>
                    {
                        "Disponible"
                    }),

                new Titulo(
                    "Harry Potter y la piedra filosofal",
                    "9788478884452",
                    20,
                    new List<string>
                    {
                        "Disponible",
                        "Prestado",
                        "Disponible",
                        "Disponible"
                    }),

                new Titulo(
                    "La Odisea",
                    "9788420634891",
                    12,
                    new List<string>
                    {
                        "Prestado",
                        "Disponible"
                    }),

                new Titulo(
                    "Los juegos del hambre",
                    "9780439023528",
                    15,
                    new List<string>
                    {
                        "Disponible",
                        "Disponible",
                        "Prestado"
                    }),

                new Titulo(
                    "Orgullo y prejuicio",
                    "9780141439518",
                    10,
                    new List<string>
                    {
                        "Disponible"
                    }),

                new Titulo(
                    "Romeo y Julieta",
                    "9780743477116",
                    20,
                    new List<string>
                    {
                        "Disponible",
                        "Prestado"
                    })
            };

            // ==========================================
            // ORDENAR CATALOGO POR PRESTAMOS
            // ==========================================

            OrdenarPorPrestamos(catalogo);

            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine();
                Console.WriteLine("==================================");
                Console.WriteLine("       BIBLIOTECA ESCOLAR");
                Console.WriteLine("==================================");

                Console.WriteLine("1. Mostrar catalogo");
                Console.WriteLine("2. Buscar libro por titulo");
                Console.WriteLine("3. Buscar libro por ISBN");
                Console.WriteLine("4. Registrar prestamo");
                Console.WriteLine("5. Medir busqueda binaria");
                Console.WriteLine("6. Salir");

                Console.WriteLine();
                Console.Write("Selecciona una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    // ==================================
                    // OPCION 1
                    // ==================================

                    case "1":

                        OrdenarPorPrestamos(catalogo);

                        MostrarCatalogo(catalogo);

                        break;


                    // ==================================
                    // OPCION 2
                    // ==================================

                    case "2":

                        Console.WriteLine();
                        Console.Write("Escribe el titulo del libro: ");

                        string nombre = Console.ReadLine();

                        BuscarPorTitulo(catalogo, nombre);

                        break;


                    // ==================================
                    // OPCION 3
                    // ==================================

                    case "3":

                        Console.WriteLine();
                        Console.Write("Escribe el ISBN del libro: ");

                        string isbn = Console.ReadLine();

                        BuscarPorISBN(catalogo, isbn);

                        break;


                    // ==================================
                    // OPCION 4
                    // ==================================

                    case "4":

                        Console.WriteLine();
                        Console.Write("Escribe el titulo que deseas prestar: ");

                        string tituloPrestamo = Console.ReadLine();

                        Titulo libro =
                            BuscarTitulo(catalogo, tituloPrestamo);

                        if (libro == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("El libro no existe.");
                        }
                        else
                        {
                            bool prestamo =
                                libro.RegistrarPrestamo();

                            if (prestamo)
                            {
                                Console.WriteLine();
                                Console.WriteLine(
                                    "Prestamo registrado correctamente.");

                                Console.WriteLine(
                                    "Ejemplares disponibles: " +
                                    libro.ContarDisponibles());

                                Console.WriteLine(
                                    "Prestamos historicos: " +
                                    libro.PrestamosHistoricos);

                                // Volvemos a ordenar porque
                                // cambiaron los prestamos historicos.
                                OrdenarPorPrestamos(catalogo);
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine(
                                    "No hay ejemplares disponibles.");
                            }
                        }

                        break;


                    // ==================================
                    // OPCION 5
                    // ==================================

                    case "5":

                        RealizarPruebasISBN(catalogo);

                        break;


                    // ==================================
                    // OPCION 6
                    // ==================================

                    case "6":

                        continuar = false;

                        Console.WriteLine();
                        Console.WriteLine("Hasta luego.");

                        break;


                    // ==================================
                    // OPCION INVALIDA
                    // ==================================

                    default:

                        Console.WriteLine();
                        Console.WriteLine(
                            "Opcion no valida. Intenta de nuevo.");

                        break;
                }
            }
        }


        // ==================================================
        // ORDENAMIENTO POR INSERCION
        // ==================================================

        static void OrdenarPorPrestamos(List<Titulo> catalogo)
        {
            for (int i = 1; i < catalogo.Count; i++)
            {
                Titulo actual = catalogo[i];

                int j = i - 1;

                while (
                    j >= 0 &&
                    (
                        catalogo[j].PrestamosHistoricos <
                        actual.PrestamosHistoricos

                        ||

                        (
                            catalogo[j].PrestamosHistoricos ==
                            actual.PrestamosHistoricos

                            &&

                            string.Compare(
                                catalogo[j].Nombre,
                                actual.Nombre,
                                StringComparison.OrdinalIgnoreCase
                            ) > 0
                        )
                    )
                )
                {
                    catalogo[j + 1] = catalogo[j];

                    j--;
                }

                catalogo[j + 1] = actual;
            }
        }


        // ==================================================
        // MOSTRAR CATALOGO
        // ==================================================

        static void MostrarCatalogo(List<Titulo> catalogo)
        {
            Console.WriteLine();
            Console.WriteLine("========== CATALOGO ==========");

            foreach (Titulo titulo in catalogo)
            {
                MostrarInformacion(titulo);

                Console.WriteLine("------------------------------");
            }
        }


        // ==================================================
        // BUSQUEDA SECUENCIAL POR TITULO
        // ==================================================

        static Titulo BuscarTitulo(
            List<Titulo> catalogo,
            string nombre)
        {
            foreach (Titulo titulo in catalogo)
            {
                if (
                    string.Equals(
                        titulo.Nombre,
                        nombre,
                        StringComparison.OrdinalIgnoreCase
                    )
                )
                {
                    return titulo;
                }
            }

            return null;
        }


        // ==================================================
        // MOSTRAR RESULTADO DE BUSQUEDA POR TITULO
        // ==================================================

        static void BuscarPorTitulo(
            List<Titulo> catalogo,
            string nombre)
        {
            Titulo titulo =
                BuscarTitulo(catalogo, nombre);

            if (titulo != null)
            {
                Console.WriteLine();
                Console.WriteLine("Libro encontrado:");

                MostrarInformacion(titulo);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(
                    "No se encontro el titulo: " +
                    nombre);
            }
        }


        // ==================================================
        // CREAR COPIA ORDENADA POR ISBN
        // ==================================================

        static List<Titulo> CrearCatalogoOrdenadoPorISBN(
            List<Titulo> catalogo)
        {
            List<Titulo> copia =
                new List<Titulo>(catalogo);

            // Ordenamiento por insercion.
            for (int i = 1; i < copia.Count; i++)
            {
                Titulo actual = copia[i];

                int j = i - 1;

                while (
                    j >= 0 &&
                    string.Compare(
                        copia[j].ISBN,
                        actual.ISBN,
                        StringComparison.Ordinal
                    ) > 0
                )
                {
                    copia[j + 1] = copia[j];

                    j--;
                }

                copia[j + 1] = actual;
            }

            return copia;
        }


        // ==================================================
        // BUSQUEDA BINARIA PROPIA
        // ==================================================

        static int BuscarPorISBN(
            List<Titulo> catalogo,
            string isbn)
        {
            // Creamos una copia ordenada por ISBN.
            List<Titulo> copia =
                CrearCatalogoOrdenadoPorISBN(catalogo);

            int inicio = 0;

            int fin =
                copia.Count - 1;

            int comparaciones = 0;

            // Busqueda binaria.
            while (inicio <= fin)
            {
                int medio =
                    inicio +
                    (fin - inicio) / 2;

                // Contamos la comparacion.
                comparaciones++;

                int resultado =
                    string.Compare(
                        copia[medio].ISBN,
                        isbn,
                        StringComparison.Ordinal
                    );

                if (resultado == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "ISBN encontrado:");

                    MostrarInformacion(
                        copia[medio]);

                    Console.WriteLine(
                        "Comparaciones realizadas: " +
                        comparaciones);

                    return comparaciones;
                }

                else if (resultado < 0)
                {
                    inicio =
                        medio + 1;
                }

                else
                {
                    fin =
                        medio - 1;
                }
            }

            Console.WriteLine();

            Console.WriteLine(
                "No se encontro el ISBN: " +
                isbn);

            Console.WriteLine(
                "Comparaciones realizadas: " +
                comparaciones);

            return comparaciones;
        }


        // ==================================================
        // CINCO PRUEBAS DE ISBN
        // ==================================================

        static void RealizarPruebasISBN(
            List<Titulo> catalogo)
        {
            string[] consultas =
            {
                "9780307474728",
                "9788420412146",
                "9780156012195",
                "9780439023528",
                "0000000000000"
            };

            Console.WriteLine();
            Console.WriteLine(
                "==================================");

            Console.WriteLine(
                "      PRUEBAS DE BUSQUEDA ISBN");

            Console.WriteLine(
                "==================================");

            for (int i = 0;
                i < consultas.Length;
                i++)
            {
                Console.WriteLine();

                Console.WriteLine(
                    "Consulta " + (i + 1));

                Console.WriteLine(
                    "ISBN: " +
                    consultas[i]);

                int comparaciones =
                    BuscarPorISBN(
                        catalogo,
                        consultas[i]);

                Console.WriteLine(
                    "Resultado: " +
                    comparaciones +
                    " comparaciones");
            }
        }


        // ==================================================
        // MOSTRAR INFORMACION DEL LIBRO
        // ==================================================

        static void MostrarInformacion(
            Titulo titulo)
        {
            Console.WriteLine(
                "Titulo: " +
                titulo.Nombre);

            Console.WriteLine(
                "ISBN: " +
                titulo.ISBN);

            Console.WriteLine(
                "Total de ejemplares: " +
                titulo.Ejemplares.Count);

            Console.WriteLine(
                "Disponibles: " +
                titulo.ContarDisponibles());

            Console.WriteLine(
                "Prestamos historicos: " +
                titulo.PrestamosHistoricos);
        }
    }
}