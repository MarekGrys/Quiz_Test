﻿using System;
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

namespace Quiz_Test.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {

        #region Zmienne
        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=G:\Program Files (x86)\DBTEST\Quiz.db; Version=3");
        public event PropertyChangedEventHandler PropertyChanged;
        private static bool isRun = false;
        List<Quiz> quizzes = Quiz.ReadData(conn);
        List<Question> questions = Question.ReadData(conn);
        List<Answer> answers = Answer.ReadData(conn);
        SQLiteDataReader reader;
        SQLiteCommand command;
        private int iter_question = 0;
        int Iter_question
        {
            get => iter_question;
            set
            {
                iter_question = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(iter_question)));
            }
        }
        int iter_odp = 0;
        private int iter_odp_pomoc = 4;
        int Iter_odp_pomoc 
        {
            get => iter_odp_pomoc;
            set
            {
                iter_odp_pomoc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(iter_odp_pomoc)));
            }
        }
        
        int count_right_answers = 0;
        long correct_check = 1;
        int correct_count = 0;
        #endregion
 
        private ICommand wlacz;
        public ICommand Wlacz
        {
            get
            {
                if (wlacz == null)
                    wlacz = new RelayCommand(

                    (o) =>
                    {
                        try
                        {
                            iter_odp_pomoc += 4;
                            correct_count += 4;
                            iter_question++;
                            Next();
                            Console.WriteLine(count_right_answers);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                    }
                    ,
                    (o) => !isRun
                    );
                return wlacz;
            }
        }
        public string TakeAnswer(int field)
        {
            List<Answer> answers = Answer.ReadData(conn);
            string answer1 = null;
            conn.Open();
            for (iter_odp = 0; iter_odp <Iter_odp_pomoc; iter_odp++)
            {
                if (answers[iter_odp].AnswerField == field)
                {
                    answer1 = answers[iter_odp].AnswerText;
                }
            }
            conn.Close();
            return answer1;
        }
        public ICommand OpenNewWindowCommand { get; }
        public MainViewModel()
        {
            answer1 = TakeAnswer(1);
            answer2 = TakeAnswer(2);
            answer3 = TakeAnswer(3);
            answer4 = TakeAnswer(4);
            question_view = Take_Question();
            OpenNewWindowCommand = new RelayCommandViews(OpenNewWindow);
            sum = count_right_answers;

        }
        private void OpenNewWindow()
        {
            FirstView newWindow = new FirstView();
            newWindow.Show();
            Application.Current.MainWindow.Close();
        }
        #region AnswerStrings

        private string answer1;
        public string Answer1
        {
            get => answer1;
            set
            {
                answer1 = TakeAnswer(1);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer1)));
            }
            
        }
        private string answer2;
        public  string Answer2
        {
            get => answer2;
            set 
            {
                answer2 = TakeAnswer(2);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer2)));
            }
            
        }
        private string answer3;
        public string Answer3
        {
            get => answer3;
            set 
            {
                answer3 = TakeAnswer(3);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer3)));
            }
        }
        private string answer4;
        public string Answer4
        {
            get => answer4;
            set
            {
                answer4 = TakeAnswer(4);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer4)));
            }
        }
        public void Next()
        {
            Answer1 = TakeAnswer(1);
            Answer2 = TakeAnswer(2);
            Answer3 = TakeAnswer(3);
            Answer4 = TakeAnswer(4);
            QuestionView = Take_Question();
            Sum = count_right_answers;
        }
        #endregion
        public void Button1_Click()
        {
            Console.WriteLine("Dziala");
        }
        private ICommand guzik1;
        public ICommand Guzik1
        {
            get
            {
                if (guzik1 == null)
                    guzik1 = new RelayCommand(

                    (o) =>
                    {
                        Button1_Click();
                        isRun = !isRun;
                    }
                    ,
                    (o) => !isRun
                    );
                return guzik1;
            }
        }

        public void ButtonNext_Click()
        {
            Console.WriteLine("Next Question");
        }
        private ICommand buttonNext;
        public ICommand ButtonNext
        {
            get
            {
                if (buttonNext == null)
                    buttonNext = new RelayCommand(

                        (o) =>
                        {
                            ButtonNext_Click();
                            isRun = !isRun;
                        }
                        ,
                        (o) => isRun
                ); 
               return buttonNext;
            }
        }
        public string Take_Question()
        {
            List<Question> questions = Question.ReadData(conn);
            string question1 = questions[iter_question].QuestionName;
            return question1;

        }
        private string question_view;
        public string QuestionView
        {
            get => question_view;
            set
            { 
                question_view = Take_Question();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionView)));

            } 
        }
        #region CheckIfCorrect
        public void CheckIfCorrect1()
        {
            Console.WriteLine(answers[correct_count].AnswerIsCorrect);
            if (answers[correct_count].AnswerIsCorrect == correct_check)
            {
                count_right_answers++;
                Console.WriteLine("Dobrze");
            }
            else
            {
                Console.WriteLine("Zle");
            }
        }
        private ICommand correct1;
        public ICommand Correct1
        {
            get
            {
                if (correct1 == null)
                    correct1 = new RelayCommand(

                    (o) =>
                    {
                        CheckIfCorrect1();
                        //isRun = !isRun;
                    }
                    ,
                    (o) => !isRun
                    );
                return correct1;
            }
        }
        public void CheckIfCorrect2()
        {
            Console.WriteLine(answers[correct_count + 1].AnswerIsCorrect);
            if (answers[correct_count+1].AnswerIsCorrect == correct_check)
            {
                count_right_answers++;
                Console.WriteLine("Dobrze");
            }
            else 
            { 
                Console.WriteLine("Zle"); 
            }
        }
        private ICommand correct2;
        public ICommand Correct2
        {
            get
            {
                if (correct2 == null)
                    correct2 = new RelayCommand(

                    (o) =>
                    {
                        CheckIfCorrect2();
                        //isRun = !isRun;
                    }
                    ,
                    (o) => !isRun
                    );
                return correct2;
            }
        }
        public void CheckIfCorrect3()
        {
            Console.WriteLine(answers[correct_count + 2].AnswerIsCorrect);
            if (answers[correct_count+2].AnswerIsCorrect == correct_check)
            {
                count_right_answers++;
                Console.WriteLine("Dobrze");
            }
            else 
            { 
                Console.WriteLine("Zle"); 
            }
        }
        private ICommand correct3;
        public ICommand Correct3
        {
            get
            {
                if (correct3 == null)
                    correct3 = new RelayCommand(

                    (o) =>
                    {
                        CheckIfCorrect3();
                        //isRun = !isRun;
                    }
                    ,
                    (o) => !isRun
                    );
                return correct3;
            }
        }
        public void CheckIfCorrect4()
        {
            Console.WriteLine(answers[correct_count + 3].AnswerIsCorrect);
            if (answers[correct_count + 3].AnswerIsCorrect == correct_check)
            {
                count_right_answers++;
                Console.WriteLine("Dobrze");
            }
            else 
            { 
                Console.WriteLine("Zle"); 
            }
        }
        private ICommand correct4;
        public ICommand Correct4
        {
            get
            {
                if (correct4 == null)
                    correct4 = new RelayCommand(

                    (o) =>
                    {
                        CheckIfCorrect4();
                        //isRun = !isRun;
                    }
                    ,
                    (o) => !isRun
                    );
                return correct4;
            }
        }
        #endregion
        #region Sum_Points

        private int sum;
        public int Sum
        {
            get => sum;
            set
            {
                sum = count_right_answers;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sum)));
            }

        }

        #endregion

    }
}
