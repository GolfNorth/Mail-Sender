using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class WindowRecipientEditor : IEntityEditor<Recipient>
    {
        public bool Edit(ref Recipient recipient)
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;

            var editor = new RecipientEditorWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = currentMainWindow
            };

            if (recipient.Id == 0)
                editor.IdRow.Height = new GridLength(0);

            if (editor.ShowDialog() != true) return false;

            recipient.Name = editor.NameEditor.Text;
            recipient.Address = editor.AddressEditor.Text;

            return true;
        }
    }
}
