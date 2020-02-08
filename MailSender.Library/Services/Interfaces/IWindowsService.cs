namespace MailSender.Library.Services.Interfaces
{
    /// <summary>
    ///     Интерфейс сервиса управления окнами
    /// </summary>
    public interface IWindowsService
    {
        /// <summary>
        ///     Открыть окно
        /// </summary>
        /// <param name="window">Имя класса типа Window</param>
        void Show(string window);

        /// <summary>
        ///     Открыть окно модально
        /// </summary>
        /// <param name="window">Имя класса типа Window</param>
        void ShowDialog(string window);
    }
}