using System;
using System.Windows;
using LissajousApp.ViewModels;

namespace LissajousApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}