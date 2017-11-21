using System;
using AutoMapper;
using PhotoExhibiter.Commands;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile ()
        {
            CreateMap<Edit.Command, Exhibit> ()
                .ForMember (dest => dest.DateTime,
                    opts => opts.MapFrom (
                        src => DateTime.Parse (string.Format ("{0} {1}",
                            src.Date, src.Time))))
                .ForMember (dest => dest.PhotographerId,
                    opts => opts.MapFrom (
                        src => src.UserId))
                .ForMember (dest => dest.Attendances,
                    opts => opts.Ignore ())
                .ForMember (dest => dest.Photographer,
                    opts => opts.Ignore ())
                .ForMember (dest => dest.Genre,
                    opts => opts.Ignore ())
                .ForMember (dest => dest.IsCanceled,
                    opts => opts.Ignore ());

            CreateMap<Create.Command, Exhibit> ()
                .ForMember (dest => dest.DateTime,
                    opts => opts.MapFrom (
                        src => DateTime.Parse (string.Format ("{0} {1}",
                            src.Date, src.Time))))
                .ForMember (dest => dest.PhotographerId,
                    opts => opts.MapFrom (
                        src => src.UserId))
                .ForMember (dest => dest.Attendances,
                    opts => opts.Ignore ())
                .ForMember (dest => dest.Photographer,
                    opts => opts.Ignore ())
                .ForMember (dest => dest.Genre,
                    opts => opts.Ignore ())
                .ForMember (dest => dest.IsCanceled,
                    opts => opts.Ignore ());

            CreateMap<Follow.Command, Following> ()
                .ForMember (dest => dest.FollowerId,
                    opts => opts.MapFrom (
                        src => src.UserId))
                .ForAllOtherMembers (x => x.Ignore ());

            CreateMap<Attend.Command, Attendance> ()
                .ForMember (dest => dest.AttendeeId,
                    opts => opts.MapFrom (
                        src => src.UserId))
                .ForAllOtherMembers (x => x.Ignore ());
        }
    }
}