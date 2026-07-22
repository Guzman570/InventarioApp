using System.Text.Json;
using System.Text.Json.Serialization;
using InventarioApp.Infraestructure;
using InventarioApp.Models;


namespace InventarioApp.Infraestructure;


public class JsonInventarioStorage
{
    private readonly FileManager _fileManager;
    private readonly JsonSerializerOptions _options;

    public JsonInventarioStorage()
    {
        _fileManager = new FileManager();
        _options = new JsonSerializerOptions

        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() } 
        };
    }

    public void Guardar(List<Producto> productos, string ruta)
    {
        string json = JsonSerializer.Serialize(productos, _options);
        _fileManager.Escribir(ruta, contenido: json);
    }

    public List<Producto> Cargar(string ruta)
    {
        string json = _fileManager.Leer(ruta);
        return JsonSerializer.Deserialize<List<Producto>>(json, _options) ?? new List<Producto>();
    }

    public string? CrearBackup(string ruta)
    {
        if (!_fileManager.Existe(ruta))
            return null;

        string? directorio = Path.GetDirectoryName(ruta);
        string nombreSinExtension = Path.GetFileNameWithoutExtension(ruta);
        string extension = Path.GetExtension(ruta);
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        string rutaBackup = Path.Combine(
            directorio ?? ".",
            $"{nombreSinExtension} _backup{timestamp}{extension}"
        );
        File.Copy(sourceFileName: ruta, destFileName: rutaBackup);
        return rutaBackup;
    }
}
