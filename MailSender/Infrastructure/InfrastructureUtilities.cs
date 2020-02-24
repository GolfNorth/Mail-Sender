using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MailSender.Infrastructure
{
    public static class InfrastructureUtilities
    {
        /// <summary>
        ///     Получение объекта окна по отправителю
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <returns>Экземпляр окна</returns>
        public static Window GetWindow(DependencyObject sender)
        {
            return sender is Window window ? window : Window.GetWindow(sender);
        }

        /// <summary>
        ///     Проверка на валидность объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsValid(DependencyObject obj)
        {
            return !Validation.GetHasError(obj) &&
                   LogicalTreeHelper.GetChildren(obj)
                       .OfType<DependencyObject>()
                       .All(IsValid);
        }
    }
}