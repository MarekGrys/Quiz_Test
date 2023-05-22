using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Test.Model
{
    class Question
    {
        public long QuestionID { get; set; }
        public long QuizID { get; set; }
        public string QuestionName { get; set; }
        public long QuestionOrder { get; set; }

        public static SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\jgrys\Desktop\QUIZDB\Quiz.db; Version=3");

        public static List<Question> ReadData(SQLiteConnection conn)
        {
            
            List<Question> questiones = new List<Question>();
            conn.Open();
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Question";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Question question = new Question();
                question.QuestionID = (long)reader["Question_ID"];
                question.QuizID = (long)reader["Quiz_ID"];
                question.QuestionName = (string)reader["Question_Name"];
                question.QuestionOrder = (long)reader["Question_Order"];
                questiones.Add(question);
            }
            conn.Close();
            return questiones;
        }
    }
}
