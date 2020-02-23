using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MailSender.Library.Entities;
using MailSender.Library.Services;
using MailSender.Library.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class SchedulerViewModel : BindableBase
    {
        private ObservableCollection<SchedulerTask> _schedulerTasks; // Коллекция сообщений
        private SchedulerTask _selectedSchedulerTask; // Выбранное сообщение
        private DateTime? _selectedDate; // Выбраная дата
        private IEnumerable<Recipient> SelectedRecipients => _distributionGroupViewModel.SelectedRecipients; // Выбранные получатели
        private Server SelectedServer => _distributionGroupViewModel.SelectedServer; // Выбранный сервер
        private Sender SelectedSender => _distributionGroupViewModel.SelectedSender; // Выбранный отправитель
        private Email SelectedEmail => _emailEditorViewModel.SelectedEmail; // Выбранное сообщение

        private readonly DistributionGroupViewModel _distributionGroupViewModel;
        private readonly EmailEditorViewModel _emailEditorViewModel;


        public SchedulerViewModel(IEntityManager<SchedulerTask> schedulerTaskManager,
            DistributionGroupViewModel distributionGroupViewModel,
            EmailEditorViewModel emailEditorViewModel,
            EMailSchedulerTPL emailScheduler)
        {
            _distributionGroupViewModel = distributionGroupViewModel;
            _emailEditorViewModel = emailEditorViewModel;

            SelectedDate = DateTime.Now;

            SchedulerTasks = new ObservableCollection<SchedulerTask>(schedulerTaskManager.GetAll());

            // Добавление задания
            CreatNewSchedulerTaskCommand = new DelegateCommand(() =>
            {
                if (!CanCreateNewTask) return;
                ;
                var newSchedulerTask = CreatNewSchedulerTask(DateTime.Now);

                schedulerTaskManager.Add(newSchedulerTask);
                schedulerTaskManager.SaveChanges();

                SchedulerTasks = new ObservableCollection<SchedulerTask>(schedulerTaskManager.GetAll());
            });//, () => CanCreateNewTask).ObservesCanExecute(() => CanCreateNewTask);

            // Добавление задания с задержкой
            CreatNewDelayedSchedulerTaskCommand = new DelegateCommand(() =>
            {
                if (!CanCreateNewTask) return;

                var newSchedulerTask = CreatNewSchedulerTask(SelectedDate ?? DateTime.Now);

                schedulerTaskManager.Add(newSchedulerTask);
                schedulerTaskManager.SaveChanges();

                SchedulerTasks = new ObservableCollection<SchedulerTask>(schedulerTaskManager.GetAll());
            });//, () => CanCreateNewTask).ObservesProperty(() => CanCreateNewTask);
        }

        /// <summary>
        ///     Создание нового задания
        /// </summary>
        /// <param name="dateTime">Время выполнения</param>
        /// <returns></returns>
        private SchedulerTask CreatNewSchedulerTask(DateTime dateTime)
        {
            var newEmailList = new EmailList();

            foreach (var recipient in SelectedRecipients)
                newEmailList.Recipients.Add(recipient);

            var newSchedulerTask = new SchedulerTask()
            {
                Recipients = newEmailList,
                Server = SelectedServer,
                Sender = SelectedSender,
                Email = SelectedEmail,
                Time = dateTime
            };

            return newSchedulerTask;
        }

        /// <summary>
        ///     Разрешение на запуск команды
        /// </summary>
        private bool CanCreateNewTask =>
            SelectedSender != null && SelectedRecipients != null && SelectedServer != null &&
            SelectedEmail != null;


        /// <summary>
        ///     Выбраная дата
        /// </summary>
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        /// <summary>
        ///     Коллекция заданий
        /// </summary>
        public ObservableCollection<SchedulerTask> SchedulerTasks
        {
            get => _schedulerTasks;
            private set => SetProperty(ref _schedulerTasks, value);
        }

        /// <summary>
        ///     Выбранное задание
        /// </summary>
        public SchedulerTask SelectedSchedulerTask
        {
            get => _selectedSchedulerTask;
            set => SetProperty(ref _selectedSchedulerTask, value);
        }

        /// <summary>
        ///     Создает задачу
        /// </summary>
        public DelegateCommand CreatNewSchedulerTaskCommand { get; }

        /// <summary>
        ///     Создает задачу по расписанию
        /// </summary>
        public DelegateCommand CreatNewDelayedSchedulerTaskCommand { get; }
    }
}