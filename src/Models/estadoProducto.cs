namespace InventarioApp.Models;

// <summary>
// Clase de vida de un producto en el inventario
public enum EstadoProducto
{
    /// <summary> Disponible para venta </summary>
    Activo,

    /// <summary> Temporalmente fuera de disponibilidad</summary>
    Inactivo,

    /// <summary> Retirado permanentemente del catalago</summary>
    Descontinuado
}