using Prism.Mvvm;

namespace MailSender.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private int _selectedTabIndex;

        /// <summary>
        ///     Индекс главного TabControl
        /// </summary>
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }
    }
}