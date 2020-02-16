using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class WindowRecipientEditor : IEntityEditor<Recipient>
    {
        private Window _editor;

        public void Edit(ref Recipient recipient)
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;
            _editor = new RecipientEditorWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = currentMainWindow
            };

            _editor.ShowDialog();
        }

        public void Close()
        {
            _editor.Close();
        }
    }
}
