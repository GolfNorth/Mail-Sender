using CommonServiceLocator;
using MailSender.Library.Services;
using MailSender.Library.Services.Interfaces;
using Prism.Unity;
using Unity;

namespace MailSender.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var container = new UnityContainer();

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocatorAdapter(container));

            container.RegisterType<MainWindowViewModel>(TypeLifetime.Singleton);

            container.RegisterType<DistributionGroupViewModel>(TypeLifetime.Singleton);
            container.RegisterType<MailEditorViewModel>(TypeLifetime.Singleton);
            container.RegisterType<SchedulerViewModel>(TypeLifetime.Singleton);
            container.RegisterType<StatisticsViewModel>(TypeLifetime.Singleton);

            container.RegisterType<IRecipientsManager, RecipientsManager>();
            container.RegisterType<IRecipientsStore, RecipientsStoreInMemory>();
        }

        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        public DistributionGroupViewModel DistributionGroupViewModel => ServiceLocator.Current.GetInstance<DistributionGroupViewModel>();
        public MailEditorViewModel MailEditorViewModel => ServiceLocator.Current.GetInstance<MailEditorViewModel>();
        public SchedulerViewModel SchedulerViewModel => ServiceLocator.Current.GetInstance<SchedulerViewModel>();
        public StatisticsViewModel StatisticsViewModel => ServiceLocator.Current.GetInstance<StatisticsViewModel>();
    }
}