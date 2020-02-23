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

            #region ViewModels
            container.RegisterType<MainWindowViewModel>(TypeLifetime.Singleton);
            container.RegisterType<DistributionGroupViewModel>(TypeLifetime.Singleton);
            container.RegisterType<MailEditorViewModel>(TypeLifetime.Singleton);
            container.RegisterType<SchedulerViewModel>(TypeLifetime.Singleton);
            container.RegisterType<StatisticsViewModel>(TypeLifetime.Singleton);
            #endregion

            #region Managers
            container.RegisterType<IEntityManager<Recipient>, RecipientsManager>();
            container.RegisterType<IEntityManager<Server>, ServersManager>();
            container.RegisterType<IEntityManager<Sender>, SendersManager>();
            #endregion

            #region StoreInMemory
            //container.RegisterType<IEntityStore<Recipient>, RecipientsStoreInMemory>();
            //container.RegisterType<IEntityStore<Server>, ServersStoreInMemory>();
            //container.RegisterType<IEntityStore<Sender>, SendersStoreInMemory>();
            #endregion

            #region StoreEntityFramework
            container.RegisterType<IEntityStore<Recipient>, RecipientsStoreEntityFramework>();
            container.RegisterType<IEntityStore<Server>, ServersStoreEntityFramework>();
            container.RegisterType<IEntityStore<Sender>, SendersStoreEntityFramework>();
            #endregion

            #region Editors
            container.RegisterType<IEntityEditor<Recipient>, WindowRecipientEditor>(TypeLifetime.Singleton);
            container.RegisterType<IEntityEditor<Sender>, WindowSenderEditor>(TypeLifetime.Singleton);
            container.RegisterType<IEntityEditor<Server>, WindowServerEditor>(TypeLifetime.Singleton);
            #endregion

            #region Exporters
            container.RegisterType<IEntityExport<Recipient>, OpenXmlRecipientsExport>();
            container.RegisterType<IEntityExport<Sender>, OpenXmlSendersExport>();
            container.RegisterType<IEntityExport<Server>, OpenXmlServersExport>();
            container.RegisterType<IEntityExport<Email>, OpenXmlEmailsExport>();
            #endregion

            #region Others
            container.RegisterType<IEmailSenderService, EmailSenderService>();
            #endregion
            
            #region DB
            container.RegisterType<MailSenderDB>();
            container.RegisterType<MailSenderDBInitializer>();
            container.RegisterInstance<DbContextOptions>(new DbContextOptionsBuilder<MailSenderDB>().UseSqlite(App.Configuration.GetConnectionString("DefaultConnection")).Options);

            var dbInitializer = ServiceLocator.Current.GetInstance<MailSenderDBInitializer>();
            dbInitializer.InitializeAsync().Wait();
            #endregion
        }

        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        public DistributionGroupViewModel DistributionGroupViewModel => ServiceLocator.Current.GetInstance<DistributionGroupViewModel>();
        public MailEditorViewModel MailEditorViewModel => ServiceLocator.Current.GetInstance<MailEditorViewModel>();
        public SchedulerViewModel SchedulerViewModel => ServiceLocator.Current.GetInstance<SchedulerViewModel>();
        public StatisticsViewModel StatisticsViewModel => ServiceLocator.Current.GetInstance<StatisticsViewModel>();
    }

    
}