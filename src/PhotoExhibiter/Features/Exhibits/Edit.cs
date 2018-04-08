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

namespace PhotoExhibiter.Features.Exhibits
{
    public class Edit
    {
        public class Query : IRequest<Result<Command>>
        {
            public string UserId { get; set; }
            public int Id { get; set; }
        }

        public class Command : IRequest<Result>
        {
            public int Id { get; set; }
            [Display (Name = "Genre")]
            public int GenreId { get; set; }
            public string UserId { get; set; }
            public string Location { get; set; }
            public string Date { get; set; }
            [Display (Name = "Date")]
            public DateTime DateTime { get; set; }
            public string Heading { get; set; }

            public IFormFile ImageUpload { get; set; }
            public string ImageName { get; set; }
            public string ImageUrl { get; set; }

            public IEnumerable<Genre> Genres { get; set; } // can embed genre class
        }

        public class QueryHandler : IRequestHandler<Query, Result<Command>>
        {
            private readonly IExhibitRepository _exhibitrepository;
            private readonly IGenreRepository _genrerepository;
            private readonly IUrlComposer _urlComposer;

            public QueryHandler (
                IExhibitRepository exhibitrepository,
                IGenreRepository genrerepository,
                IUrlComposer urlComposer)
            {
                _exhibitrepository = exhibitrepository;
                _genrerepository = genrerepository;
                _urlComposer = urlComposer;
            }

            public Result<Command> Handle (Query message)
            {
                var exhibit = _exhibitrepository.GetExhibit (message.Id);

                if (exhibit == null)
                    return Result.Fail<Command> ("Exhibit does not exit");
                if (exhibit.PhotographerId != message.UserId)
                    return Result.Fail<Command> ("Unauthorized");

                var command = new Command
                {
                    Id = exhibit.Id,
                    Heading = "Edit an Exhibit",
                    Genres = _genrerepository.GetGenres (),
                    Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                    GenreId = exhibit.GenreId,
                    Location = exhibit.Location,
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
                    .NotNull ()
                    .SetValidator (new FutureDateValidator ());
                RuleFor (m => m.GenreId)
                    .NotNull ();
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command, Result>
        {
            private readonly IExhibitRepository _repository;
            private readonly IHostingEnvironment _environment;

            public CommandHandler (IHostingEnvironment environment,
                    IExhibitRepository repository)
            {
                _environment = environment;
                _repository = repository;
            }

            public async Task<Result> Handle (Command message)
            {
                message.DateTime = DateTime.Parse (string.Format ("{0}", message.Date));
                var exhibit = _repository.GetExhibit (message.Id);

                if (exhibit == null)
                    return Result.Fail<Command> ("Exhibit does not exit");
                if (exhibit.PhotographerId != message.UserId)
                    return Result.Fail<Command> ("Unauthorized");

                var uploadPath = Path.Combine (_environment.WebRootPath, "images/exhibits");
                var ImageName = ContentDispositionHeaderValue.Parse (message.ImageUpload.ContentDisposition).FileName.Trim ('"');
                using (var fileStream = new FileStream (Path.Combine (uploadPath, message.ImageUpload.FileName), FileMode.Create))
                {
                    await message.ImageUpload.CopyToAsync (fileStream);
                    message.ImageUrl = "http://exhibitbaseurl/images/exhibits/" + ImageName;
                }

                exhibit.UpdateDetails (message);
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
