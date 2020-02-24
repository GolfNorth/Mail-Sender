using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using MailSender.Library.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MailSender
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        private List<IPlugin> _pligins;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _pligins = await Task.Run(GetPlugins).ConfigureAwait(false);
            if (_pligins.Count == 0) return;

            await InitializePluginsAsync(_pligins).ConfigureAwait(false);
            await StartPluginsAsync(_pligins).ConfigureAwait(false);
        }

        private static List<IPlugin> GetPlugins()
        {
            const string pluginsDir = "Plugins";
            var directory = new DirectoryInfo(pluginsDir);

            var result = new List<IPlugin>();
            if (!directory.Exists) return result;

            result.AddRange(from dll in directory.EnumerateFiles("*.dll")
                select Assembly.LoadFile(dll.FullName)
                into pluginDll
                from pluginType in pluginDll.GetTypes().Where(t => t.GetInterfaces().Any(i => i == typeof(IPlugin)))
                select Activator.CreateInstance(pluginType) as IPlugin
                into plugin
                where !(plugin is null)
                select plugin);

            return result;
        }

        private static async Task InitializePluginsAsync(IEnumerable<IPlugin> Plugins)
        {
            foreach (var plugin in Plugins)
                try
                {
                    await plugin.InitializeAsync();
                }
                catch (Exception e)
                {
                    Trace.TraceError("Ошибка при инициализации плагина {0}: {1}", plugin.GetType(), e);
                }

        }

        private static async Task StartPluginsAsync(IEnumerable<IPlugin> Plugins)
        {
            foreach (var plugin in Plugins)
                try
                {
                    await plugin.StartAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Trace.TraceError("Ошибка при запуске плагина {0}: {1}", plugin.GetType(), e);
                }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (_pligins.Count == 0) return;
            await StopPluginsAsync(_pligins).ConfigureAwait(false);
        }

        private static async Task StopPluginsAsync(IEnumerable<IPlugin> Plugins)
        {
            foreach (var plugin in Plugins)
                try
                {
                    await plugin.StopAsync();
                }
                catch (Exception e)
                {
                    Trace.TraceError("Ошибка при остановке плагина {0}: {1}", plugin.GetType(), e);
                }
        }
    }
}