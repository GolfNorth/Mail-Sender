using System.Windows;

namespace MailSender.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /*
        private void Button_Click(object s, RoutedEventArgs e)
        {
            var recipient = RecipientsList.SelectedItem as Recipient;
            var sender = SendersList.SelectedItem as Sender;
            var server = ServersList.SelectedItem as Server;

            if (recipient is null || server is null || sender is null) return;

            try
            {
                var mail_sender = new EmailSend(server.Host, server.Port, server.Login,
                    server.Password.Decode(3), server.EnableSsl);

                mail_sender.SendMail(sender.Address, recipient.Address, MailSubject.Text, MailBody.Text);

                Debug.WriteLine("Sended");
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
            }
        }
        */
    }
}