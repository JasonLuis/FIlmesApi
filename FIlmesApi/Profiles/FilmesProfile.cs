using AutoMapper;
using FIlmesApi.Data.Dtos;
using FIlmesApi.Models;

namespace FIlmesApi.Profiles;

public class FilmesProfile : Profile
{
    public FilmesProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, UpdateFilmeDto>();
        CreateMap<Filme, ReadFilmeDto>()
            .ForMember(
                filmeDto => filmeDto.Sessoes,
                opt => opt.MapFrom(filme => filme.Sessoes)
            );
    }
}
