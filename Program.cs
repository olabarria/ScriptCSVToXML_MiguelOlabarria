int direccion;
do
{
    Console.WriteLine();
    Console.WriteLine("=== Conversor csv & xml ===");
    Console.WriteLine("1. CSV a XML");
    Console.WriteLine("2. XML a CSV");
    Console.WriteLine("0. Salir");
    Console.Write("Elige una opción: ");
    int.TryParse(Console.ReadLine(), out direccion);

    if (direccion == 1 || direccion == 2)
    {
        bool aXml = direccion == 1;

        Console.Write(aXml ? "Ruta del CSV: " : "Ruta del XML: ");
        string rutaEntrada = Limpiar(Console.ReadLine());

        Console.Write(aXml ? "Ruta donde guardar el XML (con el nombre): "
                           : "Ruta donde guardar el CSV (con el nombre): ");
        string rutaSalida = Limpiar(Console.ReadLine());

        try
        {
            var pelis = aXml ? GestorCSV.Leer(rutaEntrada) : GestorXML.Leer(rutaEntrada);
            Console.WriteLine("Leídas " + pelis.Count + " películas.");
            MenuExportar(pelis, rutaSalida, aXml);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
    else if (direccion != 0)
    {
        Console.WriteLine("Opción no válida");
    }
} while (direccion != 0);

Console.WriteLine("Adiós");


string Limpiar(string? texto)
{
    return (texto ?? "").Trim().Trim('"');
}

void Guardar(List<Pelicula> lista, string ruta, bool aXml)
{
    if (aXml)
        GestorXML.Exportar(lista, ruta);
    else
        GestorCSV.Exportar(lista, ruta);
    Console.WriteLine("Exportadas " + lista.Count + " películas.");
}

void MenuExportar(List<Pelicula> pelis, string rutaSalida, bool aXml)
{
    int opcion;
    do
    {
        Console.WriteLine();
        Console.WriteLine("1. Exportar todo el documento");
        Console.WriteLine("2. Exportar por año");
        Console.WriteLine("3. Exportar por género");
        Console.WriteLine("4. Exportar por lenguaje");
        Console.WriteLine("0. Volver");
        Console.Write("Elige una opción: ");
        int.TryParse(Console.ReadLine(), out opcion);

        switch (opcion)
        {
            case 1:
                Guardar(pelis, rutaSalida, aXml);
                break;
            case 2:
                Console.Write("Año (se exportan ese año y los posteriores): ");
                if (int.TryParse(Console.ReadLine(), out int anio))
                    Guardar(pelis.Where(p => p.Anio >= anio).ToList(), rutaSalida, aXml);
                else
                    Console.WriteLine("Año no válido");
                break;
            case 3:
                var generos = pelis.Select(p => p.Genre).Distinct().OrderBy(g => g).ToList();
                for (int i = 0; i < generos.Count; i++)
                    Console.WriteLine((i + 1) + ". " + generos[i]);
                Console.Write("Elige un género (número): ");
                if (int.TryParse(Console.ReadLine(), out int g) && g >= 1 && g <= generos.Count)
                {
                    string generoElegido = generos[g - 1];
                    Guardar(pelis.Where(p => p.Genre == generoElegido).ToList(), rutaSalida, aXml);
                }
                else
                    Console.WriteLine("Género no válido");
                break;
            case 4:
                var idiomas = pelis.Select(p => p.Language).Distinct().OrderBy(l => l).ToList();
                for (int i = 0; i < idiomas.Count; i++)
                    Console.WriteLine((i + 1) + ". " + idiomas[i]);
                Console.Write("Elige un idioma (número): ");
                if (int.TryParse(Console.ReadLine(), out int l) && l >= 1 && l <= idiomas.Count)
                {
                    string idiomaElegido = idiomas[l - 1];
                    Guardar(pelis.Where(p => p.Language == idiomaElegido).ToList(), rutaSalida, aXml);
                }
                else
                    Console.WriteLine("Idioma no válido");
                break;
            case 0:
                break;
            default:
                Console.WriteLine("Opción no válida");
                break;
        }
    } while (opcion != 0);
}