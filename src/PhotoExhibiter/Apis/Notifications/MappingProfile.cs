using AutoMapper;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Apis.Notifications
{
    public class MappingProfile : Profile
    {
        public MappingProfile ()
        {
            CreateMap<Notification, Notifications.Dto> ();
            CreateMap<ApplicationUser, Notifications.UserDto> ();
            CreateMap<Exhibit, Notifications.ExhibitDto> ();
            CreateMap<Genre, Notifications.GenreDto> ();
        }
    }
}
