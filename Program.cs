//sistema de inventario modulo 4 completo
using InventarioApp.Models;
using InventarioApp.Services;
using InventarioApp.Repositorios;
using InventarioApp.factories;
using System.Diagnostics.Contracts;


var servicio = new InventarioServices();
bool activo = true;
//var repositorio = new InMemoryProductosRepository();
// bool activo = true;

Console.WriteLine("==================================");
Console.WriteLine(" SISTEMA DE GESTION DE INVENTARIO ");
Console.WriteLine("      MODULO 4: LINQ Y PATRONES   ");
Console.WriteLine("==================================");


while (activo)
{
  MostrarMenu();
  string opcion = Console.ReadLine() ?? "";

  switch (opcion)
  {
    case "1":
      AgregarProducto();
      break;
    case "2":
      ListarProductos();
      break;
    case "3":
      BuscarPorId();
      break;
    case "4":
      EliminarProducto();
      break;
    case "5":
      BuscarPorCategoria();
      break;
    case "6":
      MostrarResumen();
      break;
    case "7":
      MostrarStockBajo(); break;
    case "8":
      MostrarEstadisticas(); break;
    case "9":
      ExportarCSV();
      break;
    case "10":
      activo = false;
      Console.WriteLine("\n¡Hasta Luego!");
      break;
    default:
      Console.WriteLine("Opción inválida. Intente nuevamente.");
      break;
  }

  void MostrarMenu()
  {
    Console.WriteLine("===SISTEMA DE INVENTARIO===");
    Console.WriteLine("1. Agregar producto");
    Console.WriteLine("2. Listar productos");
    Console.WriteLine("3. Buscar por ID");
    Console.WriteLine("4. Eliminar producto");
    Console.WriteLine("5. Buscar productos por categoría");
    Console.WriteLine("6. Ver resumen");
    Console.WriteLine("7. Ver stock bajo");
    Console.WriteLine("8. Ver estadisticas");
    Console.WriteLine("9. Exportar CSV");
    Console.WriteLine("10. Salir");
    Console.Write("\nSelecciona: ");
  }

  void AgregarProducto()
  {
    Console.Write("Nombre: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Precio : ");
    decimal precio = decimal.Parse(Console.ReadLine() ?? "0");

    Console.Write("Cantidad: ");
    int cantidad = int.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine("\nCategorías: Electrónica, Ropa, Alimentos, Hogar, Deportes, Libros, Muebles, Otros");
    Console.Write("Categoría: ");
    string categoriaStr = Console.ReadLine() ?? "Otros";

    if (Enum.TryParse<CategoriaProducto>(categoriaStr, ignoreCase: true, out var categoria))
    {
      servicio.AgregarProducto(nombre, precio, cantidad, categoria);
      Console.WriteLine("\n Producto agregado Exitosamente");
    }
    else
    {
      Console.WriteLine("\nCategoría no valida");
    }
  }

  void ListarProductos()
  {
    var productos = servicio.ObtenerTodosLosProductos();
    if (!productos.Any())
    {
      Console.WriteLine("\n No hay productos");
      return;
    }
    Console.WriteLine("=====PRODUCTOS=====");
    foreach (var producto in productos)
    {
      Console.WriteLine($"ID: {producto.Id} | Nombre: {producto.Nombre} | Precio: ${producto.Precio:F2} | Cantidad: {producto.Cantidad} | Total: ${producto.ValorTotal} | {producto.Categoria}");
    }

  }

  void BuscarPorId()
  {
    Console.Write("\nID del producto: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    var producto = servicio.ObtenerProductoPorId(id);
    if (producto != null)
    {
      Console.WriteLine($"\nID: {producto.Id}");
      Console.WriteLine($"Nombre: {producto.Nombre}");
      Console.WriteLine($"Precio: ${producto.Precio:F2}");
      Console.WriteLine($"Cantidad: {producto.Cantidad}");
      Console.WriteLine($"Valor total: ${producto.ValorTotal:F2}");
      Console.WriteLine($"Categoria: {producto.Categoria}");
    }
    else
    {
      Console.WriteLine("\nProducto no encontrado");
    }
  }

  void EliminarProducto()
  {
    Console.WriteLine("\nId del producto a eliminar:  ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    var producto = servicio.ObtenerProductoPorId(id);
    if (producto != null)
    {
      servicio.EliminarProducto(id);
      Console.WriteLine("\n Producto eliminado");
    }
    else
    {
      Console.WriteLine("Producto no encontrado");
    }
  }

  void BuscarPorCategoria()
  {
    Console.WriteLine("\nCategorias: Electronica, Ropa, Alimentos, Hogar, Deportes, Libros, Muebles, Otros");
    Console.Write("Categoria: ");
    string categoriaStr = Console.ReadLine() ?? "Otros";

    if (Enum.TryParse<CategoriaProducto>(categoriaStr, ignoreCase: true, out var categoria))
    {

      var productos = servicio.BuscarPorCategoria(categoria);
      if (!productos.Any())
      {
        Console.WriteLine("\nNo hay productos en esta categoria");
        return;
      }

      Console.WriteLine($"===PRODUCTOS EN CATEGORIA: {categoria} ===");
      foreach (var producto in productos)
      {
        Console.WriteLine($"ID: {producto.Id} | {producto.Nombre} | ${producto.Precio} | Cantidad : {producto.Cantidad}");
      }
    }
    else
    {
      Console.WriteLine("\nCategoria no valida");
    }
  }

  void MostrarResumen()
  {
    var resumen = servicio.GenerarResumen();
    Console.WriteLine($"{resumen}");
  }

  void MostrarStockBajo()
  {
    var reporte = servicio.GenerarReporteStockBajo();
    Console.WriteLine($"{reporte}");
  }

  void MostrarEstadisticas()
  {
    Console.WriteLine("\n===ESTADISTICAS===");
    Console.WriteLine($"Valor total del inventario : ${servicio.ObtenerValorTotalInventario()}");
    Console.WriteLine($"Precio promedio: ${servicio.ObtenerPrecioPromedio()}");

    var masCaro = servicio.ObtenerProductoMasCaro();
    if (masCaro != null)
    {
      Console.WriteLine($"Producto más caro: {masCaro.Nombre} - ${masCaro.Precio:F2}");
    }
  }

  void ExportarCSV()
  {
    string csv = servicio.ExportarCSV();
    Console.WriteLine($"\n{csv}");
  }
}  

