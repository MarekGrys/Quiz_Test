using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Test.ViewModel
{
    internal class SingletonStopTimer
    {
        private static SingletonStopTimer _instance;
        private string _singletonValue;

        public static SingletonStopTimer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SingletonStopTimer();
                return _instance;
            }
        }

        public string SingletonValue
        {
            get { return _singletonValue; }
            set
            {
                _singletonValue = value;
                OnPropertyChanged(nameof(SingletonValue));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
