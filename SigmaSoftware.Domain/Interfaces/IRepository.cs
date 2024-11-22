namespace SigmaSoftware.Domain.Interfaces
{

    /// <summary>
    /// Defines a generic repository interface for managing entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity managed by the repository.</typeparam>
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

        /// <summary>
        /// Retrieves an entity by its email asynchronously.
        /// </summary>
        /// <param name="email">The email associated with the entity to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains the entity if found; otherwise, null.
        /// </returns>
        Task<TEntity?> GetByEmailAsync(string email);

    }

}