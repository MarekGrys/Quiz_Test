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
        public long AnswerID { get; set; }
        public long QuestionID { get; set; }
        public string AnswerText { get; set; }
        public long AnswerField { get; set; }
        public long AnswerIsCorrect { get; set; }

        public static SQLiteConnection conn = new SQLiteConnection(@"Data Source=G:\Program Files (x86)\Quiz.db");

        public static List<Answer> ReadData(SQLiteConnection conn)
        {
            List<Answer> answers = new List<Answer>();
            conn.Open();
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Answer";
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                Answer answer = new Answer();
                answer.AnswerID = (long)reader["Answer_ID"];
                answer.QuestionID = (long)reader["Question_ID"];
                answer.AnswerText = (string)reader["Answer_Text"];
                answer.AnswerField = (long)reader["Answer_Field"];
                answer.AnswerIsCorrect = (long)reader["Answer_IsCorrect"];
                answers.Add(answer);
            }
            conn.Close();
            return answers;
        }
       /* public string TakeAnswer1()
        {
            List<Answer>answers = Answer.ReadData(conn);
            string answer1 = null;
            conn.Open();
            for (int i = 0; i < 4; i++)
            {
                if (answers[i].AnswerField == 1)
                {
                    answer1 = answers[i].AnswerText;
                }
            }
            conn.Close();
            return answer1;
        }*/
       /* public string TakeAnswer2()
        {
            List<Answer>answers=Answer.ReadData(conn);
            string answer2 = null;
            conn.Open();
            for(int i = 0;i < 4;i++)
            {
                if (answers[i].AnswerField == 2)
                {
                    answer2 = answers[i].AnswerText;
                }
            }
            conn.Close();
            return answer2;
        }*/
        
    }
    
}
