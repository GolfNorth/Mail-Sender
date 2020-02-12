using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class WindowRecipientEditor : IEntityEditor<Recipient>
    {
        public void Edit(Recipient recipient)
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;
            var editor = new RecipientEditorWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = currentMainWindow
            };

            editor.ShowDialog();
        }
    }
}
