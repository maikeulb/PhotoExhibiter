namespace PhotoExhibiter.WebApi.Commands
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PhotoExhibiter.Domain.Commands;
    using PhotoExhibiter.Domain.Models;
    using PhotoExhibiter.Domain.Interfaces;
    using PhotoExhibiter.Domain.Queries;
    using PhotoExhibiter.Infrastructure.Repositories;
    using PhotoExhibiter.WebUI.Controllers;
    using PhotoExhibiter.WebUI.ViewModels;

    public class Attend
    {
        public class Command : IRequest
        {
            public string UserId { get; set; }
            public int ExhibitId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IAttendanceRepository _repository;
            private readonly IMapper _mapper;

            public Handler(IAttendanceRepository repository,
                    IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public void Handle(Command message)
            {
                var attendance = _mapper.Map<Command, Attendance>(message);

                _repository.Add (attendance);
                _repository.SaveAll ();
            }
        }
    }
}

