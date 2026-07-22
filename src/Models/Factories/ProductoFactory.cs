namespace InventarioApp.factories;

using InventarioApp.Models;

public static class ProductoFactory
{
    private static int _nextId = 1;

    public static Producto Crear(
        string nombre,
        decimal precio,
        int cantidad,
        CategoriaProducto categoria = CategoriaProducto.Otros)
    {
        //Guard Clauses - Validar Temprano
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("Nombre requerido.", nameof(nombre));
        
        if (precio < 0)
            throw new ArgumentException("Precio no puede ser negativo.", nameof(precio));
        
        if (cantidad < 0)
            throw new ArgumentException("Cantidad no puede ser negativa.", nameof(cantidad));

            return new Producto
            {
                Id = _nextId++,
                Nombre = nombre,
                Precio = precio,
                Cantidad = cantidad,
                Categoria = categoria,
                FechaRegistro = DateTime.Now
            };
    }

    public static Producto CrearConStock(
        string nombre,
        decimal precio,
        int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("CrearConStock requiere una cantidad mayor a cero.", nameof(cantidad));

        return Crear(nombre, precio, cantidad);
    } 
}