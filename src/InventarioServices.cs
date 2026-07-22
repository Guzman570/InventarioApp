using InventarioApp.Models;
using InventarioApp.Infraestructure;
using InventarioApp.Repositorios;
using InventarioApp.factories;

namespace InventarioApp.Services;

public class InventarioServices
{
    private readonly InMemoryProductosRepository _repository;
    private readonly JsonInventarioStorage _storage;
    private readonly string _rutaInventario;

    public InventarioServices(string rutaoInventario = "Inventario.json")
    {
        _repository = new InMemoryProductosRepository();
        _storage = new JsonInventarioStorage();
        _rutaInventario = rutaoInventario;

        CargarInventario();
    }

    private void CargarInventario()
    {
        if (File.Exists(_rutaInventario))
        {
            var productos = _storage.Cargar(_rutaInventario);
            foreach (var producto in productos)
            {
                _repository.Agregar(producto);
            }
        }
    }

    public void AgregarProducto(string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
    {
        var producto = ProductoFactory.Crear(nombre, precio, cantidad, categoria);
        _repository.Agregar(producto);
        _Persistir();
    }

    public IEnumerable<Producto> ObtenerTodosLosProductos()
    {
        return _repository.ObtenerTodos();
    }

    public Producto? ObtenerProductoPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }

    public void ActualizarProducto(int id, string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
    {
        var producto = _repository.ObtenerPorId(id);
        if (producto != null)
        {
            producto.Nombre = nombre;
            producto.Precio = precio;
            producto.Cantidad = cantidad;
            producto.Categoria = categoria;
            _repository.Actualizar(producto);
            _Persistir();
        }
    }

    public void EliminarProducto(int id)
    {
        _repository.Eliminar(id);
        _Persistir();
    }

    public IEnumerable<Producto>BuscarPorCategoria(CategoriaProducto categoria)
    {
    return _repository.BuscarPorCategoria(categoria);    
    }

    public IEnumerable<Producto>BuscarPorNombre(string nombre)
    {
        return _repository.BuscarPorNombre(nombre);
    }

    public IEnumerable<Producto>ObtenerProductoBajoStock(int minimo = 5)
    {
        return _repository.ObtenerStockBajo(minimo);
    }

    public decimal ObtenerValorTotalInventario()
    {
        return _repository.ObtenerValorTotalInventario();
    }
    

    public decimal ObtenerPrecioPromedio()
    {
        return _repository.ObtenerPrecioPromedio();
    }

    public Producto? ObtenerProductoMasCaro()
    {
        return _repository.ObtenerProductoMasCaro();
    }

    //METODOS REPORTES

    public string GenerarResumen()
    {
        var productos = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(productos);
        return generador.GenerarResumen();
    }

    public string GenerarReporteStockBajo(int minimo = 5)
    {
        var productos = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(productos);
        return generador.GenerarReporteStockBajo(minimo);
    }

    public string GenerarTopProductos(int top = 5)
    {
        var productos = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(productos);
        return generador.GenerarTopProductos(top);
    }

    public string ExportarCSV()
    {
        var productos = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(productos);
        return generador.ExportarCSV();
    }

    private void _Persistir()
    {
        _storage.CrearBackup(_rutaInventario);
        var productos = _repository.ObtenerTodos().ToList();
        _storage.Guardar(productos, _rutaInventario);
    }


}