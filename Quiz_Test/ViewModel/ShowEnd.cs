using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Quiz_Test.ViewModel;
using Quiz_Test.Views;
namespace Quiz_Test.ViewModel
{
    internal class ShowEnd
    {
        private string value =  "Liczba poprawnych odpowiedzi: "+ Singleton.Instance.SingletonValue;
        public string Value
        {
            get => value;
        }

    }
}
