using MakerSpace.Domain.Models;
using MakerSpace.Infrastructure.Data.Repositories.Interfaces;

namespace MakerSpace.Application.Interfaces.Repositories;

public interface ICategoriesRepository : IRepository<Category> { }