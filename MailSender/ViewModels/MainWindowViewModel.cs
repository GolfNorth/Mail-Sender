using System.Diagnostics;
using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        ///     Индекс главного TabControl
        /// </summary>
        public int SelectedTabIndex { get; set; }

        /// <summary>
        ///     Изменение текущего TabItem
        /// </summary>
        /// <param name="index"></param>
        public void SwitchTabIndex(string index)
        {
            Debug.WriteLine(123);
            SelectedTabIndex = int.Parse(index); // index.ToIn;

            RaisePropertyChanged(nameof(SelectedTabIndex));
        }
    }
}