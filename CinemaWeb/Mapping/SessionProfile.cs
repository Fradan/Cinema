using AutoMapper;
using Core;
using System;

namespace CinemaWeb
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<SessionViewModel, Session>()
                .ForMember(x => x.SessionTime, opt => opt.MapFrom(src => 
                    new DateTime(src.SessionTime.Year, 
                                 src.SessionTime.Month, 
                                 src.SessionTime.Day, 
                                 src.SessionTime.Hour, 
                                 src.SessionTime.Minute, 0)))
                .ForMember(x => x.CinemaId, opt => opt.MapFrom(src => src.CinemaId))
                .ForMember(x => x.FilmId, opt => opt.MapFrom(src => src.FilmId))
                .ReverseMap();
        }
    }
}
