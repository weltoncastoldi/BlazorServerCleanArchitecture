using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Features;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Features;

public class StadiumRepository : IStadiumRepository
{
    private readonly IGenericRepository<Stadium> _repository;

    public StadiumRepository(IGenericRepository<Stadium> repository)
    {
        _repository = repository;
    }

    public async Task<List<Stadium>> GetStadiumByCityAsync(string cityName)
    {
        return await _repository.Entities.Where(x => x.City == cityName).ToListAsync();
    }
}