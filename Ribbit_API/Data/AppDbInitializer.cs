using Microsoft.EntityFrameworkCore;
using Ribbit_API.DBContext;

namespace Ribbit_API.Data
{
    public class AppDbInitializer
    {
        /// <summary>
        /// Ingesta registros iniciales a la base de datos si esta vacia.
        /// </summary>
        /// <param name="app">Instancia de WebApplication</param>
        public static void CreateDatabase(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.Migrate();

                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                            new Models.Product
                            {
                                Nombre = "Amazon Echo Dot 5ta. Gen",
                                Descripcion = "Amazon Echo Dot 5ta. Generacion compatible con Alexa",
                                Precio = 999.0m,
                                Stock = 10,
                                FechaCreacion = DateTime.Now
                            },
                            new Models.Product
                            {
                                Nombre = "Samsung Galaxy S24 Ultra",
                                Descripcion = "Pantalla AMOLED 6.9\" Gorilla Glass Victus",
                                Precio = 25000.0m,
                                Stock = 5,
                                FechaCreacion = DateTime.Now
                            },
                            new Models.Product
                            {
                                Nombre = "HotWheels 370z",
                                Descripcion = "Nissan 370z NISMO",
                                Precio = 32.5m,
                                Stock = 20,
                                FechaCreacion = DateTime.Now
                            }
                        );
                    context.SaveChanges();
                }
            }
        }
    }
}
