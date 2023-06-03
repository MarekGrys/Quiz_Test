using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Test.ViewModel
{
    internal class Singleton
    {
        private static Singleton _instance;
        private string _singletonValuestart;
        private string _singletonValuestop;
        private string _singletonValueQuizID;
        private string _singletonValueRightAnswers;

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Singleton();
                return _instance;
            }
        }

        public string SingletonValueStartTimer
        {
            get { return _singletonValuestart; }
            set
            {
                _singletonValuestart = value;
                OnPropertyChanged(nameof(SingletonValueStartTimer));
            }
        }

        public string SingletonValueEndTimer
        {
            get { return _singletonValuestop; }
            set
            {
                _singletonValuestop = value; 
                OnPropertyChanged(nameof(SingletonValueEndTimer));
            }
        }

        public string SingletonValueQuizID
        {
            get { return _singletonValueQuizID; }
            set
            {
                _singletonValueQuizID = value;
                OnPropertyChanged(nameof(SingletonValueQuizID));
            }
        }

        public string SingletonValueRightAnswers
        {
            get { return _singletonValueRightAnswers; }
            set
            {
                _singletonValueRightAnswers = value;
                OnPropertyChanged(nameof(SingletonValueRightAnswers));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
