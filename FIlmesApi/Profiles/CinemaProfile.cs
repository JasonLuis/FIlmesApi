using AutoMapper;
using FIlmesApi.Data.Dtos;
using FIlmesApi.Models;

namespace FIlmesApi.Profiles
{
    public class CinemaProfile: Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<Cinema, ReadCinemaDto>()
                // Indicando ao sistema a fonte para preencher o campo ReadEnderecoDto
                .ForMember(
                    cinemaDto => cinemaDto.Endereco,
                    opt => opt.MapFrom(cinema => cinema.Endereco)
                );
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}
