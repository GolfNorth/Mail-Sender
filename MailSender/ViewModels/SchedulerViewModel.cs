using System.Collections.ObjectModel;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class SchedulerViewModel : BindableBase
    {
        private ObservableCollection<SchedulerTask> _schedulerTasks; // Коллекция сообщений
        private SchedulerTask _selectedSchedulerTask; // Выбранное сообщение

        public SchedulerViewModel(IEntityManager<SchedulerTask> schedulerTaskManager)
        {
            SchedulerTasks = new ObservableCollection<SchedulerTask>(schedulerTaskManager.GetAll());
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
    }
}