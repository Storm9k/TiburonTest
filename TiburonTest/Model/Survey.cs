using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiburonTest.Model
{
    //Модель класса Survey - информация об анкете
    public class Survey
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
    }
}
