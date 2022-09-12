using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracApp2
{
    /// <summary>
    /// Interaction logic for ResetWindow.xaml
    /// </summary>
    public partial class ResetWindow : Window
    {
        public ResetWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner).resetDisplay();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
