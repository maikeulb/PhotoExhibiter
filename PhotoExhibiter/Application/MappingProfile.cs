using System;
using AutoMapper;
using PhotoExhibiter.Application.Commands;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile ()
        {
            CreateMap<Edit.Command, Exhibit> ()
                .ForMember (dest => dest.DateTime,
                    opts => opts.MapFrom (
                        src => DateTime.Parse (string.Format ("{0} {1}",
                            src.Date,
                            src.Time))))
                .ForAllOtherMembers(x => x.Ignore());
            CreateMap<Create.Command, Exhibit> ()
                .ForMember (dest => dest.DateTime,
                    opts => opts.MapFrom (
                        src => DateTime.Parse (string.Format ("{0} {1}",
                            src.Date,
                            src.Time))))
                .ForMember (dest => dest.PhotographerId,
                    opts => opts.MapFrom (
                        src => src.UserId))
                .ForAllOtherMembers(x => x.Ignore());
            CreateMap<Follow.Command, Following> ()
                .ForMember (dest => dest.FollowerId,
                    opts => opts.MapFrom (
                        src => src.UserId))
                .ForAllOtherMembers(x => x.Ignore());
            CreateMap<Attend.Command, Attendance> ()
                .ForMember (dest => dest.AttendeeId,
                    opts => opts.MapFrom (
                        src => src.UserId))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}
