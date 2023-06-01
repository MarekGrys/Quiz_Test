using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Test.ViewModel
{
    internal class SingletonAnswers
    {
        private static SingletonAnswers _instance;
        private string _singletonValue;

        public static SingletonAnswers Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SingletonAnswers();
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
