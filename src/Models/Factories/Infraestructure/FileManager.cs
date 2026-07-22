using System.Net.Mime;

namespace InventarioApp.Infraestructure;


public class FileManager
{
    public void Escribir (string ruta, string contenido)
    {
        File.WriteAllText(ruta, contents: contenido);
    }

    public string Leer (string ruta)
    {
        return File.ReadAllText(ruta);
    }

    public void Agregar (string ruta, string contenido)
    {
        File.AppendAllText(path: ruta, contents : contenido);
    }

    public bool Existe (string ruta)
    {
        return File.Exists(ruta);
    }

    public void Eliminar (string ruta)
    {
        File.Delete(ruta);
    }

    public string[] LeerLineas (string ruta)
    {
        return File.ReadAllLines(ruta);
    }

    public void EscribirLineas(string ruta, IEnumerable<string> lineas)
    {
        File.WriteAllLines (ruta, lineas);
    }

    public void CrearDirectorio(string ruta)
    {
        Directory.CreateDirectory(ruta);
    }

    public string[] ObtenerArchivos(string directorio, string patron = "*")
    {
        return Directory.GetFiles(path: directorio, searchPattern: patron);
    }


}