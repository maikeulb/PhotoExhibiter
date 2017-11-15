using PhotoExhibiter.Domain.Interfaces;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IExhibitRepository Exhibits { get; }
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        IApplicationUserRepository Users { get; }
        INotificationRepository Notifications { get; }
        IUserNotificationRepository UserNotifications { get; }
        void Complete();
    }
}