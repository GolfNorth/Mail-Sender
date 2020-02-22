using System;
using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class WindowSenderEditor : IEntityEditor<Sender>
    {
        public bool Edit(ref Sender sender)
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;

            var editor = new SenderEditorWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = currentMainWindow
            };

            if (sender.Id == 0)
                editor.IdRow.Height = new GridLength(0);

            if (editor.ShowDialog() != true) return false;

            try
            {
                sender.Name = editor.NameEditor.Text;
                sender.Address = editor.AddressEditor.Text;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
