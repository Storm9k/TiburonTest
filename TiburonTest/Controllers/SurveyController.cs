using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TiburonTest.Model;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace TiburonTest.Controllers
{
    [ApiController]
    [Route("[controller]-{survey_id}")]
    public class SurveyController : Controller
    {
        public IConfiguration Configuration { get; }
        public SurveyController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

            //Получение вопроса и ответов из БД по ID
        [HttpGet("{q_id}")]
        public async Task<IActionResult> GetQA(int q_id)
        {
            List<Answer> AnswersList = new List<Answer>();
            if (q_id != 0 && q_id > 0)
            {
                //Подключение к БД
                await using var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                await connection.OpenAsync();
                //Запрос и его обработка
                await using (var cmd = new NpgsqlCommand($"SELECT Q.\"ID\" AS Q_ID, q.\"QText\", q.\"SurveyID\", a.\"ID\" AS A_ID, a.\"AText\" FROM public.\"Questions\" as Q LEFT JOIN public.\"Answers\" as A ON q.\"ID\" = A.\"QuestionID\" WHERE q.\"ID\" = {q_id} ", connection))
                await using (var reader = await cmd.ExecuteReaderAsync())
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {

                            AnswersList.Add(new Answer
                            {
                                ID = Convert.ToInt32(reader["a_id"]),
                                AText = reader["AText"].ToString(),
                                QuestionID = Convert.ToInt32(reader["q_id"]),
                                Question = new Question
                                {
                                    SurveyID = Convert.ToInt32(reader["SurveyID"]),
                                    QText = reader["QText"].ToString(),
                                    ID = Convert.ToInt32(reader["q_id"]),
                                }
                            });
                        }
                    }
                    else return NotFound();
            }
            //Изменение кодировки для последующей сериализации
            var serialize_option = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            return Json(AnswersList, serialize_option);
        }

        [HttpPost]
        public async Task<IActionResult> SaveStep(int interview_id, int answer_id)
        {
            int quest_id = 0;

            await using var connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            await using (var cmd = new NpgsqlCommand($"INSERT INTO \"Results\"(\"InterviewID\", \"AnswerID\") VALUES (@i, @a)", connection))
            {
                cmd.Parameters.AddWithValue("i", interview_id);
                cmd.Parameters.AddWithValue("a", answer_id);
                await cmd.ExecuteNonQueryAsync();
            }

            await using (var cmd = new NpgsqlCommand($"SELECT \"QuestionID\" FROM \"Answers\" WHERE \"ID\" = {answer_id}", connection))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    quest_id = Convert.ToInt32(reader["QuestionID"]);
                }

            return RedirectToAction("GetQA", new { q_id = ++quest_id});
        }



    }
}
