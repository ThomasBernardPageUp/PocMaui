using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PocMaui.Wrappers
{
    public class SutomWordEntityWrapper : INotifyPropertyChanged
    {

        private char _value;
        public char Value
        {
            get => _value;
            set
            {
                _value = value;
                NotifyPropertyChanged(nameof(Value));
            }
        }

        private int _status;
        public int Status // 0 => Grey / 1 => Yellow / 2 => Red
        {
            get => _status;
            set
            {
                _status = value;
                NotifyPropertyChanged(nameof(Status));
            }
        }



        public SutomWordEntityWrapper()
        {
        }

        public SutomWordEntityWrapper(char value)
        {
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
