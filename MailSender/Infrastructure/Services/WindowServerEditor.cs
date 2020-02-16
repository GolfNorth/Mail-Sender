using System.Diagnostics;
using System.Threading;
using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class WindowServerEditor : IEntityEditor<Server>
    {
        private Window _editor;

        public void Edit(ref Server server)
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;
            _editor = new ServerEditorWindow()
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
