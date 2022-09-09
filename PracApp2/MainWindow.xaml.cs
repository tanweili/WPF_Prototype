﻿using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System;
using System.IO;
using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel.Design.Serialization;
using System.IO.Compression;

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
            }
        }

        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_BoardDirectoryValue))
            {
                displayError(Backup_Result);
                MessageBox.Show("Please select a board folder");
                return;
            }
            try
            {
                string parentFolder = System.IO.Directory.GetParent(_BoardDirectoryValue).FullName;
                string folderName = _BoardDirectoryValue.Replace(parentFolder, "");
                string backupFolder = parentFolder + String.Format("{0}_testBackup.zip", folderName);
                ZipFile.CreateFromDirectory(_BoardDirectoryValue, backupFolder);
                displayOkay(Backup_Result);
                LogsTextBox.AppendText($"Backup folder {folderName}_backup.zip created in {parentFolder}.");
            }
            catch (Exception err)
            {
                displayError(Backup_Result);
                MessageBox.Show("Error while creating backup zip file. Please delete the previous backup in the same directory if it exists.");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        { 
            if (Backup_Result.Text != "Okay")
            {
                displayError(Edit_Result);
                MessageBox.Show("Please backup the folder first.");
                return;
            }
            try
            {
                var fileContents = System.IO.File.ReadAllText(_BoardDirectoryValue + @"\board");
                fileContents = fileContents.Replace("World", "World after editing this file!");
                System.IO.File.WriteAllText(_BoardDirectoryValue + @"\board_after_editing", fileContents);
                displayOkay(Edit_Result);
            } catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                displayError(Edit_Result);
            }
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
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            resetDisplay();
        }

        private void displayOkay(TextBox textBox)
        {
            textBox.Text = "Okay";
            textBox.Foreground = Brushes.Green;
            textBox.FontWeight = FontWeights.Bold;
        }

        private void displayError(TextBox textBox)
        {
            textBox.Text = "Error";
            textBox.Foreground = Brushes.Red;
            textBox.FontWeight = FontWeights.Bold;
        }

        private void resetDisplay()
        {
            foreach (object child in leftPanel.Children)
            {
                if (child is TextBox)
                    (child as TextBox).Text = "";
            }
        }

        private void SaveLogsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
