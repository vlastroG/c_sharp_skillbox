﻿using System.Windows;

namespace PhoneBook.Desktop.Services
{
    public class MessageBoxService
    {
        public MessageBoxService()
        {

        }


        public void ShowInfo(string message, string title)
        {
            MessageBox.Show(
                Application.Current.MainWindow,
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }


        public void ShowError(string message, string title)
        {
            MessageBox.Show(
                Application.Current.MainWindow,
                message,
                title,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
