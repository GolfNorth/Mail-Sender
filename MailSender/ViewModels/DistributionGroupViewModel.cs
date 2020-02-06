using System.Collections.ObjectModel;
using CommonServiceLocator;
using MailSender.Enums;
using MailSender.Library.Data;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;
using MailSender.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class DistributionGroupViewModel : BindableBase
    {
        private readonly IRecipientsManager _recipientsManager;         // Менеджер получателей

        private ObservableCollection<Recipient> _filteredRecipients;    // Коллекция отфильтрованных получателей
        private string _filterText;                                     // Текст фильтра
        private ObservableCollection<Recipient> _recipients;            // Коллекция получателей
        private Recipient _selectedRecipient;                           // Выбранный получатель
        private ObservableCollection<Sender> _senders;                  // Коллекция отправителей
        private ObservableCollection<Server> _servers;                  // Коллекция серверов

        public DistributionGroupViewModel(IRecipientsManager recipientsManager)
        {
            FilteredRecipients = new ObservableCollection<Recipient>();
            FilterText = string.Empty;

            Servers = new ObservableCollection<Server>(DevData.Servers);
            Senders = new ObservableCollection<Sender>(DevData.Senders);

            _recipientsManager = recipientsManager;

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

            RecipientEditCommand = new DelegateCommand(() =>
            {
                var editWindow = new RecipientEditorWindow();

                editWindow.ShowDialog();
            }, () => SelectedRecipient != null).ObservesProperty(() => SelectedRecipient);

            SaveRecipientChangesCommand = new DelegateCommand<Recipient>(recipient =>
            {
                _recipientsManager.Edit(recipient);
                _recipientsManager.SaveChanges();
            }, recipient => recipient != null).ObservesProperty(() => SelectedRecipient);

            #endregion
        }

        public ObservableCollection<Recipient> Recipients
        {
            get => _recipients;
            private set => SetProperty(ref _recipients, value);
        }

        public ObservableCollection<Server> Servers
        {
            get => _servers;
            set => SetProperty(ref _servers, value);
        }

        public ObservableCollection<Sender> Senders
        {
            get => _senders;
            set => SetProperty(ref _senders, value);
        }

        public Recipient SelectedRecipient
        {
            get => _selectedRecipient;
            set => SetProperty(ref _selectedRecipient, value);
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                SetProperty(ref _filterText, value);
                FilterRecipients();
            }
        }

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
        public DelegateCommand RecipientEditCommand { get; }

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