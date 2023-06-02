using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Quiz_Test.Model;
using System.Timers;
using System.Data.SQLite;
using System.Windows;
using Prism.Mvvm;
using Prism.Commands;
using Quiz_Test.Views;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Controls;

namespace Quiz_Test.ViewModel
{
    internal class ViewModelQuiz : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=G:\Program Files (x86)\DBTEST\Quiz.db; Version=3");
        public List<Quiz> quizzes = Quiz.ReadData(conn);
        private static bool isRun = false;
        private int tmp_iter = 0;
        private int iter_id_quizu = 0;
        private long id_quizu = 0;
        long Id_quizu
        {
            get => id_quizu;
            set
            {
                id_quizu = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(id_quizu)));
            }
        }
        public ViewModelQuiz() 
        {
            OpenNewWindowCommand = new RelayCommandViews(OpenNewWindow);
        }  
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        int Tmp_iter
        {
            get => tmp_iter;
            set
            {
                tmp_iter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(tmp_iter)));
            }
        }
        int Iter_id_quizu
        {
            get => iter_id_quizu;
            set
            {
                iter_id_quizu = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(iter_id_quizu)));
            }
        }
        public ObservableCollection<string> YourCollection { get; } = new ObservableCollection<string>();

        private void LoadQuizFromDatabase()
        {
            foreach (Quiz quiz in quizzes)
            {
                YourCollection.Add(quizzes[tmp_iter].QuizName);
                tmp_iter++;
            }
        }
        private ICommand load;
        public ICommand Load
        {
            get
            {
                if (load == null)
                    load = new RelayCommand(

                    (o) =>
                    {
                        LoadQuizFromDatabase();
                        //isRun = !isRun;
                    }
                    ,
                    (o) => !isRun
                    );
                return load;
            }
        }
        private string _selectedItem;

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private ICommand wypisz;
        public ICommand Wypisz
        {
            get
            {
                if (wypisz == null)
                    wypisz = new RelayCommand(

                    (o) =>
                    {
                        Proba();
                        Znajdz_ID();
                        SingletonQuiz.Instance.SingletonValue = id_quizu.ToString();
                    }
                    ,
                    (o) => !isRun
                    );
                return wypisz;

            }

        }
        public void Proba()
        {
            Console.WriteLine(SelectedItem);
        }
        public void Znajdz_ID()
        {
            foreach (Quiz quiz in quizzes)
            {
                
                if (SelectedItem == quizzes[iter_id_quizu].QuizName)
                {
                    Id_quizu = quizzes[iter_id_quizu].QuizID; 
                    break;
                }
                iter_id_quizu++;
            }
            Console.WriteLine(Id_quizu);
        }
        public ICommand OpenNewWindowCommand { get; }
        private void OpenNewWindow()
        {
            Proba();
            Znajdz_ID();
            SingletonQuiz.Instance.SingletonValue = Id_quizu.ToString();
            SingletonStartTimer.Instance.SingletonValue = DateTime.Now.ToString();
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = newWindow;
        }

    }
}
