using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class MailSenderVM : BindableBase
    {
        /// <summary>
        /// Индекс главного TabControl
        /// </summary>
        public int SelectedTabIndex { get; set; } = 0;

        /// <summary>
        /// Изменение текущего TabItem
        /// </summary>
        /// <param name="index"></param>
        public void SwitchTabIndex(string index)
        {

            Debug.WriteLine(123);
            SelectedTabIndex = int.Parse(index);// index.ToIn;

            RaisePropertyChanged(nameof(SelectedTabIndex));
        }
    }
}
