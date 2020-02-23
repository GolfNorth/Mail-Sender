using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace MailSender.Infrastructure.Behaviors
{
    public class TextBoxChangedBehavior : Behavior<TextBox>
    {
        private string _originalText;

        /// <summary>
        ///     Прикрепление к текстовому полю
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            _originalText = AssociatedObject.Text;

            AssociatedObject.GotFocus += AssociatedObjectOnGotFocus;
            AssociatedObject.LostFocus += AssociatedObjectOnLostFocus;
        }

        /// <summary>
        ///     Открепление от текстовому полю
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.GotFocus -= AssociatedObjectOnGotFocus;
            AssociatedObject.LostFocus -= AssociatedObjectOnLostFocus;
        }

        /// <summary>
        ///     Получение фокуса клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObjectOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (InfrastructureUtilities.IsValid(AssociatedObject)) return;

            AssociatedObject.Text = _originalText;
            AssociatedObject.Focus();

            SystemSounds.Hand.Play();
        }

        /// <summary>
        ///     Потеря фокуса клавиатуры
        /// </summary>
        private void AssociatedObjectOnGotFocus(object sender, RoutedEventArgs e)
        {
            _originalText = AssociatedObject.Text;
        }



    }
}
