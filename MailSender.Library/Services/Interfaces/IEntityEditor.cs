namespace MailSender.Infrastructure.Services.Interfaces
{
    /// <summary>
    ///     Интерфейс редактора объекта сущности
    /// </summary>
    /// <typeparam name="T">Тип редактируемой сущности</typeparam>
    public interface IEntityEditor<T>
    {
        /// <summary>
        ///     Функция запуска редактирования объекта
        /// </summary>
        bool Edit(ref T item);
    }
}