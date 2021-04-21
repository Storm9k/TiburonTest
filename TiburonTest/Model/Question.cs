using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiburonTest.Model
{
    //Модель класса Question - вопрос анкеты
    public class Question
    {
        public int ID { get; set; }
        public string QText { get; set; }
        public int SurveyID { get; set; }
        public Survey Survey { get; set; }
        public List<Answer> Answers { get; set; }
    }

}
