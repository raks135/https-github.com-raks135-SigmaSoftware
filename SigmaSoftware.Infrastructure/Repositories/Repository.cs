using Microsoft.EntityFrameworkCore;
using SigmaSoftware.Domain.Interfaces;

namespace SigmaSoftware.Infrastructure.Repositories
{
    /// <summary>
    /// A generic repository for managing entities in a database context.
    /// Implements the <see cref="IRepository{TEntity}"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that the repository manages.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        /// <summary>
        /// Inserts a new entity into the database.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="entity"/> is null.</exception>
        public virtual async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="entity"/> is null.</exception>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="email">The identifier of the entity to retrieve.</param>
        /// <returns>
        /// The entity if found; otherwise, null.
        /// </returns>
        public virtual async Task<TEntity?> GetByEmailAsync(object email)
        {
            return await _context.Set<TEntity>().FindAsync(email);
        }

    }
}
