using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MailSender.Library.Entities;
using MailSender.Library.Services;
using MailSender.Library.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class SchedulerViewModel : BindableBase
    {
        private readonly DistributionGroupViewModel _distributionGroupViewModel; // Вью-модель вкладки формирования рассылки
        private readonly EmailEditorViewModel _emailEditorViewModel; // Вью-модель вкладки редактора писем
        private ObservableCollection<SchedulerTask> _schedulerTasks; // Коллекция сообщений
        private DateTime? _selectedDate; // Выбраная дата
        private SchedulerTask _selectedSchedulerTask; // Выбранное сообщение

        public SchedulerViewModel(IEntityManager<SchedulerTask> schedulerTaskManager,
            DistributionGroupViewModel distributionGroupViewModel,
            EmailEditorViewModel emailEditorViewModel,
            EMailSchedulerTPL emailScheduler)
        {
            _distributionGroupViewModel = distributionGroupViewModel;
            _emailEditorViewModel = emailEditorViewModel;

            _distributionGroupViewModel.PropertyChanged += ViewModelsPropertyChanged;
            _emailEditorViewModel.PropertyChanged += ViewModelsPropertyChanged;

            // Запуск заданий, если остались
            _ = emailScheduler.StartAsync();

            SelectedDate = DateTime.Now;

            SchedulerTasks = new ObservableCollection<SchedulerTask>(schedulerTaskManager.GetAll());

            // Добавление задания
            CreatNewSchedulerTaskCommand = new DelegateCommand(() =>
            {
                var newSchedulerTask = CreateNewSchedulerTask(DateTime.Now.AddSeconds(10));

                schedulerTaskManager.Add(newSchedulerTask);
                schedulerTaskManager.SaveChanges();

                _ = emailScheduler.StartAsync();

                SchedulerTasks = new ObservableCollection<SchedulerTask>(schedulerTaskManager.GetAll());
            }, () => CanCreateNewTask);

            // Добавление задания с задержкой
            CreatNewDelayedSchedulerTaskCommand = new DelegateCommand(() =>
            {
                var newSchedulerTask = CreateNewSchedulerTask(SelectedDate ?? DateTime.Now);

                schedulerTaskManager.Add(newSchedulerTask);
                schedulerTaskManager.SaveChanges();

                _ = emailScheduler.StartAsync();

                SchedulerTasks = new ObservableCollection<SchedulerTask>(schedulerTaskManager.GetAll());
            }, () => CanCreateNewTask);
        }

        /// <summary>
        ///     Оповещение команд о изменении данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModelsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CreatNewDelayedSchedulerTaskCommand.RaiseCanExecuteChanged();
            CreatNewSchedulerTaskCommand.RaiseCanExecuteChanged();
        }

        private IEnumerable<Recipient> SelectedRecipients => _distributionGroupViewModel.SelectedRecipients; // Выбранные получатели
        private Server SelectedServer => _distributionGroupViewModel.SelectedServer; // Выбранный сервер
        private Sender SelectedSender => _distributionGroupViewModel.SelectedSender; // Выбранный отправитель
        private Email SelectedEmail => _emailEditorViewModel.SelectedEmail; // Выбранное сообщение

        /// <summary>
        ///     Разрешение на запуск команды
        /// </summary>
        private bool CanCreateNewTask =>
            SelectedRecipients != null && SelectedRecipients.Any() && SelectedServer != null &&
            SelectedEmail != null && SelectedSender != null;

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

        /// <summary>
        ///     Создание нового задания
        /// </summary>
        /// <param name="dateTime">Время выполнения</param>
        /// <returns></returns>
        private SchedulerTask CreateNewSchedulerTask(DateTime dateTime)
        {
            var newEmailList = new EmailList()
            {
                Name = SelectedEmail.Subject
            };
            newEmailList.RecipientsList = SelectedRecipients.Select(recipient =>
                new EmailListRecipient {Recipient = recipient, EmailList = newEmailList}).ToArray();

            var newSchedulerTask = new SchedulerTask
            {
                Recipients = newEmailList,
                Server = SelectedServer,
                Sender = SelectedSender,
                Email = SelectedEmail,
                Time = dateTime
            };

            return newSchedulerTask;
        }
    }
}