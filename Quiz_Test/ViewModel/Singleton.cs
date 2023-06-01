﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Test.ViewModel 
{
    internal class Singleton : INotifyPropertyChanged
    {
        private static Singleton _instance;
        private string _singletonValue;

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Singleton();
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
