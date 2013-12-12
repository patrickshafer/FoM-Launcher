using System;
using System.Windows.Input;

namespace FoM.Launcher.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<bool> _CanExecute;
        private readonly Func<object, bool> _CanExecuteP1;
        private readonly Action _ActionExecute;
        private readonly Action<object> _ActionExecuteP1;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action ActionExecute, Func<bool> CanExecute)
        {
            this._ActionExecute = ActionExecute;
            this._CanExecute = CanExecute;
        }

        public DelegateCommand(Action ActionExecute) : this(ActionExecute, () => true) { }

        public DelegateCommand(Action<object> ActionExecute, Func<object, bool> CanExecute)
        {
            this._ActionExecuteP1 = ActionExecute;
            this._CanExecuteP1 = CanExecute;
        }

        public void Execute(object parameter)
        {
            if (this._ActionExecute != null)
                this._ActionExecute();
            if (this._ActionExecuteP1 != null)
                this._ActionExecuteP1(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (this._CanExecute != null)
                return this._CanExecute();
            if (this._CanExecuteP1 != null)
                return this._CanExecuteP1(parameter);
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
