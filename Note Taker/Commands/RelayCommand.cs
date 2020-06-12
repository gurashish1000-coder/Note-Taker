using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Note_Taker.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action DoWork;
        public RelayCommand(Action Work)
        {
            DoWork = Work;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DoWork();
        }
    }
}
