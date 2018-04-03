using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.ManageUsers
{
    public class Details
    {
        public class Query : IRequest<Result<Model>>
        {
            public string Id { get; set; }
        }

        public class Model : IRequest<Result>
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string ImageUrl { get; set; }
            public bool IsSuspended { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Model>>
        {
            private readonly IApplicationUserRepository _repository;

            public Handler (IApplicationUserRepository repository)
            {
                _repository = repository;
            }

            public Result<Model> Handle (Query message)
            {
                var user = _repository.GetPhotographer (message.Id);

                if (user == null)
                    return Result.Fail<Model> ("User does not exit");

                var model = new Model
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    IsSuspended = user.IsSuspended
                };

                return Result.Ok (model);
            }
        }
    }
}