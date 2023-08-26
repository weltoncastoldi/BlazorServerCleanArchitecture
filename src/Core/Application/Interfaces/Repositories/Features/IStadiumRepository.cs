using Domain.Entities;

namespace Application.Interfaces.Repositories.Features;

public interface IStadiumRepository
{
    Task<List<Stadium>> GetStadiumByCityAsync(string cityName);

}