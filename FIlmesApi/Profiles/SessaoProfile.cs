using AutoMapper;
using FIlmesApi.Data.Dtos;
using FIlmesApi.Models;

namespace FIlmesApi.Profiles;

public class SessaoProfile : Profile
{
    public SessaoProfile()
    {
        CreateMap<CreateSessaoDto, Sessao>();
        CreateMap<Sessao, ReadSessaoDto>();
    }
}
