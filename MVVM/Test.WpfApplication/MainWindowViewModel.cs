using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Mvvm;

namespace Test.WpfApplication
{
    class MainWindowViewModel : NotifyPropertyChangedBase
    {
        private string _name;
        private DateTime? _birthday;
        private float _score;
        private ObservableCollection<int> _items;
        private ActionCommand<object> _runCommand;
        private readonly Random _random = new Random();
        private ActionCommand<object> _randomCommand;
        private DispatcherTimer _timer;
        private bool _runButtonIsEnabled;

        //code snippet: vmp
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, "Name"); }
        }

        public DateTime? Birthday
        {
            get { return _birthday; }
            set { SetProperty(ref _birthday, value, "Birthday"); }
        }

        public float Score
        {
            get { return _score; }
            set { SetProperty(ref _score, value, "Score"); }
        }

        public ObservableCollection<int> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value, "Items"); }
        }

        public bool RunButtonIsEnabled
        {
            get { return _runButtonIsEnabled; }
            set { SetProperty(ref _runButtonIsEnabled, value, "RunButtonIsEnabled"); }
        }

        //code snippet: vmcc
        public ICommand RunCommand
        {
            get { return _runCommand ?? (_runCommand = new ActionCommand<object>(Run, CanRun)); }
        }

        //code snippet: vmc
        public ICommand RandomCommand
        {
            get { return _randomCommand ?? (_randomCommand = new ActionCommand<object>(RandomGen)); }
        }

        private void Run(object obj)
        {
            if (MessageBoxResult.Cancel == MessageBox.Show("Continue?", "MVVM TEST", MessageBoxButton.OKCancel))
            {
                return;
            }
            RunButtonIsEnabled = false;
            if (_timer == null)
            {
                _timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Background, TimerCallback, Application.Current.Dispatcher);
            }
        }

        private bool CanRun(object arg)
        {
            return RunButtonIsEnabled;
        }

        private void RandomGen(object obj)
        {
            Foo();
        }

        private void TimerCallback(object sender, EventArgs e)
        {
            Foo();
        }

        private void Foo()
        {
            Name = "Bob " + _random.Next().ToString(CultureInfo.InvariantCulture);
            Birthday = Birthday.HasValue
                                      ? Birthday.Value.AddDays(_random.Next(10))
                                      : DateTime.Now;
            Score = (float)_random.NextDouble();
            var n = _random.Next(9);
            Items[n] = _random.Next();
        }

        public MainWindowViewModel()
        {
            this.Name = "Bob";
            this.Birthday = new DateTime(1990, 1, 1);
            this.Score = 99.9f;
            this.Items = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            this.RunButtonIsEnabled = true;
        }
    }
}
