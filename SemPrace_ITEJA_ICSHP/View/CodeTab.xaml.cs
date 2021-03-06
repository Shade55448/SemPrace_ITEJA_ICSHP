﻿using GUI.Model;
using GUI.Services;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for CodeTab.xaml
    /// </summary>
    public partial class CodeTab : UserControl
    {
        private string currentFile;
        public CodeTab(string code) //Tab for new file code or existing code
        {
            InitializeComponent();
            txtBox_Code.Text = code;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFile))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text file (*.txt)|*.txt|Script file (*.script)|*.script";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (saveFileDialog.ShowDialog() == true)
                {
                    currentFile = saveFileDialog.FileName;
                }
            }
            File.WriteAllText(currentFile, txtBox_Code.Text);
        }

        private void btnCompile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageViewCanvas img = new ImageViewCanvas();
                img.Show();
                LanguageExecutor.Compile(txtBox_Code.Text, new DrawingService(img.ImgCanvas));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Compilation fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
