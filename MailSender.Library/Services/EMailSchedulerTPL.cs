﻿using System;
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
        private readonly Dictionary<Server, IEmailSender> _emailSendServices;  // Словарь сервисов отправки электронной почты
        private readonly IEntityStore<SchedulerTask> _schedulerTasksStore;  // Хранилище заданий
        private readonly IEmailSenderService _emailSenderService; // Сервис отправки сообщений
        private volatile CancellationTokenSource _processTaskCancellation;  // Токен отмены выполнения заданий

        /// <summary>
        ///     Инициализация класса выполнения заданий
        /// </summary>
        /// <param name="schedulerTasksStore">Хранилище заданий</param>
        public EMailSchedulerTPL(IEntityStore<SchedulerTask> schedulerTasksStore, IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
            _schedulerTasksStore = schedulerTasksStore;
            _emailSendServices = new Dictionary<Server, IEmailSender>();
        }

        /// <summary>
        ///     Выполнение отправки писем из хранилища заданий
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            var cancellation = new CancellationTokenSource();

            Interlocked.Exchange(ref _processTaskCancellation, cancellation)?.Cancel();

            var firstTask = _schedulerTasksStore.GetAll()
                .Where(task => task.Time > DateTime.Now.AddMinutes(-1)) // Минус минута исключения для погрешности
                .OrderBy(task => task.Time)
                .FirstOrDefault();

            if (firstTask is null) return;

            WaitTaskAsync(firstTask, cancellation.Token);
        }

        /// <summary>
        ///     Выполнение задания отправки почты по расписанию
        /// </summary>
        /// <param name="schedulerTask"></param>
        /// <param name="cancel"></param>
        private async void WaitTaskAsync(SchedulerTask schedulerTask, CancellationToken cancel)
        {
            var taskTime = schedulerTask.Time;
            var delta = taskTime.Subtract(DateTime.Now);

            await Task.Delay(delta, cancel).ConfigureAwait(false);

            await ExecuteTask(schedulerTask, cancel);
            _schedulerTasksStore.Remove(schedulerTask.Id);

            await StartAsync();
        }

        /// <summary>
        ///     Отмена выполнения заданий
        /// </summary>
        public void CancelTask()
        {
            _processTaskCancellation.Cancel();
        }

        /// <summary>
        ///     Выполнение задания по отправки писем
        /// </summary>
        /// <param name="schedulerTask">Экземпляр задачи</param>
        /// <param name="cancel">Точен отмеены задачи</param>
        /// <returns></returns>
        private async Task ExecuteTask(SchedulerTask schedulerTask, CancellationToken cancel)
        {
            cancel.ThrowIfCancellationRequested();

            if (!_emailSendServices.ContainsKey(schedulerTask.Server))
                _emailSendServices.Add(schedulerTask.Server, _emailSenderService.GetSender(schedulerTask.Server));

            await _emailSendServices[schedulerTask.Server].SendMailAsync(schedulerTask.Sender, schedulerTask.Recipients, schedulerTask.Email, cancel);
        }
    }
}
