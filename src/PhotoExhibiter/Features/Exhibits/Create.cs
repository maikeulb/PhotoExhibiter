using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure;

namespace PhotoExhibiter.Features.Exhibits
{
    public class Create
    {
        public class Query : IRequest<Command>
        {
            public string UserId { get; set; }
        }

        public class Command : IRequest
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

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly IGenreRepository _repository;

            public QueryHandler (
                IGenreRepository repository)
            {
                _repository = repository;
            }

            public Command Handle (Query message)
            {
                var command = new Command
                {
                    UserId = message.UserId,
                    Genres = _repository.GetGenres (),
                    Heading = "Add an Exhibit"
                };

                return command;
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator ()
            {
                RuleFor (m => m.Location)
                    .NotNull ().WithMessage ("Name is required.")
                    .Length (1, 22).WithMessage ("Length must be between 1 and 22 characters");
                RuleFor (m => m.Date)
                    .NotNull ()
                    .SetValidator (new FutureDateValidator ());
                RuleFor (m => m.GenreId)
                    .NotNull ();
                RuleFor (m => m.ImageUpload) 
                    .NotNull ();
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command>
        {
            private readonly IExhibitRepository _repository;
            private readonly IHostingEnvironment _environment;

            public CommandHandler (IExhibitRepository repository) => _repository = repository;

            public CommandHandler(IHostingEnvironment environment,
                    IExhibitRepository repository)
            {
                _environment = environment;
                _repository = repository;
            }

            public async Task Handle (Command message)
            {
                var uploadPath = Path.Combine (_environment.WebRootPath, "images/exhibits");
                var ImageName = ContentDispositionHeaderValue.Parse (message.ImageUpload.ContentDisposition).FileName.Trim ('"');

                using (var fileStream = new FileStream (Path.Combine (uploadPath, message.ImageUpload.FileName), FileMode.Create))
                {
                    await message.ImageUpload.CopyToAsync (fileStream);
                    message.ImageUrl = "http://exhibitbaseurl/images/exhibits/" + ImageName;
                }
                message.DateTime = DateTime.Parse (string.Format ("{0}", message.Date));

                var exhibit = Exhibit.Create (message);

                _repository.Add (exhibit);
                _repository.SaveAll ();
            }
        }
    }
}
