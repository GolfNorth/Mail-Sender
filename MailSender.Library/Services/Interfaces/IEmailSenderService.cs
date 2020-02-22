using MailSender.Library.Entities;

namespace MailSender.Library.Services.Interfaces
{
    public interface IEmailSenderService
    {
        /// <summary>
        ///     Получене экзмепляра класса отправки сообщений
        /// </summary>
        /// <param name="server">Сервер для отправки</param>
        /// <returns>Экзмепляр класса отправки сообщений</returns>
        IEmailSender GetSender(Server server);
    }
}
