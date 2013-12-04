using System;
using System.Windows.Input;

namespace FoM.Launcher.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Func<bool> _CanExecute;
        private readonly Action _ActionExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action ActionExecute, Func<bool> CanExecute)
        {
            this._ActionExecute = ActionExecute;
            this._CanExecute = CanExecute;
        }

        public DelegateCommand(Action ActionExecute) : this(ActionExecute, () => true) { }

        public void Execute(object parameter)
        {
            this._ActionExecute();
        }

        public bool CanExecute(object parameter)
        {
            return this._CanExecute == null || this._CanExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
