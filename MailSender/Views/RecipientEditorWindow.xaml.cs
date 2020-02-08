using System.Windows;
using System.Windows.Controls;

namespace MailSender.Views
{
    /// <summary>
    ///     Логика взаимодействия для RecipientEditorWindow.xaml
    /// </summary>
    public partial class RecipientEditorWindow : Window
    {
        public RecipientEditorWindow()
        {
            InitializeComponent();
        }

        private void OnDataValidationError(object Sender, ValidationErrorEventArgs E)
        {
            if (!(E.Source is Control control)) return;

            if (E.Action == ValidationErrorEventAction.Added)
                control.ToolTip = E.Error.ErrorContent.ToString();
            else
                control.ClearValue(ToolTipProperty);
        }
    }
}