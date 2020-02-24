using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services.Managers
{
    public class SchedulerTaskManager : EntityManager<SchedulerTask>
    {
        public SchedulerTaskManager(IEntityStore<SchedulerTask> store) : base(store)
        {
        }
    }
}