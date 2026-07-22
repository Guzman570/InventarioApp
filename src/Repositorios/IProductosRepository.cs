using InventarioApp.Models;

namespace InventarioApp.Repositorios;

/// <summary>
/// Contrato para el repositorio de productos.
/// Define las operaciones básicas del almacenamiento.
/// </summary>
public interface IProductosRepository
{
    /// <summary>Agrega un producto al repositorio.</summary>
    void Agregar(Producto producto);

    /// <summary>Define un producto por su ID.</summary>
    Producto? ObtenerPorId(int id);

    /// <summary>Obtiene todos los productos.</summary>
    IEnumerable<Producto> ObtenerTodos();

    /// <summary>Actualiza un producto existente.</summary>
    bool Actualizar(Producto producto);

    /// <summary>Elimina un producto por su ID.</summary>
    bool Eliminar(int id);

    /// <summary>Busca productos cuyo nombre coincida parcialmente.</summary>
    IEnumerable<Producto> BuscarPorNombre(string nombre);

    /// <summary>Obtiene los nombres de todos los productos.</summary>
    IEnumerable<string> ObtenerNombres();

    /// <summary>Cantidad total de productos en el repositorio.</summary>
    int Cantidad { get; }
}