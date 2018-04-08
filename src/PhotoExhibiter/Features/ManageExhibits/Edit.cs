using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure;
using PhotoExhibiter.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace PhotoExhibiter.Features.ManageExhibits
{
    public class Edit
    {
        public class Query : IRequest<Result<Command>>
        {
            public int Id { get; set; }
        }

        public class Command : IRequest<Result>
        {
            public int Id { get; set; }
            public string Genre { get; set; }
            public string Location { get; set; }
            public string Photographer { get; set; }
            public bool IsCanceled { get; set; }

            public string Date { get; set; }
            [Display (Name = "Date")]
            public DateTime DateTime { get; set; }

            public IFormFile ImageUpload { get; set; }
            public string ImageName { get; set; }
            public string ImageUrl { get; set; }

            public IEnumerable<Genre> Genres { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Result<Command>>
        {
            private readonly IExhibitRepository _repository;
            private readonly IUrlComposer _urlComposer;

            public QueryHandler (IExhibitRepository repository,
                IUrlComposer urlComposer)
            {
                _repository = repository;
                _urlComposer = urlComposer;
            }

            public Result<Command> Handle (Query message)
            {
                var exhibit = _repository.GetExhibit (message.Id);

                if (exhibit == null)
                    return Result.Fail<Command> ("Exhibit does not exit");

                var command = new Command
                {
                    Id = exhibit.Id,
                    Photographer = exhibit.Photographer.Name,
                    Location = exhibit.Location,
                    Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                    IsCanceled = exhibit.IsCanceled,
                    ImageUrl = _urlComposer.ComposeImgUrl(exhibit.ImageUrl)
                };

                return Result.Ok (command);
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator ()
            {
                RuleFor (m => m.Location)
                    .NotNull ().WithMessage ("Name is required.")
                    .Length (1, 100).WithMessage ("Length must be between 1 and 100 characters");
                RuleFor (m => m.Date)
                    .NotNull ();
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command, Result>
        {
            private readonly IExhibitRepository _repository;
            private readonly IHostingEnvironment _environment;
            private readonly ILogger _logger;

            public CommandHandler (IHostingEnvironment environment,
                    IExhibitRepository repository,
                    ILogger<ManageExhibitsController> logger)
            {
                _environment = environment;
                _repository = repository;
                _logger = logger;
            }

            public async Task<Result> Handle (Command message)
            {
                var exhibit = _repository.GetExhibit (message.Id);

                if (exhibit == null)
                    return Result.Fail<Command> ("Exhibit does not exit");

                var uploadPath = Path.Combine (_environment.WebRootPath, "images/exhibits");
                if (message.ImageUpload != null)
                {
                    var ImageName = ContentDispositionHeaderValue.Parse (message.ImageUpload.ContentDisposition).FileName.Trim ('"');
                    using (var fileStream = new FileStream (Path.Combine (uploadPath, message.ImageUpload.FileName), FileMode.Create))
                    {
                        await message.ImageUpload.CopyToAsync (fileStream);
                        message.ImageUrl = "http://exhibitbaseurl/images/exhibits/" + ImageName;
                    }
                }
                message.DateTime = DateTime.Parse (string.Format ("{0}", message.Date));

                exhibit.ManagerUpdate (
                    message.Location,
                    message.DateTime,
                    message.ImageUrl);

                if (message.IsCanceled == true)
                    exhibit.Cancel ();

                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
