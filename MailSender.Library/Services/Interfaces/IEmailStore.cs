using MailSender.Library.Entities;

namespace MailSender.Library.Services.Interfaces
{
    /// <summary>
    /// Интерфейс хранилища электронных писем
    /// </summary>
    public interface IEmailStore : IEntityStore<Email>
    {
    }
}