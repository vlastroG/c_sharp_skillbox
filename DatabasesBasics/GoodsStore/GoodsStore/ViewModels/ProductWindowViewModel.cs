﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace GoodsStore.ViewModels {
    internal class ProductWindowViewModel : BaseViewModel, IDataErrorInfo {
        public ProductWindowViewModel(string title, string email) {
            if (string.IsNullOrWhiteSpace(title)) { throw new ArgumentNullException(nameof(title)); }
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentNullException(nameof(email)); }
            Title = title;
            Email = email;
            SaveCommand = new LambdaCommand(Save, CanSave);
            CancelCommand = new LambdaCommand(Cancel, CanCancel);
        }


        public string Title { get; }

        public ICommand SaveCommand { get; }

        public ICommand CancelCommand { get; }


        private string _name = string.Empty;
        public string Name {
            get => _name;
            set {
                if (Set(ref _name, value)) {
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        private string _productCode = string.Empty;
        public string ProductCode {
            get => _productCode;
            set {
                if (Set(ref _productCode, value)) {
                    OnPropertyChanged(nameof(Error));
                }
            }
        }

        public string Email { get; }

        public string Error {
            get {
                return GetType()
                    .GetProperties()
                    .Select(prop => this[prop.Name])
                    .FirstOrDefault(error => !string.IsNullOrWhiteSpace(error)) ?? string.Empty;
            }
        }

        public string this[string columnName] {
            get {
                var error = string.Empty;
                switch (columnName) {
                    case nameof(ProductCode):
                    case nameof(Name):
                        string? propertyValue = GetType()
                            .GetProperties()
                            .FirstOrDefault(prop => prop.Name == columnName)
                            ?.GetValue(this)
                            ?.ToString();
                        if (string.IsNullOrWhiteSpace(propertyValue)) {
                            error = "Поле не может быть пустым";
                        } else if (propertyValue.StartsWith(' ') || propertyValue.EndsWith(' ')) {
                            error = "Поле не может начинаться или заканчиваться пробелом";
                        }
                        break;
                }
                return error;
            }
        }


        private void Save(object p) {
            if (!CanSave(p)) { return; }

            var window = (Window)p!;
            window.DialogResult = true;
            window.Close();
        }
        private bool CanSave(object p) => string.IsNullOrWhiteSpace(Error) && (p is not null) && (p is Window);


        private void Cancel(object p) {
            if (!CanCancel(p)) { return; }

            var window = (Window)p!;
            window.DialogResult = false;
            window.Close();
        }
        private bool CanCancel(object p) => (p is not null) && (p is Window);
    }
}
