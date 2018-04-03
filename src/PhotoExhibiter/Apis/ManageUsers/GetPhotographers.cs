using System.Linq;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.ManageUsers
{
    public class GetPhotographers
    {
        public class Query : IRequest<IEnumerable<Model>>
        {
            public string query { get; set; }
        }

        public class Model : IRequest
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string ImageUrl { get; set; }
            public bool IsSuspended { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly IApplicationUserRepository _repository;

            public Handler (IApplicationUserRepository repository)
            {
                _repository = repository;
            }

            public IEnumerable<Model> Handle (Query message)
            {
                var users = _repository.GetAllPhotographers(message.query);

                return users.Select( u =>  new Model()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    ImageUrl = u.ImageUrl,
                    IsSuspended = u.IsSuspended
                });
            }
        }
    }
}
