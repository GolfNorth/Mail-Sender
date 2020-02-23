using System.Collections.ObjectModel;
using MailSender.Enums;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class DistributionGroupViewModel : BindableBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel; // ViewModel главного окна
        private readonly IEntityManager<Recipient> _recipientsManager; // Менеджер получателей
        private readonly IEntityManager<Sender> _sendersManager; // Менеджер отправителей
        private readonly IEntityManager<Server> _serversManager; // Менеджер серверов

        private Recipient _editableRecipient; // Редактируемый получатель
        private Sender _editableSender; // Редактируемый отправитель
        private Server _editableServer; // Редактируемый сервер
        private ObservableCollection<Recipient> _filteredRecipients; // Коллекция отфильтрованных получателей
        private string _filterText; // Текст фильтра
        private ObservableCollection<Recipient> _recipients; // Коллекция получателей
        private Recipient _selectedRecipient; // Выбранный получатель
        private Sender _selectedSender; // Выбранный отпарвитель
        private Server _selectedServer; // Выбранный сервер
        private ObservableCollection<Sender> _senders; // Коллекция отправителей
        private ObservableCollection<Server> _servers; // Коллекция серверов

        public DistributionGroupViewModel(MainWindowViewModel mainWindowViewModel,
            IEntityManager<Recipient> recipientsManager,
            IEntityManager<Server> serversManager, IEntityManager<Sender> sendersManager)
        {
            FilteredRecipients = new ObservableCollection<Recipient>();
            FilterText = string.Empty;

            _mainWindowViewModel = mainWindowViewModel;

            _recipientsManager = recipientsManager;
            _serversManager = serversManager;
            _sendersManager = sendersManager;

            Servers = new ObservableCollection<Server>(_serversManager.GetAll());
            Senders = new ObservableCollection<Sender>(_sendersManager.GetAll());

            #region Реализация команд

            // Переключение на вкладку планировщика
            SwitchToScheduler = new DelegateCommand(() =>
            {
                _mainWindowViewModel.SelectedTabIndex = (int) MainWindowTabItems.Scheduler;
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

                _recipientsManager.Add(EditableRecipient);
                _recipientsManager.SaveChanges();

                Recipients = new ObservableCollection<Recipient>(_recipientsManager.GetAll());
                FilterRecipients();
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

                _recipientsManager.Edit(EditableRecipient);
                _recipientsManager.SaveChanges();

                Recipients = new ObservableCollection<Recipient>(_recipientsManager.GetAll());
                FilterRecipients();
            }, () => SelectedRecipient != null).ObservesProperty(() => SelectedRecipient);

            // Удаление получателя
            RemoveRecipientCommand = new DelegateCommand(() =>
            {
                _recipientsManager.Remove(SelectedRecipient);
                _recipientsManager.SaveChanges();

                Recipients = new ObservableCollection<Recipient>(_recipientsManager.GetAll());
                FilterRecipients();
            }, () => SelectedRecipient != null).ObservesProperty(() => SelectedRecipient);

            // Добавление отправителя
            AddSenderCommand = new DelegateCommand(() =>
            {
                EditableSender = new Sender();

                _sendersManager.Add(EditableSender);
                _sendersManager.SaveChanges();

                Senders = new ObservableCollection<Sender>(_sendersManager.GetAll());
            }, () => Senders != null).ObservesProperty(() => Senders);

            // Редактирование отправителя
            EditSenderCommand = new DelegateCommand(() =>
            {
                EditableSender = new Sender
                {
                    Id = SelectedSender.Id,
                    Name = SelectedSender.Name,
                    Address = SelectedSender.Address
                };

                _sendersManager.Edit(EditableSender);
                _sendersManager.SaveChanges();

                Senders = new ObservableCollection<Sender>(_sendersManager.GetAll());
            }, () => SelectedSender != null).ObservesProperty(() => SelectedSender);

            // Удаление отправителя
            RemoveSenderCommand = new DelegateCommand(() =>
            {
                _sendersManager.Remove(SelectedSender);
                _sendersManager.SaveChanges();

                Senders = new ObservableCollection<Sender>(_sendersManager.GetAll());
            }, () => SelectedSender != null).ObservesProperty(() => SelectedSender);

            // Добавление сервера
            AddServerCommand = new DelegateCommand(() =>
            {
                EditableServer = new Server();

                _serversManager.Add(EditableServer);
                _serversManager.SaveChanges();

                Servers = new ObservableCollection<Server>(_serversManager.GetAll());
            }, () => Servers != null).ObservesProperty(() => Servers);

            // Редактирование сервера
            EditServerCommand = new DelegateCommand(() =>
            {
                EditableServer = new Server
                {
                    Id = SelectedServer.Id,
                    Name = SelectedServer.Name,
                    Host = SelectedServer.Host,
                    Port = SelectedServer.Port,
                    EnableSsl = SelectedServer.EnableSsl,
                    Login = SelectedServer.Login,
                    Password = SelectedServer.Password
                };

                _serversManager.Edit(EditableServer);
                _serversManager.SaveChanges();

                Servers = new ObservableCollection<Server>(_serversManager.GetAll());
            }, () => SelectedServer != null).ObservesProperty(() => SelectedServer);

            // Удаление сервера
            RemoveServerCommand = new DelegateCommand(() =>
            {
                _serversManager.Remove(SelectedServer);
                _serversManager.SaveChanges();

                Servers = new ObservableCollection<Server>(_serversManager.GetAll());
            }, () => SelectedServer != null).ObservesProperty(() => SelectedServer);

            #endregion
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

        #region Коллекции сущностей

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
        ///     Коллекция отфильтрованных получателей
        /// </summary>
        public ObservableCollection<Recipient> FilteredRecipients
        {
            get => _filteredRecipients;
            set => SetProperty(ref _filteredRecipients, value);
        }

        #endregion

        #region Выбранные сущности

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

        #endregion

        #region Редактируемые сущности

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

        #endregion

        #region Объявление команд

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
        ///     Команда удаления получателя
        /// </summary>
        public DelegateCommand RemoveRecipientCommand { get; }

        /// <summary>
        ///     Команда добавления отпарвителя
        /// </summary>
        public DelegateCommand AddSenderCommand { get; }

        /// <summary>
        ///     Команда реактирования отпарвителя
        /// </summary>
        public DelegateCommand EditSenderCommand { get; }

        /// <summary>
        ///     Команда удаления отпарвителя
        /// </summary>
        public DelegateCommand RemoveSenderCommand { get; }

        /// <summary>
        ///     Команда добавления сервера
        /// </summary>
        public DelegateCommand AddServerCommand { get; }

        /// <summary>
        ///     Команда реактирования сервера
        /// </summary>
        public DelegateCommand EditServerCommand { get; }

        /// <summary>
        ///     Команда удаления сервера
        /// </summary>
        public DelegateCommand RemoveServerCommand { get; }

        #endregion
    }
}