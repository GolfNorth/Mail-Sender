using System.Windows;
using System.Windows.Controls;

namespace MailSender.Views
{
    /// <summary>
    ///     Логика взаимодействия для SenderEditorWindow.xaml
    /// </summary>
    public partial class SenderEditorWindow : Window
    {
        public SenderEditorWindow()
        {
            InitializeComponent();
        }

        private void OnDataValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (!(e.Source is Control control)) return;

            if (e.Action == ValidationErrorEventAction.Added)
                control.ToolTip = e.Error.ErrorContent.ToString();
            else
                control.ClearValue(ToolTipProperty);
        }
    }
}