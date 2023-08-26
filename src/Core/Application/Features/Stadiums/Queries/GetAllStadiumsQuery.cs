using Application.Features.Stadiums.Dtos;
using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Stadiums.Queries;

public record GetAllStadiumsQuery : IRequest<List<GetAllStadiumsDto>>;

internal class GetAllStadiumsQueryHandler : IRequestHandler<GetAllStadiumsQuery, List<GetAllStadiumsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllStadiumsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<GetAllStadiumsDto>> Handle(GetAllStadiumsQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<Stadium>()
            .Entities.ProjectTo<GetAllStadiumsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}