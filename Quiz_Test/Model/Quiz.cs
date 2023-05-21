using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Quiz_Test.Model
{
    class Quiz
    {

        public long QuizID { get; set; }
        public string QuizName { get; set; }

        public static SQLiteConnection conn = new SQLiteConnection(@"Data Source=G:\Program Files (x86)\DBTEST\Quiz.db; Version=3");

        public static List<Quiz> ReadData(SQLiteConnection conn)
        {
            
            List<Quiz> quizzes = new List<Quiz>();
            conn.Open();
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Quiz";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Quiz quiz = new Quiz();
                quiz.QuizID = (long)reader["Quiz_ID"];
                quiz.QuizName = (string)reader["Quiz_Name"];
                quizzes.Add(quiz);
            }
           conn.Close();
            return quizzes;
        }
        public static void ReadData()
        {
            conn.Open();
            List<Quiz> quizzes = Quiz.ReadData(conn);
            try
            {
                foreach (Quiz quiz in quizzes)
                {
                    Console.WriteLine($"{quiz.QuizID}, {quiz.QuizName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
        }
        public static void OneElementTest(int quizelement)
        {
            conn.Open();
            List<Quiz> quizzes = Quiz.ReadData(conn);
            Console.WriteLine(quizzes[quizelement].QuizName);
            conn.Close();
        }
    }
}
