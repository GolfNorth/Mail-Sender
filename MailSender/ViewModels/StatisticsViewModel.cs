using System.ComponentModel;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class StatisticsViewModel : BindableBase
    {
        private readonly DistributionGroupViewModel
            _distributionGroupViewModel; // Вью-модель вкладки формирования рассылки

        private readonly EmailEditorViewModel _emailEditorViewModel; // Вью-модель вкладки редактора писем
        private readonly SchedulerViewModel _schedulerViewModel; // Вью-модель вкладки задач

        public StatisticsViewModel(DistributionGroupViewModel distributionGroupViewModel,
            EmailEditorViewModel emailEditorViewModel, SchedulerViewModel schedulerViewModel)
        {
            _distributionGroupViewModel = distributionGroupViewModel;
            _emailEditorViewModel = emailEditorViewModel;
            _schedulerViewModel = schedulerViewModel;

            _distributionGroupViewModel.PropertyChanged += ViewModelsPropertyChanged;
            _emailEditorViewModel.PropertyChanged += ViewModelsPropertyChanged;
            _schedulerViewModel.PropertyChanged += ViewModelsPropertyChanged;
        }

        public int SchedulerTasksCount => _schedulerViewModel.SchedulerTasks?.Count ?? 0;
        public int RecipientsCount => _distributionGroupViewModel.Recipients?.Count ?? 0;
        public int SendersCount => _distributionGroupViewModel.Senders?.Count ?? 0;
        public int ServersCount => _distributionGroupViewModel.Servers?.Count ?? 0;
        public int EmailsCount => _emailEditorViewModel.Emails?.Count ?? 0;

        /// <summary>
        ///     Оповещение команд о изменении данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModelsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(SchedulerTasksCount));
            RaisePropertyChanged(nameof(RecipientsCount));
            RaisePropertyChanged(nameof(SendersCount));
            RaisePropertyChanged(nameof(ServersCount));
            RaisePropertyChanged(nameof(EmailsCount));
        }
    }
}