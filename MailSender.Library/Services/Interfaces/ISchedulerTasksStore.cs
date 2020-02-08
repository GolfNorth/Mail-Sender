using MailSender.Library.Entities;

namespace MailSender.Library.Services.Interfaces
{
    public interface ISchedulerTasksStore : IEntityStore<SchedulerTask>
    {
    }
}