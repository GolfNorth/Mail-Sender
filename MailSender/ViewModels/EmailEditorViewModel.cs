using System.Collections.ObjectModel;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class EmailEditorViewModel : BindableBase
    {
        private ObservableCollection<Email> _emails; // Коллекция сообщений
        private Email _selectedEmail; // Выбранное сообщение

        public EmailEditorViewModel(IEntityManager<Email> emailsManager)
        {
            Emails = new ObservableCollection<Email>(emailsManager.GetAll());

            // Добавление сообщения
            AddEmailCommand = new DelegateCommand(() =>
            {
                var newEmail = new Email()
                {
                    Subject = "New Email"
                };

                SelectedEmail = newEmail;

                emailsManager.Add(newEmail);
                emailsManager.SaveChanges();

                Emails = new ObservableCollection<Email>(emailsManager.GetAll());
            }, () => Emails != null).ObservesProperty(() => Emails);

            // Удаление сообщения
            RemoveEmailCommand = new DelegateCommand(() =>
            {
                emailsManager.Remove(SelectedEmail);
                emailsManager.SaveChanges();

                SelectedEmail = null;

                Emails = new ObservableCollection<Email>(emailsManager.GetAll());
            }, () => SelectedEmail != null).ObservesProperty(() => SelectedEmail);

            // Сохранение сообщения
            EditEmailCommand = new DelegateCommand(() =>
            {
                emailsManager.Edit(SelectedEmail);
                emailsManager.SaveChanges();

                Emails = new ObservableCollection<Email>(emailsManager.GetAll());
            });
        }

        /// <summary>
        ///     Коллекция сообщений
        /// </summary>
        public ObservableCollection<Email> Emails
        {
            get => _emails;
            private set => SetProperty(ref _emails, value);
        }

        /// <summary>
        ///     Выбранное сообщение
        /// </summary>
        public Email SelectedEmail
        {
            get => _selectedEmail;
            set => SetProperty(ref _selectedEmail, value);
        }

        /// <summary>
        ///     Команда добавления сообщения
        /// </summary>
        public DelegateCommand AddEmailCommand { get; }

        /// <summary>
        ///     Команда удаления сообщения
        /// </summary>
        public DelegateCommand RemoveEmailCommand { get; }

        /// <summary>
        ///     Команда сохранения сообщения
        /// </summary>
        public DelegateCommand EditEmailCommand { get; }
    }
}