﻿using GUI.Model;
using GUI.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for ShellTab.xaml
    /// </summary>
    public partial class ShellTab : UserControl
    {
        private ShellTabViewModel shellTabViewModel;
        public ShellTab()
        {
            InitializeComponent();

            shellTabViewModel = new ShellTabViewModel();
            DataContext = shellTabViewModel;
            txtBoxInput.Focus();
        }

        public string GetHistoryCodeWithEnd()
        {
            return GetHistoryCode() + "\n" + "END.";
        }
        private string GetHistoryCode()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in listViewHistory.Items)
            {
                builder.Append(item);
                builder.Append('\n');
            }
            return builder.ToString();
        }

        private void MyCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            shellTabViewModel.CanvasSizeText = (int)e.NewSize.Width + "x" + (int)e.NewSize.Height; //Update label canvas size on resize
        }

        private void TxtBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                txtBoxOutput.Clear();

                string output = GetHistoryCode();

                output += txtBoxInput.Text + "\n"; //Add new command to other commands
                output += "END.";
                try
                {
                    txtBoxOutput.Text = LanguageExecutor.Compile(output); //If compiled succesfuly
                    listViewHistory.Items.Add(txtBoxInput.Text);
                    txtBoxInput.Clear();
                    txtBoxInput.Focus();
                } catch (Exception ex) //TODO Uplně max super by byli vlastní exception
                {
                    txtBoxOutput.Text = "Compiler error: " + ex.Message;
                }
            } //TODO Funkce na kreslení a write draw atd atd
        }

        private void ListView_ClearClick(object sender, RoutedEventArgs e)
        {
            listViewHistory.Items.Clear();
        }
    }
}
