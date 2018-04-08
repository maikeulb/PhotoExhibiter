using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.ManageUsers
{
    public class Edit
    {
        public class Query : IRequest<Result<Command>>
        {
            public string Id { get; set; }
        }

        public class Command : IRequest<Result>
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public bool IsSuspended { get; set; }

            public IFormFile ImageUpload { get; set; }
            public string ImageName { get; set; }
            public string ImageUrl { get; set; }

        }

        public class QueryHandler : IRequestHandler<Query, Result<Command>>
        {
            private readonly IApplicationUserRepository _repository;

            public QueryHandler (IApplicationUserRepository repository)
            {
                _repository = repository;
            }

            public Result<Command> Handle (Query message)
            {
                var user = _repository.GetPhotographer (message.Id);

                if (user == null)
                    return Result.Fail<Command> ("User does not exit");

                var command = new Command
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    IsSuspended = user.IsSuspended
                };

                return Result.Ok (command);
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator ()
            {
                RuleFor (m => m.Name)
                    .NotNull ().WithMessage ("Name is required.");
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command, Result>
        {
            private readonly IApplicationUserRepository _repository;
            private readonly IHostingEnvironment _environment;

            public CommandHandler(IHostingEnvironment environment, 
                    IApplicationUserRepository repository)
            {
                _environment = environment;
                _repository = repository;
            }

            public async Task<Result> Handle (Command message)
            {
                var user = _repository.GetPhotographerWithExhibits (message.Id);

                if (user == null)
                    return Result.Fail<Command> ("User does not exit");

                var uploadPath = Path.Combine (_environment.WebRootPath, "images/exhibits");
                var ImageName = ContentDispositionHeaderValue.Parse (message.ImageUpload.ContentDisposition).FileName.Trim ('"');
                using (var fileStream = new FileStream (Path.Combine (uploadPath, message.ImageUpload.FileName), FileMode.Create))
                {
                    await message.ImageUpload.CopyToAsync (fileStream);
                    message.ImageUrl = "http://exhibitbaseurl/images/exhibits/" + ImageName;
                }

                user.Name = message.Name;
                user.ImageUrl = message.ImageUrl;
                user.IsSuspended = message.IsSuspended;

                if (message.IsSuspended == true)
                    user.Suspend ();

                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
