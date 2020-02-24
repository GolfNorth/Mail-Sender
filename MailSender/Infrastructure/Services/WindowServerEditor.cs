using System;
using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class WindowServerEditor : IEntityEditor<Server>
    {
        public bool Edit(ref Server server)
        {
            var currentMainWindow = (MainWindow) Application.Current.MainWindow;

            var editor = new ServerEditorWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = currentMainWindow
            };

            if (server.Id == 0)
                editor.IdRow.Height = new GridLength(0);

            if (editor.ShowDialog() != true) return false;

            try
            {
                server.Name = editor.NameEditor.Text;
                server.Host = editor.HostEditor.Text;
                server.Port = int.Parse(editor.PortEditor.Text);
                server.EnableSsl = editor.EnableSslEditor.IsChecked ?? false;
                server.Login = editor.LoginEditor.Text;
                server.Password = editor.PasswordEditor.Text;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}