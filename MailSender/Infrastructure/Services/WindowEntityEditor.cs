using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities.Base;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public abstract class WindowEntityEditor<T> : IEntityEditor<T> where T : BaseEntity
    {
        public void Edit()
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
