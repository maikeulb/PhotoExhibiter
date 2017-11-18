namespace PhotoExhibiter.WebApi.Commands
{
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

            public Handler(IAttendanceRepository repository)
            {
                _repository = repository;
            }

            public void Handle(Command message)
            {
                var attendance = new Attendance
                {
                    AttendeeId = message.UserId,
                    ExhibitId = message.ExhibitId
                };
                _repository.Add (attendance);
                _repository.SaveAll ();
            }
        }
    }
}

