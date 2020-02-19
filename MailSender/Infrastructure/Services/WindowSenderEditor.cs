using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class WindowSenderEditor : IEntityEditor<Sender>
    {
        private Window _editor;

        public void Edit(ref Sender sender)
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;
            _editor = new SenderEditorWindow()
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
