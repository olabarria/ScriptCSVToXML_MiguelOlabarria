public class Pelicula
{
    public string Title {get ; set; }
    public string Genre {get ; set; }
    public string Premiere{ get ; set;}
    public int Runtime { get; set;}
    public double ImdbScore { get; set;}
    public string Language {get; set;}


    public Pelicula (string title, string genre, string premiere, int runtime, double imdbScore, string language)
    {
        Title=title;
        Genre = genre;
        Premiere = premiere;
        Runtime = runtime;
        ImdbScore = imdbScore;
        Language = language;
    }

    public int Anio
{
    get{
        string p = Premiere.Trim();
        return int.Parse(p.Substring(p.Length - 4));
    }
}
}
