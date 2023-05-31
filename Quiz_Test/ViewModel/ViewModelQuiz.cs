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
        public ObservableCollection<string> YourCollection { get; } = new ObservableCollection<string>();

        private void LoadQuizFromDatabase()
        {
            foreach (Quiz quiz in quizzes)
            {
                YourCollection.Add(quizzes[tmp_iter].QuizName.ToString());
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
        /*private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (sender is ComboBox comboBox)
            {

                var selectedItem = comboBox.SelectedItem;
                Console.WriteLine(selectedItem?.ToString());
            }
        }
        *//*void PrintText(object sender, SelectionChangedEventArgs args)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
        }*//*
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {           
            if (sender is ListBox ListBox)
            {            
                var clickedItem = ListBox.SelectedItem;
                Console.WriteLine(clickedItem.ToString());
            }
        }*/
        private Quiz _selectedItem;
        public Quiz SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                Console.WriteLine($"Wybrany element: {SelectedItem.ToString()}");
            }
        }

    }
}
