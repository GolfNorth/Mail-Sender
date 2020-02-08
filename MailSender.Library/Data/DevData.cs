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
                Id = 0, Name = "Localhost", Host = "127.0.0.1", Port = 25, Login = "", Password = "".Encode(3),
                EnableSsl = false
            }
        };

        public static List<Sender> Senders { get; } = new List<Sender>
        {
            new Sender {Id = 0, Name = "Иванов", Address = "ivanov@server.ru"},
            new Sender {Id = 1, Name = "Петров", Address = "petrov@server.ru"},
            new Sender {Id = 2, Name = "Сидоров", Address = "sidorov@server.ru"}
        };

        public static List<Recipient> Recipients { get; } = new List<Recipient>
        {
            new Recipient {Id = 0, Name = "Иванов", Address = "ivanov@server.ru"},
            new Recipient {Id = 1, Name = "Петров", Address = "petrov@server.ru"},
            new Recipient {Id = 2, Name = "Сидоров", Address = "sidorov@server.ru"}
        };
    }
}