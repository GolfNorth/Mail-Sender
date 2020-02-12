using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class WindowSenderEditor : IEntityEditor<Sender>
    {
        public void Edit()
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;
            var editor = new SenderEditorWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = currentMainWindow
            };

            editor.ShowDialog();
        }
    }
}
