using System.Globalization;
using System.Xml.Linq;

public static class GestorXML
{
    public static void Exportar(List<Pelicula> peliculas, string rutaXml)
    {
        var raiz = new XElement("Peliculas");

        foreach (var p in peliculas)
        {
            var elemento = new XElement("Pelicula",
                new XElement("Title", p.Title),
                new XElement("Genre", p.Genre),
                new XElement("Premiere", p.Premiere),
                new XElement("Runtime", p.Runtime),
                new XElement("IMDBScore", p.ImdbScore.ToString(CultureInfo.InvariantCulture)),
                new XElement("Language", p.Language));

            raiz.Add(elemento);
        }

        var documento = new XDocument(raiz);
        documento.Save(rutaXml);
    }

    public static List<Pelicula> Leer(string rutaXml)
    {
        var lista = new List<Pelicula>();
        XDocument doc = XDocument.Load(rutaXml);

        foreach (var e in doc.Root!.Elements("Pelicula"))
        {
            lista.Add(new Pelicula(
                e.Element("Title")!.Value,
                e.Element("Genre")!.Value,
                e.Element("Premiere")!.Value,
                int.Parse(e.Element("Runtime")!.Value),
                double.Parse(e.Element("IMDBScore")!.Value, CultureInfo.InvariantCulture),
                e.Element("Language")!.Value));
        }
        return lista;
    }
}