using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfTestMailSender
{
    /// <summary>
    /// Логика взаимодействия для SendEndWindow.xaml
    /// </summary>
    public partial class SendEndWindow : Window
    {
        public SendEndWindow(string content, Window owner = null, Brush brush = null)
        {
            InitializeComponent();

            lSendEnd.Content = content;

            if (owner != null)
                Owner = owner;

            if (brush != null)
                lSendEnd.Foreground = brush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
