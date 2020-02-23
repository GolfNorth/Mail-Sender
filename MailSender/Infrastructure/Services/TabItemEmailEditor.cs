using System.Windows;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using MailSender.Views;

namespace MailSender.Infrastructure.Services
{
    public class TabItemEmailEditor : IEntityEditor<Email>
    {
        public bool Edit(ref Email email)
        {
            var currentMainWindow = (MainWindow)Application.Current.MainWindow;

            return InfrastructureUtilities.IsValid(currentMainWindow?.EmailSubject);
        }
    }
}
