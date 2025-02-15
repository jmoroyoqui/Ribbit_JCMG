namespace Ribbit_API.ExceptionHandling
{
    public class ProductNotMatchException : Exception
    {
        public ProductNotMatchException() : base("No esta permitido modificar el id de un producto.")
        {
            
        }
    }
}
