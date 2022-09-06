using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PracApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string _BoardDirectoryValue;
        public string BoardDirectoryValue
        {
            get { return _BoardDirectoryValue; }
            set
            {
                _BoardDirectoryValue = value;
                OnPropertyChanged("BoardDirectoryValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        private void boardDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                BoardDirectoryValue = folderBrowserDialog.SelectedPath;
                System.Windows.MessageBox.Show(_BoardDirectoryValue);
            }
        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(_BoardDirectoryValue);
        }
    }
}
