using Quiz_Test.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Test.Model
{
    class Answer
    {
        private static long value = long.Parse(Singleton.Instance.SingletonValueQuizID);
        public long Value
        {
            get => value;
        }
        public long AnswerID { get; set; }
        public long QuestionID { get; set; }
        public string AnswerText { get; set; }
        public long AnswerField { get; set; }
        public long AnswerIsCorrect { get; set; }

        public static SQLiteConnection conn = new SQLiteConnection(@"Data Source=G:\Program Files (x86)\DBTEST\Quiz.db; Version=3");

        public static List<Answer> ReadData(SQLiteConnection conn)
        {
            List<Answer> answers = new List<Answer>();
            conn.Open();
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = $"SELECT Answer_ID, Question.Question_ID, Answer_Text, Answer_Field, Answer_IsCorrect, Quiz.Quiz_ID FROM Answer" +
                $" INNER JOIN Question ON Question.Question_ID = Answer.Question_ID" +
                $" INNER JOIN Quiz ON Quiz.Quiz_ID = Question.Quiz_ID WHERE Quiz.Quiz_ID = {value}";
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                Answer answer = new Answer();
                answer.AnswerID = (long)reader["answer_id"];
                answer.QuestionID = (long)reader["question_id"];
                answer.AnswerText = (string)reader["answer_text"];
                answer.AnswerField = (long)reader["answer_field"];
                answer.AnswerIsCorrect = (long)reader["answer_iscorrect"];
                answers.Add(answer);
            }
            conn.Close();
            return answers;
        } 
    }
    
}
