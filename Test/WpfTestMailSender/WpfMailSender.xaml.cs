using System;
using System.Windows;
using System.Windows.Media;
using MailSender.Library.Services;

namespace WpfTestMailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var messageSubject = MessageSubject.Text;
            var messageBody = MessageBody.Text;
            var userName = UserNameEdit.Text;
            var userPassword = PasswordEdit.SecurePassword;

            var emailService = new EmailSend(SMTPServer.Host, SMTPServer.Port, userName, userPassword);

            try
            {
                emailService.SendMail(SMTPServer.From, SMTPServer.To, messageSubject, messageBody);

                var sew = new SendEndWindow("Работа завершена", this);

                sew.ShowDialog();
            }
            catch (Exception error)
            {
                var sew = new SendEndWindow(error.Message, this, new SolidColorBrush(Colors.Red));

                sew.ShowDialog();
            }
        }
    }
}
