﻿using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Library.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class StatisticsViewModel : BindableBase
    {
        public StatisticsViewModel(IEntityManager<Recipient> recipientsManager, IEntityExportToExcel<Recipient> recipientsImporter)
        {
            // Загрузка списка получателей
            ExportRecipientsToExcel = new DelegateCommand(() =>
            {
                var recipients = recipientsManager.GetAll();

                recipientsImporter.ExportToExcel(recipients);
            });
        }

        /// <summary>
        ///     Загружает список получателей сообщений
        /// </summary>
        public DelegateCommand ExportRecipientsToExcel { get; }
    }
}