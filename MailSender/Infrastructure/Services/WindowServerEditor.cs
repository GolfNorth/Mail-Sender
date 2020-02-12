using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class WindowServerEditor : IEntityEditor<Server>
    {
        public void Edit()
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;
            var editor = new ServerEditorWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = currentMainWindow
            };

            editor.ShowDialog();
        }
    }
}
