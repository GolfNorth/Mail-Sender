using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace MailSender.Infrastructure.Behaviors
{
    public class SaveChangesBehavior : Behavior<Button>
    {
        /// <summary>
        ///     Прикрепление к кнопке
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Click += AssociatedObject_Click;
        }

        /// <summary>
        ///     Открепление от кнопки
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Click -= AssociatedObject_Click;
        }

        /// <summary>
        ///     Обработка события клика кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            var window = GetWindow(sender as DependencyObject);
            window.DialogResult = true;
            window.Close();
        }

        /// <summary>
        ///     Получение объекта окна по отправителю
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <returns>Экземпляр окна</returns>
        private static Window GetWindow(DependencyObject sender)
        {
            Window window = null;

            if (sender is Window)
                window = (Window)sender;

            return window ?? Window.GetWindow(sender);
        }
    }
}
