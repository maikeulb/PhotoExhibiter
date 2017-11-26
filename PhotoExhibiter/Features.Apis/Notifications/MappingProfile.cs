using AutoMapper;
using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Features.Api.Notifications
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Notification, Notifications.Dto>();
            CreateMap<ApplicationUser, Notifications.UserDto>();
            CreateMap<Exhibit, Notifications.ExhibitDto>();
            CreateMap<Genre, Notifications.GenreDto>();
        }
    }
}