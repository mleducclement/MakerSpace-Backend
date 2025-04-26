using MakerSpace.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Infrastructure.Data.Repositories.Interfaces;

/// <summary>
/// Defines a generic repository interface for managing entities.
/// </summary>
/// <typeparam name="T">The type of entity managed by the repository.</typeparam>
public interface IRepository<T> where T : class, IEntity {
   Task<IReadOnlyList<T>> GetAllAsync();
   
   Task<T?> GetByIdAsync(Guid id);
   
   /// <summary>
   /// Fetches multiple entities matching the provided list of IDs.
   /// Returns only the entities that exist; missing IDs are ignored.
   /// </summary>
   /// <param name="ids">A list of unique entity IDs to fetch.</param>
   /// <returns>A list of matching entities. May be empty if no IDs match.</returns>
   Task<List<T>> GetManyByIdsAsync(List<Guid> ids);
   
   /// <summary>
   /// Fetches a single entity by its unique ID.
   /// Throws an InvalidOperationException if the entity is not found.
   /// </summary>
   /// <param name="id">The unique identifier of the entity to fetch.</param>
   /// <returns>The matching entity.</returns>
   /// <exception cref="InvalidOperationException">Thrown if the entity is not found.</exception>
   Task<T> GetRequiredByIdAsync(Guid id);
   
   Task<T> CreateAsync(T entity);
   
   /// <summary>
   /// Persists a batch of new entities to the database atomically.
   /// If any entity is invalid or causes a database constraint violation,
   /// the entire operation fails and no entities are created.
   /// </summary>
   /// <param name="entities">The collection of entities to create.</param>
   /// <returns>The list of created entities with any database-generated values populated.</returns>
   /// <exception cref="DbUpdateException">Thrown if any entity cannot be saved.</exception>
   /// <exception cref="DbUpdateConcurrencyException">Thrown if a concurrency violation is detected during save.</exception>
   Task<IReadOnlyList<T>> CreateManyAsync(IReadOnlyList<T> entities);
   
   Task UpdateAsync(T entity);
   
   Task DeleteAsync(Guid id);
}