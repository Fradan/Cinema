﻿using AutoMapper;
using Core;

namespace CinemaWeb
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CinemaViewModel, Cinema>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}