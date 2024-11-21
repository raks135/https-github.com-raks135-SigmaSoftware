namespace SigmaSoftware.Domain.Interfaces
{

    public interface IRepository<TEntity>
    { 
        /// <summary>
       /// Inserts a new entity asynchronously.
       /// </summary>
       /// <param name="entity">The entity to be inserted.</param>
       /// <returns>A task that represents the asynchronous operation.</returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(TEntity entity);
        Task<TEntity?> GetByEmailAsync(object email);
    }
}