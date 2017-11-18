namespace PhotoExhibiter.Application.Commands
{
    using AutoMapper;
    using MediatR;
    using PhotoExhibiter.Domain.Interfaces;
    using PhotoExhibiter.Domain.Models;

    public class Follow
    {
        public class Command : IRequest
        {
            public string UserId { get; set; }
            public string FolloweeId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IFollowingRepository _repository;
            private readonly IMapper _mapper;

            public Handler (IFollowingRepository repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public void Handle (Command message)
            {
                var following = _mapper.Map<Command, Following> (message);

                _repository.Add (following);
                _repository.SaveAll ();
            }
        }
    }
}
