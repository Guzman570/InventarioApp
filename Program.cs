
//============================
//SISTEMA DE INVENTARIO - CLASE 1
//ESTADO: mensaje de bienvenida
//============================
using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

Console.WriteLine("========================================");
Console.WriteLine("     SISTEMA DE GESTION DE INVENTARIO   ");
Console.WriteLine("========================================");
Console.WriteLine();
Console.WriteLine($"Version: {version}");
Console.WriteLine($"Plataforma: {Environment.OSVersion}");
Console.WriteLine($".NET version: {Environment.Version}");
Console.WriteLine();
Console.WriteLine("Estructurad del proyecto:");
Console.WriteLine("configuracion .csproj");
Console.WriteLine("carpeta src/ creada");
Console.WriteLine("Metadatos configurados");
Console.WriteLine();
Console.WriteLine("proximo paso: agregar argumentos CLI y configuracion de repositorio en github");
