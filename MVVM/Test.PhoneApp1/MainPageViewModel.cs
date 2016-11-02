using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Mvvm;

namespace Test.PhoneApp1
{
    public class MainPageViewModel : NotifyPropertyChangedBase
    {
        private string _name;
        private double _score;
        private DateTime _birthday;
        private ObservableCollection<int> _items;
        private ActionCommand<object> _pageLoadedCommand;
        private readonly Random _random = new Random();
        private ActionCommand<object> _randomCommand;
        private ActionCommand<object> _runCommand;
        private DispatcherTimer _timer;
        private bool _runButtonIsEnabled;

        //code snippet: vmp
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, "Name"); }
        }

        public double Score
        {
            get { return _score; }
            set { SetProperty(ref _score, value, "Score"); }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set { SetProperty(ref _birthday, value, "Birthday"); }
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

        //code snippet: vmc
        public ICommand PageLoadedCommand
        {
            get { return _pageLoadedCommand ?? (_pageLoadedCommand = new ActionCommand<object>(PageLoaded)); }
        }

        public ICommand RandomCommand
        {
            get { return _randomCommand ?? (_randomCommand = new ActionCommand<object>(RandomGen)); }
        }

        //code snippet: vmcc
        public ICommand RunCommand
        {
            get { return _runCommand ?? (_runCommand = new ActionCommand<object>(Run, CanRun)); }
        }

        private bool CanRun(object arg)
        {
            return RunButtonIsEnabled;
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
                _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                _timer.Tick += TimerCallback;
                _timer.Start();
            }
        }

        private void TimerCallback(object sender, EventArgs e)
        {
            Foo();
        }

        private void RandomGen(object obj)
        {
            Foo();
        }

        private void Foo()
        {
            Name = "Bob " + _random.Next().ToString(CultureInfo.InvariantCulture);
            Birthday = Birthday.AddDays(_random.Next(10));
            Score = (float)_random.NextDouble();
            var n = _random.Next(9);
            Items[n] = _random.Next();
        }

        private void PageLoaded(object obj)
        {
            Name = "Bob";
            Score = 100.0;
            Birthday = new DateTime(1900, 1, 1);
            Items = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            RunButtonIsEnabled = true;
        }
    }
}
