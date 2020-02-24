using System;
using System.Threading.Tasks;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Plugin
{
    public class Plugin : IPlugin
    {
        public async Task InitializeAsync()
        {
            await Task.Run(() => Console.WriteLine("Плагин проиизиализирован"));
        }

        public async Task StartAsync()
        {
            await Task.Run(() => Console.WriteLine("Плагин запущен"));
        }

        public async Task StopAsync()
        {
            await Task.Run(() => Console.WriteLine("Плагин остановлен"));
        }
    }
}
