using System;
using AutoMapper;
using TodoApi.Models;

namespace TodoApi.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            // Source -> Target
            CreateMap<Country, CountryReadDto>();
            CreateMap<CountryCreateDto, Country>();
            CreateMap<CountryUpdateDto, Country>();
            CreateMap<Country, CountryUpdateDto>();
        }
    }
}