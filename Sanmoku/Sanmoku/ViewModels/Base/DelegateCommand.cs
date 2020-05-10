using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sanmoku.ViewModels.Base
{
    public class DelegateCommand :ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<object> ExecuteHandler { get; set; }
        public Func<object, bool> CanExecuteHandler { get; set; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteHandler == null ? true : CanExecuteHandler(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteHandler?.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, null);
        }
    }
}

