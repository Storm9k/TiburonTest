using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiburonTest.Model
{
    //Модель класса Answer - вариант ответа на вопрос анкеты
    public class Answer
    {
        public int ID { get; set; }
        public string AText { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }

    }
}
