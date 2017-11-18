namespace PhotoExhibiter.Application.Commands
{
    using MediatR;
    using PhotoExhibiter.Domain.Interfaces;

    public class Cancel
    {
        public class Command : IRequest
        {
            public string UserId { get; set; }
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IExhibitRepository _repository;

            public Handler (IExhibitRepository repository)
            {
                _repository = repository;
            }

            public void Handle (Command message)
            {
                var exhibit = _repository.GetExhibitWithAttendees (message.Id);

                exhibit.Cancel ();
                _repository.SaveAll ();
            }
        }
    }
}