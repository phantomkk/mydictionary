using AutoMapper;
using MongoDb.DataAccess.Entities;
using MyDictionary.Service.Mongo.Dtos;
using MyDictionary.Services.Dtos;

namespace MyDictionary.Web.Utils
{
    public class AppProfiles:Profile
    {
        public AppProfiles()
        {
            CreateMap<Word, WordDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.ToString()))
                .ForMember(dest => dest.ExamplesIds, src => src.MapFrom(x => x.ExampleIds))
                ;
           // CreateMap<WordDto, Word>();
             
            CreateMap<Example , ExampleDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.ToString()))
                .ForMember(dest => dest.Words, src => src.Ignore ());
          //  CreateMap<ExampleDto, Example>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.ToString()));
           // CreateMap<UserDto, User>();

         //   CreateMap<WordExample, WordExampleDto>();
          //  CreateMap<WordExampleDto, WordExample>();
        }
    }
}
