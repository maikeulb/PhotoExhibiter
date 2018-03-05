using System;
using System.Linq;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.ManageExhibits
{
    public class GetExhibits
    {
        public class Query : IRequest<IEnumerable<Model>>
        {
            public int? Id { get; set; }
        }

        public class Model : IRequest
        {
            public int Id { get; set; }
            public string Genre { get; set; }
            public string Photographer { get; set; }
            public string Date { get; set; }
            public string Location { get; set; }
            public string ImageUrl { get; set; }
            public bool IsCanceled { get; set; }
            public DateTime DateTime { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly IExhibitRepository _repository;

            public Handler (IExhibitRepository repository)
            {
                _repository = repository;
            }

            public IEnumerable<Model> Handle (Query message)
            {
                var exhibits = _repository.GetAllExhibits();

                return exhibits.Select( e =>  new Model()
                {
                    Id = e.Id,
                    Genre = e.Genre.Name,
                    Photographer = e.Photographer.Name,
                    Date = e.DateTime.ToString ("d MMM yyyy"),
                    Location = e.Location,
                    ImageUrl = e.ImageUrl,
                    DateTime = e.DateTime,
                    IsCanceled = e.IsCanceled,
                });
            }
        }
    }
}
