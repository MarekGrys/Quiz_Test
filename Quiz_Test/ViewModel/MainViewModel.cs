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

namespace Quiz_Test.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {

        #region Zmienne
        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\jgrys\Desktop\QUIZDB\Quiz.db; Version=3");
        public event PropertyChangedEventHandler PropertyChanged;
        private static bool isRun = false;
        List<Quiz> quizzes = Quiz.ReadData(conn);
        List<Question> questions = Question.ReadData(conn);
        List<Answer> answers = Answer.ReadData(conn);
        SQLiteDataReader reader;
        SQLiteCommand command;
        int tmp_index = 0;
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
        /* static void X()
         {
             Quiz.ReadData();
         }*/

        private ICommand wlacz;
        public ICommand Wlacz
        {
            get
            {
                if (wlacz == null)
                    wlacz = new RelayCommand(

                    (o) =>
                    {
                        //conn.Open();
                        //Quiz.OneElementTest(0);
                        //Console.WriteLine(questions[3].QuestionName);
                        //Console.WriteLine(answers[0].AnswerText);
                        try
                        {
                            /*if (questions[0].QuestionID == answers[0].AnswerID)
                            {
                                Console.WriteLine("Dziala");
                            }
                            else
                            {
                                Console.WriteLine("Nie dziala");
                            }*/
                            //iter_odp += 4;
                            //iter_odp_pomoc += 4;
                            //isRun = !isRun;
                            //conn.Close();
                            //answer = TakeAnswer2();
                            iter_odp_pomoc += 4;
                            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer1)));
                            Answer1 = TakeAnswer(1);
                            //TakeAnswer(4);
                            //Tmp_answer();
                            //Console.WriteLine(count_right_answers);
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
        public string Name
        {
            get => questions[tmp_index].QuestionName;
        }
        public long QuizID
        {
            get => quizzes[tmp_index].QuizID;
        }
        public string FirstAnswer
        {
            get => answers[tmp_index].AnswerText;
            
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
            //iter_odp += 4;
            //iter_odp_pomoc += 4;
            Console.WriteLine(iter_odp);
            Console.WriteLine(iter_odp_pomoc);
            conn.Close();
            return answer1;
        }
        public void Tmp_answer()
        {
            /*Console.WriteLine(correct_count);
            Console.WriteLine(correct_count+2);*/
            Console.WriteLine(answers[correct_count+1].AnswerIsCorrect);
        }
        private string answer_tmp = null;
        #region AnswerStrings
        public MainViewModel()
        {
            answer1 = TakeAnswer(1);
        }
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
        public  string answer2
        {
            get => TakeAnswer(2);
            //set => TakeAnswer(2);
            
        }
        public string answer3
        {
            get => TakeAnswer(3);
            //set => TakeAnswer(3);
        }
        public string answer4
        {
            get => TakeAnswer(4);
            //set => TakeAnswer(4);
        }
        #endregion
        /*public void Next()
        {
            iter_odp += 4;
            iter_odp_pomoc += 4;
        }*/
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
    }
}
