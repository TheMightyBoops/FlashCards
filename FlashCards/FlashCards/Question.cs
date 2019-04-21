using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FlashCards
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string BaseQuestion { get; set; }
        public string Answer { get; set; }


        public Question()
        {

        }

        public Question(string bq, string a)
        {
            BaseQuestion = bq;
            Answer = a;
        }
    }
}
