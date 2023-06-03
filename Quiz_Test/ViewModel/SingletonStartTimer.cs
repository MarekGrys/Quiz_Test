using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Test.ViewModel
{
    internal class SingletonStartTimer
    {
        private static SingletonStartTimer _instance;
        private string _singletonValuestart;
        private string _singletonValuestop;

        public static SingletonStartTimer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SingletonStartTimer();
                return _instance;
            }
        }

        public string SingletonValueStart
        {
            get { return _singletonValuestart; }
            set
            {
                _singletonValuestart = value;
                OnPropertyChanged(nameof(SingletonValueStart));
            }
        }

        public string SingletonValueEnd
        {
            get { return _singletonValuestop; }
            set
            {
                _singletonValuestop = value; 
                OnPropertyChanged(nameof(SingletonValueEnd));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
