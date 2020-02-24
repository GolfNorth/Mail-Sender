using System.Media;
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

        /// <summary>
        ///     Обработка события клика кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void AssociatedObjectOnClick(object sender, RoutedEventArgs e)
        {
            var window = InfrastructureUtilities.GetWindow(sender as DependencyObject);

            if (!InfrastructureUtilities.IsValid(window))
            {
                SystemSounds.Hand.Play();

                return;
            }

            window.DialogResult = true;
            window.Close();
        }
    }
}