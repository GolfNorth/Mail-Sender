using System.Collections.ObjectModel;
using System.Diagnostics;
using CommonServiceLocator;
using MailSender.Enums;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;
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

        private string _filterText; // Текст фильтра
        private ObservableCollection<Recipient> _filteredRecipients; // Коллекция отфильтрованных получателей
        private ObservableCollection<Recipient> _recipients; // Коллекция получателей
        private ObservableCollection<Sender> _senders; // Коллекция отправителей
        private ObservableCollection<Server> _servers; // Коллекция серверов

        private Recipient _selectedRecipient; // Выбранный получатель
        private Server _selectedServer; // Выбранный сервер
        private Sender _selectedSender; // Выбранный отпарвитель

        private Recipient _editableRecipient; // Редактируемый получатель
        private Server _editableServer; // Редактируемый сервер
        private Sender _editableSender; // Редактируемый отправитель

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

            // Загрузка списка получателей
            LoadRecipientsDataCommand = new DelegateCommand(() =>
            {
                Recipients = new ObservableCollection<Recipient>(_recipientsManager.GetAll());
                FilterRecipients();
            });

            // Добавление получателя
            AddRecipientCommand = new DelegateCommand(() =>
            {
                EditableRecipient = new Recipient();

                _recipientEditor.Edit();
            }, () => Recipients != null).ObservesProperty(() => Recipients);

            // Редактирование получателя
            EditRecipientCommand = new DelegateCommand(() =>
            {
                EditableRecipient = new Recipient
                {
                    Id = SelectedRecipient.Id,
                    Name = SelectedRecipient.Name,
                    Address = SelectedRecipient.Address
                };

                _recipientEditor.Edit();
            }, () => SelectedRecipient != null).ObservesProperty(() => SelectedRecipient);

            // Сохранение изменений получателя
            SaveRecipientChangesCommand = new DelegateCommand(() =>
            {
                Debug.WriteLine(_recipients.Count);
                if (EditableRecipient.Id != 0)
                {
                    _recipientsManager.Edit(EditableRecipient);
                    SelectedRecipient.Name = EditableRecipient.Name;
                    SelectedRecipient.Address = EditableRecipient.Address;
                }
                else
                {
                    _recipientsManager.Add(EditableRecipient);
                    SelectedRecipient = EditableRecipient;
                }

                _recipientsManager.SaveChanges();

                Recipients = new ObservableCollection<Recipient>(_recipientsManager.GetAll());
                FilterRecipients();
            }, () => EditableRecipient != null).ObservesProperty(() => EditableRecipient);

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
        ///     Выбранный сервер
        /// </summary>
        public Server SelectedServer
        {
            get => _selectedServer;
            set => SetProperty(ref _selectedServer, value);
        }

        /// <summary>
        ///     Выбранный отправитель
        /// </summary>
        public Sender SelectedSender
        {
            get => _selectedSender;
            set => SetProperty(ref _selectedSender, value);
        }

        /// <summary>
        ///     Редактируемый получатель
        /// </summary>
        public Recipient EditableRecipient
        {
            get => _editableRecipient;
            set => SetProperty(ref _editableRecipient, value);
        }

        /// <summary>
        ///     Редактируемый сервер
        /// </summary>
        public Server EditableServer
        {
            get => _editableServer;
            set => SetProperty(ref _editableServer, value);
        }

        /// <summary>
        ///     Редактируемый отправитель
        /// </summary>
        public Sender EditableSender
        {
            get => _editableSender;
            set => SetProperty(ref _editableSender, value);
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
        ///     Команда добавления получателя
        /// </summary>
        public DelegateCommand AddRecipientCommand { get; }

        /// <summary>
        ///     Команда реактирования получателя
        /// </summary>
        public DelegateCommand EditRecipientCommand { get; }

        /// <summary>
        ///     Открывает окно редактирование получателя
        /// </summary>
        public DelegateCommand SaveRecipientChangesCommand { get; }

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