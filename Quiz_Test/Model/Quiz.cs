﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Quiz_Test.ViewModel;

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
            conn.Close();
            conn.Open();
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM quiz";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Quiz quiz = new Quiz();
                quiz.QuizID = (long)reader["quiz_id"];
                quiz.QuizName = (string)reader["quiz_name"];
                quizzes.Add(quiz);
            }
           conn.Close();
            return quizzes;
        }
    }
}
