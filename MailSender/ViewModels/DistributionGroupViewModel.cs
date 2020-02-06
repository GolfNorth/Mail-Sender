using System.Collections.Generic;
using MailSender.Library.Data;
using MailSender.Library.Entities;
using CommonServiceLocator;
using MailSender.Enums;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class DistributionGroupViewModel : BindableBase
    {
        public List<Server> Servers => DevData.Servers;
        public List<Sender> Senders => DevData.Senders;
        public List<Recipient> Recipients => DevData.Recipients;

        public DistributionGroupViewModel()
        {
            SwitchToScheduler = new DelegateCommand((() =>
            {
                var mainWindowViewModel = ServiceLocator.Current.GetInstance<MainWindowViewModel>();

                mainWindowViewModel.SelectedTabIndex = (int) MainWindowTabItems.Scheduler;
            }));
        }

        /// <summary>
        ///     Сменяет вкладку на "Планировщик"
        /// </summary>
        public DelegateCommand SwitchToScheduler { get; private set; }
    }
}