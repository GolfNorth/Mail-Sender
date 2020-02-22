using CommonServiceLocator;
using MailSender.Infrastructure.Services;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Data.EntityFramework;
using MailSender.Library.Entities;
using MailSender.Library.Services;
using MailSender.Library.Services.EntityFramework;
using MailSender.Library.Services.InMemory;
using MailSender.Library.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            container.RegisterType<IEntityManager<Recipient>, RecipientsManager>();
            //container.RegisterType<IEntityStore<Recipient>, RecipientsStoreInMemory>();
            container.RegisterType<IEntityStore<Recipient>, RecipientsStoreEntityFramework>();
            container.RegisterType<IEntityEditor<Recipient>, WindowRecipientEditor>(TypeLifetime.Singleton);

            container.RegisterType<IEntityManager<Server>, ServersManager>();
            //container.RegisterType<IEntityStore<Server>, ServersStoreInMemory>();
            container.RegisterType<IEntityStore<Server>, ServersStoreEntityFramework>();
            container.RegisterType<IEntityEditor<Sender>, WindowSenderEditor>(TypeLifetime.Singleton);

            container.RegisterType<IEntityManager<Sender>, SendersManager>();
            //container.RegisterType<IEntityStore<Sender>, SendersStoreInMemory>();
            container.RegisterType<IEntityStore<Sender>, SendersStoreEntityFramework>();
            container.RegisterType<IEntityEditor<Server>, WindowServerEditor>(TypeLifetime.Singleton);

            container.RegisterType<ISaveReport<Recipient>, OpenXMLSaveReport>();

            container.RegisterType<MailSenderDB>();
            container.RegisterInstance<DbContextOptions>(new DbContextOptionsBuilder<MailSenderDB>().UseSqlite(App.Configuration.GetConnectionString("DefaultConnection")).Options);
        }

        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        public DistributionGroupViewModel DistributionGroupViewModel => ServiceLocator.Current.GetInstance<DistributionGroupViewModel>();
        public MailEditorViewModel MailEditorViewModel => ServiceLocator.Current.GetInstance<MailEditorViewModel>();
        public SchedulerViewModel SchedulerViewModel => ServiceLocator.Current.GetInstance<SchedulerViewModel>();
        public StatisticsViewModel StatisticsViewModel => ServiceLocator.Current.GetInstance<StatisticsViewModel>();
    }

    
}