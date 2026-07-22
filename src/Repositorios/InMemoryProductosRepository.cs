namespace InventarioApp.Repositorios;

using InventarioApp.Models;

/// <summary>
/// Implementacion en memoria deol repositorio de productos.
/// Usa dictionary oara acceso 0(1) por ID
/// Incluye metodos LINQ para b usqeudas y agregaciones.
/// </summary>
public class InMemoryProductosRepository : IProductosRepository
{
    private readonly Dictionary<int, Producto> _productos = new();

    public int Cantidad => _productos.Count;

    //================
    // CRUD basico
    //================

    public void Agregar (Producto producto)
    {
        _productos[producto.Id] = producto;
    }

    public Producto? ObtenerPorId(int id)
    {
        return _productos.GetValueOrDefault(id);
    }

    public IEnumerable <Producto> ObtenerTodos()
    {
        return _productos.Values;
    }

    public bool Actualizar(Producto producto)
    {
        if (!_productos.ContainsKey(producto.Id))
            return false;

        _productos[producto.Id] = producto;
        return true;
    }

    public bool Eliminar(int id)
    {
        return _productos.Remove(id);
    }

    //====================
    // Busquedas con LINQ
    //====================

    public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
    {
        return _productos.Values.Where(p => p.Categoria == categoria);
    }

    public IEnumerable<Producto> BuscarPorNombre(string nombre)
    {
        return _productos.Values
        .Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Producto> BuscarPorRangoPrecio (decimal min, decimal max)
    {
        return _productos.Values
        .Where(p => p.Precio >= min && p.Precio <= max);
    }

    public IEnumerable<string> ObtenerPorNombre()
    {
        return _productos.Values.Select(p => p.Nombre);
    }

    public IEnumerable<string> ObtenerNombres()
    {
        return ObtenerPorNombre();
    }

    public bool HayStockBajo(int minimo = 5)
    {
        return  _productos.Values.Any(p => p.Cantidad < minimo);
    }   

    //====================
    // LINQ Avanzado
    //====================

    public IEnumerable<Producto> ObtenerOrdenadosPorPrecio (bool descendente = false)
    {
        return descendente
        ? _productos.Values.OrderByDescending(p => p.Precio)
        : _productos.Values.OrderBy(p => p.Precio);
    } 

    public IEnumerable<Producto> ObtenerTopPorPrecio (int cantidad =5)
    {
        return _productos.Values
        .OrderByDescending(p => p.Precio)
        .Take(cantidad);
    }

    public Dictionary<CategoriaProducto, List <Producto>> AgruparPorCategoria()
    {
    return _productos.Values
        .GroupBy(p => p.Categoria)
        .ToDictionary(g => g.Key, g => g.ToList());
    }

    public Dictionary <CategoriaProducto, int> ContarPorCategoria()
    {
        return _productos.Values
        .GroupBy(p => p.Categoria)
        .ToDictionary(g => g.Key, g => g.Count());
    }

    public decimal ObtenerValorTotalInventario()
    {
        return _productos.Values.Sum(p => p.Precio * p.Cantidad);
    }

    public decimal ObtenerPrecioPromedio()
    {
        return _productos.Values.Any()
        ? _productos.Values.Average(p => p.Precio)
        : 0m;
    }

    public Producto? ObtenerProductoMasCaro()
    {
        return _productos.Values
        .OrderByDescending(p => p.Precio)
        .FirstOrDefault();
    }

    public Dictionary<CategoriaProducto, decimal> ObtenerValorTotalPorCategoria()
    {
        return _productos.Values
        .GroupBy(p => p.Categoria)
        .ToDictionary(
            g => g.Key, 
            g => g.Sum(p => p.Precio * p.Cantidad));
    }

    public IEnumerable<Producto> ObtenerStockBajo(int minimo = 5)
    {
        return _productos.Values
        .Where(p => p.Cantidad < minimo)
        .OrderBy(p => p.Cantidad);
    }
}