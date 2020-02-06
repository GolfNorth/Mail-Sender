using System.Collections.Generic;
using CommonServiceLocator;
using MailSender.Enums;
using MailSender.Library.Data;
using MailSender.Library.Entities;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class DistributionGroupViewModel : BindableBase
    {
        public DistributionGroupViewModel()
        {
            SwitchToScheduler = new DelegateCommand(() =>
            {
                var mainWindowViewModel = ServiceLocator.Current.GetInstance<MainWindowViewModel>();

                mainWindowViewModel.SelectedTabIndex = (int) MainWindowTabItems.Scheduler;
            });
        }

        public List<Server> Servers => DevData.Servers;
        public List<Sender> Senders => DevData.Senders;
        public List<Recipient> Recipients => DevData.Recipients;

        /// <summary>
        ///     Сменяет вкладку на "Планировщик"
        /// </summary>
        public DelegateCommand SwitchToScheduler { get; }
    }
}