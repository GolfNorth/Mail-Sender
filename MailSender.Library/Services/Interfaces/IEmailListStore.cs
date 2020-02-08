using MailSender.Library.Entities;

namespace MailSender.Library.Services.Interfaces
{
    /// <summary>
    /// Интерфейс хранилища списка рассылки
    /// </summary>
    public interface IEmailListStore : IEntityStore<EmailList>
    {
    }
}