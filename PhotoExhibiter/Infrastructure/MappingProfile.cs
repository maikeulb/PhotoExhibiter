using System;
using AutoMapper;
using PhotoExhibiter.Domain.Models;
using PhotoExhibiter.Domain.Commands;
using PhotoExhibiter.Domain.Queries;
using PhotoExhibiter.WebApi.Commands;

namespace PhotoExhibiter.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Edit.Command, Exhibit> ()
                .ForMember (dest => dest.DateTime,
                            opts => opts.MapFrom(
                              src => DateTime.Parse (string.Format ("{0} {1}", 
                                  src.Date, 
                                  src.Time))));
            CreateMap<Create.Command, Exhibit> ()
                .ForMember (dest => dest.DateTime,
                            opts => opts.MapFrom(
                              src => DateTime.Parse (string.Format ("{0} {1}", 
                                  src.Date, 
                                  src.Time))))
                .ForMember (dest => dest.PhotographerId,
                            opts => opts.MapFrom(
                              src => src.UserId));
            CreateMap<Follow.Command, Following> ()
                .ForMember (dest => dest.FollowerId,
                            opts => opts.MapFrom(
                                src => src.UserId));
            CreateMap<Attend.Command, Attendance> ()
                .ForMember (dest => dest.AttendeeId,
                            opts => opts.MapFrom(
                                src => src.UserId));
        }
    }
}
