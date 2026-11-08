using System.Globalization;
using System.Text.RegularExpressions;

public static class GestorCSV
{
    public static List<Pelicula> Leer(string ruta)
    {
        var lista = new List<Pelicula>();
        string[] lineas = File.ReadAllLines(ruta);

        for(int i = 1 ; i<lineas.Length; i++)
        {
            if(string.IsNullOrWhiteSpace(lineas[i]))continue;
            string[] c = Regex.Split(lineas[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            if (c.Length != 6){
                Console.WriteLine("Línea rara (" + (i + 1) + "): " + lineas[i]);
                continue;
            }

            for(int j = 0; j < c.Length; j++)
            {
                c[j] =  c[j].Trim().Trim('"');
            }

            lista.Add(new Pelicula(c[0], c[1], c[2], int.Parse(c[3]), double.Parse(c[4], CultureInfo.InvariantCulture), c[5]));

        }
        return lista;
    }

    public static void Exportar(List<Pelicula> peliculas, string rutaCsv)
    {
        var lineas = new List<string>();
        lineas.Add("Title,Genre,Premiere,Runtime,IMDB Score,Language");

        foreach (var p in peliculas)
        {
            lineas.Add(Campo(p.Title) + "," +
                       Campo(p.Genre) + "," +
                       Campo(p.Premiere) + "," +
                       p.Runtime + "," +
                       p.ImdbScore.ToString(CultureInfo.InvariantCulture) + "," +
                       Campo(p.Language));
        }

        File.WriteAllLines(rutaCsv, lineas);
    }

    private static string Campo(string texto)
    {
        if (texto.Contains(','))
            return "\"" + texto + "\"";
        return texto;
    }
}