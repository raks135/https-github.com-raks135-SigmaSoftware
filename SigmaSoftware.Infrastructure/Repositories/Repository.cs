using Microsoft.EntityFrameworkCore;
using SigmaSoftware.Domain.Interfaces;
using SigmaSoftware.Infrastructure.DBContext;

namespace SigmaSoftware.Infrastructure.Repositories
{
    /// <summary>
    /// A generic repository for managing entities in a database context.
    /// Implements the <see cref="IRepository{TEntity}"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that the repository manages.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The database context to use for the repository.</param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
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
        /// Retrieves an entity by its email.
        /// </summary>
        /// <param name="email">The email address of the entity to retrieve.</param>
        /// <returns>
        /// The entity if found; otherwise, null.
        /// </returns>
        public virtual async Task<TEntity?> GetByEmailAsync(string email)
        {
            return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => EF.Property<string>(e, "Email") == email);
        }

    }
}
