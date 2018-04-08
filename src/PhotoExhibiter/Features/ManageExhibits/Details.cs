using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure.Interfaces;

namespace PhotoExhibiter.Features.ManageExhibits
{
    public class Details
    {
        public class Query : IRequest<Result<Model>>
        {
            public int Id { get; set; }
        }

        public class Model : IRequest<Result>
        {
            public int Id { get; set; }

            [Display (Name = "Genre")]
            public int GenreId { get; set; }
            public string Genre { get; set; }
            public string Date { get; set; }
            public string Location { get; set; }
            public string Photographer { get; set; }
            public string ImageUrl { get; set; }
            public bool IsCanceled { get; set; }
            public DateTime DateTime { get; set; }
            public IEnumerable<Genre> Genres { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Model>>
        {
            private readonly IExhibitRepository _repository;
            private readonly IUrlComposer _urlComposer;

            public Handler (IExhibitRepository repository,
                IUrlComposer urlComposer)
            {
                _repository = repository;
                _urlComposer = urlComposer;
            }

            public Result<Model> Handle (Query message)
            {
                var exhibit = _repository.GetExhibit (message.Id);

                if (exhibit == null)
                    return Result.Fail<Model> ("Exhibit does not exit");

                var model = new Model
                {
                    Id = exhibit.Id,
                    Genre = exhibit.Genre.Name,
                    Photographer = exhibit.Photographer.Name,
                    Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                    Location = exhibit.Location,
                    ImageUrl = _urlComposer.ComposeImgUrl(exhibit.ImageUrl),
                    DateTime = exhibit.DateTime,
                    IsCanceled = exhibit.IsCanceled,
                };

                return Result.Ok (model);
            }
        }
    }
}
