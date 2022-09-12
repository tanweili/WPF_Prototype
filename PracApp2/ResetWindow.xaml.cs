using System;
using System.Windows;

namespace PracApp2
{
    /// <summary>
    /// Interaction logic for ResetWindow.xaml
    /// </summary>
    public partial class ResetWindow : Window
    {
        public ResetWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Owner).resetDisplay();
            ((MainWindow)Owner).resetFolder();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
