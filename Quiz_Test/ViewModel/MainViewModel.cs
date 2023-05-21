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

        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=G:\Program Files (x86)\Quiz.db");
        public event PropertyChangedEventHandler PropertyChanged;
        private static bool isRun = false;
        List<Quiz> quizzes = Quiz.ReadData(conn);
        List<Question> questions = Question.ReadData(conn);
        List<Answer> answers = Answer.ReadData(conn);
        SQLiteDataReader reader;
        SQLiteCommand command;
        int tmp_index = 0;
        int iter_odp = 0;
        int iter_odp_pomoc =4;
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
                        if (questions[0].QuestionID == answers[0].AnswerID)
                        {
                            Console.WriteLine("Dziala");
                        }
                        else
                        {
                            Console.WriteLine("Nie dziala");
                        }
                        //iter_odp += 4;
                        //iter_odp_pomoc += 4;
                        //isRun = !isRun;
                        //conn.Close();
                        //answer = TakeAnswer2();
                        iter_odp_pomoc += 4;
                        TakeAnswer(4);
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
        public string FirstAnswer
        {
            get => answers[tmp_index].AnswerText;
            
        }
        public string TakeAnswer(int field)
        {
            List<Answer> answers = Answer.ReadData(conn);
            string answer1 = null;
            conn.Open();
            for (iter_odp = 0; iter_odp <iter_odp_pomoc; iter_odp++)
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
        private string answer_tmp = null;
        public string answer1
        {
            get => TakeAnswer(1);
            set => TakeAnswer(1);
        }
        public  string answer2
        {
            get => TakeAnswer(2);
            set => TakeAnswer(2);
            
        }
        public string answer3
        {
            get => TakeAnswer(3);
            set => TakeAnswer(3);
        }
        public string answer4
        {
            get => TakeAnswer(4);
            set => TakeAnswer(4);
        }
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
                    }
                    ,
                    (o) => !isRun
                    );
                return guzik1;
            }
        }
    }
}
