namespace Ribbit_API.ExceptionHandling
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Producto no encontrado.") { }
        public ProductNotFoundException(int id) : base($"Producto con id: {id} no encontrado en la base de datos.") { }
        public ProductNotFoundException(string message) : base(message) { }
    }
}
