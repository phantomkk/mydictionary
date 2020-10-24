using AutoMapper;
using MyDictionary.DataAccess.Models;
using MyDictionary.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDictionary.Web.Utils
{
    public class AppProfiles:Profile
    {
        public AppProfiles()
        {
            CreateMap<Word, WordDto>().ForMember(dest => dest.Examples, src => src.MapFrom(x => x.Examples));
            CreateMap<WordDto, Word>();

            CreateMap<Example, ExampleDto>().ForMember(dest=>dest.Words, src=>src.MapFrom(x=>x.Words));
            CreateMap<ExampleDto, Example>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<WordExample, WordExampleDto>();
            CreateMap<WordExampleDto, WordExample>();
        }
    }
}
