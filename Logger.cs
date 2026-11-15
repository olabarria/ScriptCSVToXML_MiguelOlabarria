using System.Diagnostics;


public static class Logger
{
    private const string Origen = "ScriptCSVToXML";   
    private const string NombreLog = "NetflixLog";  

    private static bool disponible;

    static Logger()
    {
        try
        {
            
            if (!EventLog.SourceExists(Origen))
                EventLog.CreateEventSource(Origen, NombreLog);
            disponible = true;
        }
        catch (Exception e)
        {
            disponible = false;
            Console.WriteLine("[AVISO] No se pudo crear el log de Windows (" + e.Message +
                              "). Ejecuta Visual Studio como administrador la primera vez.");
        }
    }

    public static void Info(string mensaje)
    {
        Escribir(mensaje, EventLogEntryType.Information);
    }

    public static void Error(string mensaje)
    {
        Escribir(mensaje, EventLogEntryType.Error);
    }

    private static void Escribir(string mensaje, EventLogEntryType tipo)
    {
        if (!disponible) return;
        EventLog.WriteEntry(Origen, mensaje, tipo);
    }
}

