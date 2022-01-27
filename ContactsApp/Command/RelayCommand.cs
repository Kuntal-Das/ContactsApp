using System;
using System.Windows.Input;

namespace ContactsApp.Command
{
    //public class RelayCommand<T> : ICommand
    //{
    //    Action<T> executeAction;
    //    Func<T, bool> canExecute;
    //    bool canExecuteCache;
    //    public RelayCommand(Action<T> executeAction, Func<T, bool> canExecute, bool canExecuteCache)
    //    {
    //        this.canExecute = canExecute;
    //        this.executeAction = executeAction;
    //        this.canExecuteCache = canExecuteCache;
    //    }

    //    public event EventHandler? CanExecuteChanged
    //    {
    //        add => CommandManager.RequerySuggested += value;
    //        remove => CommandManager.RequerySuggested -= value;
    //    }

    //    public bool CanExecute(object? parameter)
    //    {
    //        return canExecute == null ? true : canExecute((T)parameter);
    //    }

    //    public void Execute(object? parameter)
    //    {
    //        executeAction((T)parameter);
    //    }
    //}

    public class RelayCommand : ICommand
    {
        Action<object> executeAction;
        Func<object, bool> canExecute;
        bool canExecuteCache;
        public RelayCommand(Action<object> executeAction, Func<object, bool> canExecute, bool canExecuteCache)
        {
            this.canExecute = canExecute;
            this.executeAction = executeAction;
            this.canExecuteCache = canExecuteCache;
        }
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => canExecute == null ? true : canExecute(parameter);

        public void Execute(object parameter) => executeAction(parameter);
    }
}
