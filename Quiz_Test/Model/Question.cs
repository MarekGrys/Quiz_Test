﻿using Quiz_Test.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Test.Model
{
    class Question
    {
        private static long value = long.Parse(SingletonQuiz.Instance.SingletonValue);
        public long Value
        {
            get => value;
        }
        public long QuestionID { get; set; }
        public long QuizID { get; set; }
        public string QuestionName { get; set; }
        public long QuestionOrder { get; set; }

        public static SQLiteConnection conn = new SQLiteConnection(@"Data Source=G:\Program Files (x86)\DBTEST\Quiz.db; Version=3");

        public static List<Question> ReadData(SQLiteConnection conn)
        {
            
            List<Question> questiones = new List<Question>();
            conn.Open();
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            //command.Parameters.AddWithValue("@Variable", Question.value);
            command.CommandText = $"SELECT * FROM Question WHERE Quiz_ID = {value}";
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
