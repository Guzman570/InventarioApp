
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
Console.WriteLine("InventarioApp/");
Console.WriteLine(" |-- Program.cs");
Console.WriteLine(" |--InventarioApp.csproj");
Console.WriteLine(" |-- gitignore");
Console.WriteLine(" |-- README.md");
Console.WriteLine(" |-- src/");
Console.WriteLine("     |--Models/ (proxima clase)");
Console.WriteLine(" Configuracion .csproj");
Console.WriteLine("carpeta src/ creada");
Console.WriteLine("Metadatos configurados");
Console.WriteLine();
Console.WriteLine("proximo paso: Checkpint");
