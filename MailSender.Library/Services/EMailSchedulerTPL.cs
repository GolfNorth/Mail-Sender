using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Library.Services
{
    public class EMailSchedulerTPL
    {
        private readonly Dictionary<Server, EmailSend> _emailSendServices;
        private readonly IEntityStore<SchedulerTask> _schedulerTasksStore;
        private volatile CancellationTokenSource _processTaskCancellation;

        public EMailSchedulerTPL(IEntityStore<SchedulerTask> schedulerTasksStore)
        {
            _schedulerTasksStore = schedulerTasksStore;
            _emailSendServices = new Dictionary<Server, EmailSend>();
        }

        public async Task StartAsync()
        {
            var cancellation = new CancellationTokenSource();

            Interlocked.Exchange(ref _processTaskCancellation, cancellation)?.Cancel();

            var firstTask = _schedulerTasksStore.GetAll()
                .Where(task => task.Time > DateTime.Now)
                .OrderBy(task => task.Time)
                .FirstOrDefault();

            if (firstTask is null) return;

            WaitTaskAsync(firstTask, cancellation.Token);
        }

        private async void WaitTaskAsync(SchedulerTask schedulerTask, CancellationToken cancel)
        {
            var taskTime = schedulerTask.Time;
            var delta = taskTime.Subtract(DateTime.Now);

            await Task.Delay(delta, cancel).ConfigureAwait(false);

            await ExecuteTask(schedulerTask, cancel);
            _schedulerTasksStore.Remove(schedulerTask.Id);

            await StartAsync();
        }

        public void CancelTask()
        {
            _processTaskCancellation.Cancel();
        }

        private async Task ExecuteTask(SchedulerTask schedulerTask, CancellationToken cancel)
        {
            cancel.ThrowIfCancellationRequested();

            if (!_emailSendServices.ContainsKey(schedulerTask.Server))
                _emailSendServices.Add(schedulerTask.Server, new EmailSend(schedulerTask.Server));

            await _emailSendServices[schedulerTask.Server].SendMailAsync(schedulerTask.Sender, schedulerTask.Recipients, schedulerTask.Email, cancel);
        }
    }
}
