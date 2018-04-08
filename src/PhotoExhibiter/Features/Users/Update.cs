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
using PhotoExhibiter.Features;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure;

namespace PhotoExhibiter.Features.Users
{
    public class Update
    {
        public class Command : IRequest<Result>
        {
            public string Id { get; set; }
            public string ImageUrl { get; set; }
            public string ImageName { get; set; }
            public IFormFile ImageUpload { get; set; }
        }

        public class CommandHandler : IAsyncRequestHandler<Command, Result>
        {
            private readonly IApplicationUserRepository _repository;
            private readonly IHostingEnvironment _environment;
            private readonly ILogger _logger;

            public CommandHandler(IHostingEnvironment environment,
                    IApplicationUserRepository repository,
                    ILogger<CommandHandler> logger)
            {
                _environment = environment;
                _repository = repository;
                _logger = logger;
            }

            public async Task<Result> Handle (Command message)
            {
                var applicationUser = _repository.GetPhotographer (message.Id);

                if (applicationUser == null)
                    return Result.Fail<Command> ("User does not exit");

                var uploadPath = Path.Combine (_environment.WebRootPath, "images/exhibits");
                var ImageName = ContentDispositionHeaderValue.Parse (message.ImageUpload.ContentDisposition).FileName.Trim ('"');
                using (var fileStream = new FileStream (Path.Combine (uploadPath, message.ImageUpload.FileName), FileMode.Create))
                {
                    await message.ImageUpload.CopyToAsync (fileStream);
                    message.ImageUrl = "http://exhibitbaseurl/images/exhibits/" + ImageName;
                }

                applicationUser.ImageUrl = message.ImageUrl;
                _repository.SaveAll ();

                return Result.Ok (message);
            }
        }
    }
}
