using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Apis.Exhibits
{
    public class Cancel
    {
        public class Command : IRequest<Result>
        {
            public string UserId { get; set; }
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IExhibitRepository _repository;

            public Handler (IExhibitRepository repository) => _repository = repository;

            public Result Handle (Command message)
            {
                var exhibit = _repository.GetExhibitWithAttendees (message.Id);
                /* if (exhibit == null) */
                /* return Result.Fail<Command> ("Exhibit does not exist"); */
                /* if (exhibit.IsCanceled) */
                /* return Result.Fail<Command> ("Exhibit is cancelled."); */
                /* if (exhibit.PhotographerId != message.UserId) */
                /* return Result.Fail<Command> ("Unauthorized"); */

                exhibit.Cancel (); //event
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}