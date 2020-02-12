using System.Windows;
using System.Windows.Controls;

namespace MailSender.Views
{
    /// <summary>
    /// Логика взаимодействия для SenderEditorWindow.xaml
    /// </summary>
    public partial class SenderEditorWindow : Window
    {
        public SenderEditorWindow()
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
