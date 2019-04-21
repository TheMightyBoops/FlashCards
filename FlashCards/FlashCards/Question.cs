using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards
{
    class Question
    {
        public string BaseQuestion { get; set; }
        public string Answer { get; set; }

        public Question(string bq, string a)
        {
            BaseQuestion = bq;
            Answer = a;
        }
    }
}
