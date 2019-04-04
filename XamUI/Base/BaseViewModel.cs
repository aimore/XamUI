using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace XamUI
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Lazy<ConcurrentDictionary<Type, object>> _lazyPropertiesMapping = new Lazy<ConcurrentDictionary<Type, object>>(() => new ConcurrentDictionary<Type, object>());

        #region Properties

        protected T Get<T>(T defaultValue = default(T), [CallerMemberName] string key = null)
            => GetTypeDict<T>().TryGetValue(key, out T val)
                ? val
                : defaultValue;

        protected bool Set<T>(T value, bool shouldEqual = true, bool shouldRaisePropertyChanged = true, [CallerMemberName] string key = null)
        {
            var typeDict = GetTypeDict<T>();
            if (shouldEqual && typeDict.TryGetValue(key, out T oldValue) && EqualityComparer<T>.Default.Equals(oldValue, value))
            {
                return false;
            }
            typeDict[key] = value;
            if (shouldRaisePropertyChanged)
            {
                OnPropertyChanged(key);
            }
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string key = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));

        #endregion

        #region Commands

        /// <summary>
        /// Using: public ICommand YourCommand => RetCmd(() => new CustomCommand());
        /// </summary>
        protected ICommand RetCmd(Func<ICommand> commandCreator, [CallerMemberName] string key = null)
            => Cmd(key) ?? RegCmd(commandCreator, key);

        /// <summary>
        /// Using: public ICommand YourCommand => RetCmd(() => { ..action.. });
        /// </summary>
        protected ICommand RetCmd(Action<object> action, TimeSpan? actionFrequency = null, bool shouldSuppressExceptions = false, Action<Exception> onExceptionAction = null, Func<bool> canExecute = null, [CallerMemberName] string key = null)
            => Cmd(key) ?? RegCmd(action, actionFrequency, shouldSuppressExceptions, onExceptionAction, canExecute, key);

        /// <summary>
        /// Using: public ICommand YourCommand => RetCmd(() => { ..action.. });
        /// </summary>
        protected ICommand RetCmd(Action action, TimeSpan? actionFrequency = null, bool shouldSuppressExceptions = false, Action<Exception> onExceptionAction = null, Func<bool> canExecute = null, [CallerMemberName]  string key = null)
            => RetCmd(p => action?.Invoke(), actionFrequency, shouldSuppressExceptions, onExceptionAction, canExecute, key);

        /// <summary>
        /// Using: public ICommand YourCommand => Cmd() ?? RegCmd(() => { ..action.. });
        /// </summary>
        protected ICommand Cmd([CallerMemberName]  string key = null)
            => GetTypeDict<ICommand>().TryGetValue(key, out ICommand command)
                ? command
                : null;

        /// <summary>
        /// Using: public ICommand YourCommand => Cmd() ?? RegCmd(new CustomCommand());
        /// </summary>
        protected ICommand RegCmd(ICommand command, [CallerMemberName]  string key = null)
            => GetTypeDict<ICommand>()[key] = command;

        /// <summary>
        /// Using: public ICommand YourCommand => Cmd() ?? RegCmd(() => new CustomCommand());
        /// </summary>
        protected ICommand RegCmd(Func<ICommand> commandCreator, [CallerMemberName]  string key = null)
            => RegCmd(commandCreator?.Invoke());

        /// <summary>
        /// Using: public ICommand YourCommand => Cmd() ?? RegCmd(() => { ..action.. });
        /// </summary>
        protected ICommand RegCmd(Action<object> action, TimeSpan? actionFrequency = null, bool shouldSuppressExceptions = false, Action<Exception> onExceptionAction = null, Func<bool> canExecute = null, [CallerMemberName] string key = null)
            => RegCmd(new BaseCommand(p => action?.Invoke(p), actionFrequency, shouldSuppressExceptions, onExceptionAction, canExecute));

        /// <summary>
        /// Using: public ICommand YourCommand => Cmd() ?? RegCmd(() => { ..action.. });
        /// </summary>
        protected ICommand RegCmd(Action action, TimeSpan? actionFrequency = null, bool shouldSuppressExceptions = false, Action<Exception> onExceptionAction = null, Func<bool> canExecute = null, [CallerMemberName] string key = null)
            => RegCmd(p => action?.Invoke(), actionFrequency, shouldSuppressExceptions, onExceptionAction, canExecute, key);

        #endregion

        private ConcurrentDictionary<string, T> GetTypeDict<T>()
        {
            var type = typeof(T);
            if (!_lazyPropertiesMapping.Value.TryGetValue(type, out object valDict))
            {
                _lazyPropertiesMapping.Value[type] = valDict = new ConcurrentDictionary<string, T>();
            }
            return valDict as ConcurrentDictionary<string, T>;
        }
    }
}