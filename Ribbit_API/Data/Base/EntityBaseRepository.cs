using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ribbit_API.DBContext;
using Ribbit_API.ExceptionHandling;
using System.Linq.Expressions;

namespace Ribbit_API.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;

        public EntityBaseRepository(AppDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Agrega un nuevo registro a la tabla T de la base de datos
        /// </summary>
        /// <param name="entity">Objeto a insertar</param>
        public async Task AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Elimina un registro de una tabla T en la base de datos
        /// </summary>
        /// <param name="id">Elemento a eliminar</param>
        public async Task DeleteAsync(int id)
        {

            var entity = await _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) throw new ProductNotFoundException();

            try
            {
                EntityEntry entry = _context.Entry<T>(entity);
                entry.State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Listar todos los elementos de una tabla T
        /// </summary>
        /// <returns>List<T> elementos</returns>
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        /// <summary>
        /// Listar los elementos de una tabla filtrando con Lambda
        /// </summary>
        /// <param name="predicate">Filtro a aplicar</param>
        /// <returns>List<T> elementos</returns>
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, bool>>[] predicate)
        {
            IQueryable<T> query = _context.Set<T>();
            query = predicate.
                        Aggregate(
                            query,
                            (current, property) => current.Include(property)
                        );
            return await query.ToListAsync();
        }

        /// <summary>
        /// Obtener un elemento especifico
        /// </summary>
        /// <param name="id">Valor a buscar</param>
        /// <returns>Elemento encontrado</returns>
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(T => T.Id == id);

        /// <summary>
        /// Actualizar un elemento de la base de datos
        /// </summary>
        /// <param name="id">Identificador del elemento</param>
        /// <param name="entity">Nuevo valor</param>
        public async Task UpdateAsync(int id, T entity)
        {

            EntityEntry entry = _context.Entry<T>(entity);

            if (id != entity.Id) { throw new ProductNotMatchException(); }

            try
            {
                entry.State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }
    }
}
