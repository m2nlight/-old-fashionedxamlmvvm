using System;
using System.Windows.Input;

namespace Mvvm
{
    public class ActionCommand<T> : ICommand
    {
        private readonly Action<T> _executeAction;
        private readonly Func<T, bool> _canExecuteAction;

        public ActionCommand(Action<T> executeAction)
            : this(executeAction, null)
        {
        }

        public ActionCommand(Action<T> executeAction, Func<T, bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }


        public bool CanExecute(object parameter)
        {
            if (_canExecuteAction != null)
            {
                return _canExecuteAction((T)parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (_executeAction != null)
            {
                _executeAction((T)parameter);
            }
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged(EventArgs e)
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, e);
        }
    }
}
