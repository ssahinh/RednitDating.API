using System.Linq;
using AutoMapper;
using RednitDating.Api.DTOs;
using RednitDating.Api.Models;

namespace RednitDating.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsProfile).Url); 
                })
                .ForMember(d => d.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                 });

            CreateMap<User, UserForDetailedDto>()
                .ForMember(d => d.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault( p => p.IsProfile).Url);
                })
                .ForMember(d => d.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                 });

            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();

        }
    }
    
}