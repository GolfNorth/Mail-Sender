using System.Collections.ObjectModel;
using CommonServiceLocator;
using MailSender.Enums;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;
using MailSender.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class DistributionGroupViewModel : BindableBase
    {
        private readonly IEntityManager<Recipient> _recipientsManager; // Менеджер получателей
        private readonly IEntityManager<Sender> _sendersManager; // Менеджер отправителей
        private readonly IEntityManager<Server> _serversManager; // Менеджер серверов

        private readonly IEntityEditor<Recipient> _recipientEditor; // Сервис открытия окон

        private ObservableCollection<Recipient> _filteredRecipients; // Коллекция отфильтрованных получателей
        private string _filterText; // Текст фильтра
        private ObservableCollection<Recipient> _recipients; // Коллекция получателей
        private Recipient _selectedRecipient; // Выбранный получатель
        private ObservableCollection<Sender> _senders; // Коллекция отправителей
        private ObservableCollection<Server> _servers; // Коллекция серверов

        public DistributionGroupViewModel(IEntityManager<Recipient> recipientsManager,
            IEntityManager<Server> serversManager, IEntityManager<Sender> sendersManager, IEntityEditor<Recipient> recipientEditor)
        {
            FilteredRecipients = new ObservableCollection<Recipient>();
            FilterText = string.Empty;

            _recipientsManager = recipientsManager;
            _serversManager = serversManager;
            _sendersManager = sendersManager;

            _recipientEditor = recipientEditor;

            Servers = new ObservableCollection<Server>(_serversManager.GetAll());
            Senders = new ObservableCollection<Sender>(_sendersManager.GetAll());

            #region Реализация команд

            SwitchToScheduler = new DelegateCommand(() =>
            {
                var mainWindowViewModel = ServiceLocator.Current.GetInstance<MainWindowViewModel>();

                mainWindowViewModel.SelectedTabIndex = (int) MainWindowTabItems.Scheduler;
            });

            LoadRecipientsDataCommand = new DelegateCommand(() =>
            {
                Recipients = new ObservableCollection<Recipient>(_recipientsManager.GetAll());
                FilterRecipients();
            });

            RecipientEditorCommand = new DelegateCommand<Recipient>(recipient =>
            {
                _recipientEditor.Edit(recipient);
            }, recipient => recipient != null).ObservesProperty(() => SelectedRecipient);

            SaveRecipientChangesCommand = new DelegateCommand<Recipient>(recipient =>
            {
                _recipientsManager.Edit(recipient);
                _recipientsManager.SaveChanges();
            }, recipient => recipient != null).ObservesProperty(() => SelectedRecipient);

            #endregion
        }

        /// <summary>
        ///     Коллекция получателей
        /// </summary>
        public ObservableCollection<Recipient> Recipients
        {
            get => _recipients;
            private set => SetProperty(ref _recipients, value);
        }

        /// <summary>
        ///     Коллекция серверов
        /// </summary>
        public ObservableCollection<Server> Servers
        {
            get => _servers;
            set => SetProperty(ref _servers, value);
        }

        /// <summary>
        ///     Коллекция отправителей
        /// </summary>
        public ObservableCollection<Sender> Senders
        {
            get => _senders;
            set => SetProperty(ref _senders, value);
        }

        /// <summary>
        ///     Выбранный получатель
        /// </summary>
        public Recipient SelectedRecipient
        {
            get => _selectedRecipient;
            set => SetProperty(ref _selectedRecipient, value);
        }

        /// <summary>
        ///     Текст фильтра получателей
        /// </summary>
        public string FilterText
        {
            get => _filterText;
            set
            {
                SetProperty(ref _filterText, value);
                FilterRecipients();
            }
        }

        /// <summary>
        ///     Коллекция отфильтрованных получателей
        /// </summary>
        public ObservableCollection<Recipient> FilteredRecipients
        {
            get => _filteredRecipients;
            set => SetProperty(ref _filteredRecipients, value);
        }

        /// <summary>
        ///     Сменяет вкладку на "Планировщик"
        /// </summary>
        public DelegateCommand SwitchToScheduler { get; }

        /// <summary>
        ///     Загружает список получателей сообщений
        /// </summary>
        public DelegateCommand LoadRecipientsDataCommand { get; }

        /// <summary>
        ///     Открывает окно редактирование получателя
        /// </summary>
        public DelegateCommand<Recipient> RecipientEditorCommand { get; }

        /// <summary>
        ///     Открывает окно редактирование получателя
        /// </summary>
        public DelegateCommand<Recipient> SaveRecipientChangesCommand { get; }

        /// <summary>
        ///     Фильтрация списка получателей
        /// </summary>
        private void FilterRecipients()
        {
            if (Recipients == null)
                return;

            _filteredRecipients.Clear();

            foreach (var item in Recipients)
                if (item.Name.Contains(FilterText))
                    _filteredRecipients.Add(item);
        }
    }
}