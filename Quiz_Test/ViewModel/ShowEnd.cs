using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Quiz_Test.Model;
using Quiz_Test.ViewModel;
using Quiz_Test.Views;
namespace Quiz_Test.ViewModel
{
    internal class ShowEnd
    {
        private string value =  "Liczba poprawnych odpowiedzi: " + Singleton.Instance.SingletonValueRightAnswers;
        static DateTime starttime = DateTime.Parse(Singleton.Instance.SingletonValueStartTimer);
        static DateTime endtime = DateTime.Parse(Singleton.Instance.SingletonValueEndTimer);
        string completiontime = "Czas wykonania quizu: "+endtime.Subtract(starttime).ToString();
        public string Completiontime
        {
            get => completiontime;
        }
        
        public string Value
        {
            get => value;
        }
        public ICommand OpenNewWindowCommand { get; }
        public ICommand CloseNewWindowCommand { get; }
        public ShowEnd()
        {
            OpenNewWindowCommand = new RelayCommandViews(OpenNewWindow);
            CloseNewWindowCommand = new RelayCommandViews(CloseNewWindow);
        }
        private void OpenNewWindow()
        {           
            
            QuizView newWindow = new QuizView();
            newWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = newWindow;
        }
        private void CloseNewWindow()
        {
            Application.Current.MainWindow.Close();
        }
        
    }
}
