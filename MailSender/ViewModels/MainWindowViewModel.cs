using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private int _selectedTabIndex;

        public MainWindowViewModel(IEntityManager<Recipient> recipientsManager,
            IEntityExport<Recipient> recipientsExporter,
            IEntityManager<Sender> sendersManager, IEntityExport<Sender> sendersExporter,
            IEntityManager<Server> serversManager, IEntityExport<Server> serversExporter,
            IEntityManager<Email> emailsManager, IEntityExport<Email> emailsExporter)
        {
            // Экспорт получателей
            ExportRecipients = new DelegateCommand(() => { recipientsExporter.Export(recipientsManager.GetAll()); });
            // Экспорт отправителей
            ExportSenders = new DelegateCommand(() => { sendersExporter.Export(sendersManager.GetAll()); });
            // Экспорт серверов
            ExportServers = new DelegateCommand(() => { serversExporter.Export(serversManager.GetAll()); });
            // Экспорт сообщений
            ExportEmails = new DelegateCommand(() => { emailsExporter.Export(emailsManager.GetAll()); });
        }


        /// <summary>
        ///     Индекс главного TabControl
        /// </summary>
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

        /// <summary>
        ///     Экспорт получателей
        /// </summary>
        public DelegateCommand ExportRecipients { get; }

        /// <summary>
        ///     Экспорт отправителей
        /// </summary>
        public DelegateCommand ExportSenders { get; }

        /// <summary>
        ///     Экспорт серверов
        /// </summary>
        public DelegateCommand ExportServers { get; }

        /// <summary>
        ///     Экспорт сообщений
        /// </summary>
        public DelegateCommand ExportEmails { get; }
    }
}