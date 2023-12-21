using System.Collections.ObjectModel;
using System.Windows.Input;
using vlastroG.WPF.Commands;
using vlastroG.WPF.ViewModels;

namespace WordsHandler.ViewModels {
    internal class MainWindowViewModel : BaseViewModel {
        public MainWindowViewModel() : base() {
            SplitLineCommand = new LambdaCommand(SplitLine, CanSplitLine);
            ReverseLineCommand = new LambdaCommand(ReverseLine, CanReverseLine);
        }


        private string _lineForSplit = string.Empty;
        public string LineForSplit {
            get { return _lineForSplit; }
            set { Set(ref _lineForSplit, value); }
        }

        public ObservableCollection<string> SplittedWords { get; } = new ObservableCollection<string>();


        private string _lineForReverse = string.Empty;
        public string LineForReverse {
            get { return _lineForReverse; }
            set { Set(ref _lineForReverse, value); }
        }

        private string _reversedLine = string.Empty;
        public string ReversedLine {
            get { return _reversedLine; }
            set { Set(ref _reversedLine, value); }
        }


        public ICommand SplitLineCommand { get; }

        public ICommand ReverseLineCommand { get; }


        private void SplitLine(object? obj) {
            SplittedWords.Clear();
            string[] words = LineForSplit
                .Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToArray();
            Array.ForEach(words, item => SplittedWords.Add(item));
        }

        private bool CanSplitLine(object? obj) => !string.IsNullOrWhiteSpace(LineForSplit);


        private void ReverseLine(object? obj) {
            ReversedLine = string.Join(' ', LineForReverse.Split(new char[] { ' ', '\n' }).Reverse());
        }

        private bool CanReverseLine(object? obj) => !string.IsNullOrWhiteSpace(LineForReverse);
    }
}
