using System;
using System.Collections.Generic;
using System.Windows;
using MailSender.Library.Services.Interfaces;
using MailSender.Views;

namespace MailSender
{
    public class WindowsService : IWindowsService
    {
        /// <summary>
        ///     Коллекция окон доступных для открытия
        /// </summary>
        public Dictionary<string, Type> Windows = new Dictionary<string, Type>()
        {
            {"MainWindow", typeof(MainWindow)},
            {"RecipientEditorWindow", typeof(RecipientEditorWindow)}

        };

        public void Show(string windowName)
        {
            if (!Windows.ContainsKey(windowName)) return;

            var window =  Activator.CreateInstance(Windows[windowName]) as Window;

            window?.Show();
        }

        public void ShowDialog(string windowName)
        {
            if (!Windows.ContainsKey(windowName)) return;

            var window = Activator.CreateInstance(Windows[windowName]) as Window;

            window?.ShowDialog();
        }
    }
}
