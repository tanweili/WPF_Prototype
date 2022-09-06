using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;

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

        private string FixtureType;

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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_BoardDirectoryValue))
            {
                Result1.Text = "Error";
                Result1.Foreground = Brushes.Red;
                MessageBox.Show("Please select a board folder");
                return;
            }
            Result1.Text = "Okay";
            Result1.Foreground = Brushes.Green;
        }

        private void ShowDir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(_BoardDirectoryValue);
        }

        private void SampleLogs_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo process = new ProcessStartInfo("cmd.exe");
            process.UseShellExecute = false;
            process.RedirectStandardInput = true;
            process.RedirectStandardOutput = true;
            process.CreateNoWindow = true;

            var p1 = Process.Start(process);
            p1.StandardInput.WriteLine("set");
            p1.StandardInput.WriteLine("exit");
            LogsTextBox.Text = p1.StandardOutput.ReadToEnd();
        }

        private void FixtureType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            FixtureType = ((ComboBoxItem)FixtureTypeComboBox.SelectedItem).Content.ToString();
            MessageBox.Show("Fixture type is: " + FixtureType);
        }
    }
}
