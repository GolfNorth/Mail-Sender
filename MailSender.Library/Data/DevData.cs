using System.Collections.Generic;
using MailSender.Library.Entities;
using MailSender.Library.Service;

namespace MailSender.Library.Data
{
    public class DevData
    {
        public static List<Server> Servers { get; } = new List<Server>
        {
            new Server
            {
                Id = 1, Name = "Localhost", Host = "127.0.0.1", Port = 25, Login = "", Password = "".Encode(3),
                EnableSsl = false
            },
            new Server
            {
                Id = 2, Name = "Яндекс", Host = "smtp.yandex.ru", Port = 587, Login = "", Password = "".Encode(3),
                EnableSsl = true
            },
            new Server
            {
                Id = 3, Name = "Mail.ru", Host = "smtp.mail.ru", Port = 587, Login = "", Password = "".Encode(3),
                EnableSsl = true
            },
            new Server
            {
                Id = 4, Name = "GMail", Host = "smtp.gmail.com", Port = 587, Login = "", Password = "".Encode(3),
                EnableSsl = true
            }
        };

        public static List<Sender> Senders { get; } = new List<Sender>
        {
            new Sender {Id = 1, Name = "Иванов", Address = "ivanov@server.ru"},
            new Sender {Id = 2, Name = "Петров", Address = "petrov@server.ru"},
            new Sender {Id = 3, Name = "Сидоров", Address = "sidorov@server.ru"}
        };

        public static List<Recipient> Recipients { get; } = new List<Recipient>
        {
            new Recipient {Id = 1, Name = "Иванов", Address = "ivanov@server.ru"},
            new Recipient {Id = 2, Name = "Петров", Address = "petrov@server.ru"},
            new Recipient {Id = 3, Name = "Сидоров", Address = "sidorov@server.ru"}
        };
    }
}