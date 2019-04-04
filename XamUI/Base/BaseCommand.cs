using System;
using System.Windows.Input;

namespace XamUI
{
    public class BaseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<object> _action;
        private readonly Action<Exception> _onExceptionAction;
        private readonly TimeSpan _actionFrequency;
        private readonly bool _shouldSuppressExceptions;
        private DateTime _lastActionTime;
        private Func<bool> _canExecute;

        public BaseCommand(Action<object> action, TimeSpan? actionFrequency = null, bool shouldSuppressExceptions = false, Action<Exception> onExceptionAction = null, Func<bool> canExecute = null)
        {
            _action = action;
            _actionFrequency = actionFrequency ?? TimeSpan.Zero;
            _shouldSuppressExceptions = shouldSuppressExceptions;
            _onExceptionAction = onExceptionAction;
            _canExecute = canExecute;
        }

        public BaseCommand(Action action, TimeSpan? actionFrequency = null, bool shouldSuppressExceptions = false, Action<Exception> onExceptionAction = null, Func<bool> canExecute = null) : this(p => action?.Invoke(), actionFrequency, shouldSuppressExceptions, onExceptionAction, canExecute)
        {
        }

        public bool CanExecute(object parameter)
            => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter)
        {
            var nowTime = DateTime.UtcNow;
            if (_actionFrequency == TimeSpan.Zero
                || Math.Abs((nowTime - _lastActionTime).TotalMilliseconds) >= _actionFrequency.TotalMilliseconds)
            {
                _lastActionTime = nowTime;
                try
                {
                    _action.Invoke(parameter);
                }
                catch (Exception ex)
                {
                    _onExceptionAction?.Invoke(ex);
                    if (_shouldSuppressExceptions)
                    {
                        return;
                    }
                    throw ex;
                }
            }
        }

        public void ChangeCanExecute(bool value)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}