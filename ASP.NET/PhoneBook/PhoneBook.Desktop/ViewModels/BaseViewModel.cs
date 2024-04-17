using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneBook.Desktop.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel() { }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Назначает значение свойству модели представления, если оно имеет другое значение, и вызывает событие PropertyChanged
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">Поле модели представления, значение которого меняется через свойство</param>
        /// <param name="value">Значение, которое нужно установить</param>
        /// <param name="propertyName">Название свойства, значение которого надо изменить</param>
        /// <returns>True, если значение свойства изменено, иначе False</returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
