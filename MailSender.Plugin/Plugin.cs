using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MailSender.Library.Services.Interfaces;

namespace MailSender.Plugin
{
    public class Plugin : IPlugin
    {
        public async Task InitializeAsync()
        {
            await Task.Run(() => Debug.WriteLine("Плагин проиизиализирован"));
        }

        public async Task StartAsync()
        {
            await Task.Run(() => Debug.WriteLine("Плагин запущен"));
        }

        public async Task StopAsync()
        {
            await Task.Run(() => Debug.WriteLine("Плагин остановлен"));
        }
    }
}
