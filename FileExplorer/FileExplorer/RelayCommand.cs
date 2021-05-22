using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileExplorer
{
    internal class RelayCommand<T> : ICommand
    {
       
        private readonly Action<T> _Execute;
        private readonly Predicate<T> _CanExecute ;

        public RelayCommand()
        {
        }

        public RelayCommand(Action<T> execute)
        : this(execute, null)
        {
        }

      
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this._Execute = execute;
            this._CanExecute = canExecute;
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this._Execute != null)
                    CommandManager.RequerySuggested += value;
            }

            remove
            {
                if (this._CanExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
     

     
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this._CanExecute == null ? true : this._CanExecute((T)parameter);
        }

     
        public void Execute(object parameter)
        {
            this._Execute((T)parameter);
        }
       
    }

   
    internal class RelayCommand : ICommand
    {
      
        private readonly Action mExecute;
        private readonly Func<bool> mCanExecute;
       
        public RelayCommand(Action execute)
          : this(execute, null)
        {
        }

        public RelayCommand(RelayCommand inputRC)
          : this(inputRC.mExecute, inputRC.mCanExecute)
        {
        }

       
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.mExecute = execute;
            this.mCanExecute = canExecute;
        }

       
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.mCanExecute != null)
                    CommandManager.RequerySuggested += value;
            }

            remove
            {
                if (this.mCanExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
       
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.mCanExecute == null ? true : this.mCanExecute();
        }

        public void Execute(object parameter)
        {
            this.mExecute();
        }
      
    }
}

