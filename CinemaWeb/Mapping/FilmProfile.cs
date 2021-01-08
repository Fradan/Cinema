using AutoMapper;
using Core;

namespace CinemaWeb.Mapping
{
    public class FilmProfile : Profile
    {
        public FilmProfile()
        {
            CreateMap<FilmViewModel, Film>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(x => x.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
