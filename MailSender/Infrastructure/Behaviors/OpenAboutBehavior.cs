using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using MailSender.Views;

namespace MailSender.Infrastructure.Behaviors
{
    public class OpenAboutBehavior : Behavior<MenuItem>
    {
        /// <summary>
        ///     Прикрепление к кнопке
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Click += AssociatedObjectOnClick;
        }

        /// <summary>
        ///     Открепление от кнопки
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Click -= AssociatedObjectOnClick;
        }

        private void AssociatedObjectOnClick(object sender, RoutedEventArgs e)
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;

            var aboutWindow = new AboutWindow()
            {
                Owner = currentMainWindow
            };

            aboutWindow.ShowDialog();
        }
    }
}
