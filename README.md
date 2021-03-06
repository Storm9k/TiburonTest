Тестовое задание для вакансии “Разработчик Backend
ASP.NET”
Вы - разработчик, вливающийся в команду проекта NanoSurvey - очень простой
системы онлайн-опросов. Фронтэнд анкеты уже реализован отдельным приложением
на React. Приложение показывает страницу с текстом вопроса, вариантами ответов и
кнопкой “Далее”, для перехода к следующему вопросу анкеты. Например, так:
Нужно сделать SQL-базу и апи бэкенда для NanoSurvey. Апи должно быть
реализовано на ASP.NET Core + Docker, язык C#. Данные - в базе PostgreSQL. Запуск
приложения и базы - через docker-compose файл.
1. В базе должны присутствовать следующие сущности:
Survey - информация об анкете.
Question - вопрос анкеты.
Answer - вариант ответа на вопрос анкеты.
Interview - информация об интервью (отдельной сессии прохождения анкеты
конкретным человеком)
Result - данные ответов людей на вопросы анкеты
Подумать о том, какие поля могут быть у каждой сущности и какие связи должны быть
между ними. Какие индексы должны быть на таблицах, чтобы наша анкета работала
быстро.
2. У апи необходимо реализовать два async метода:
● Получение данных вопроса для отображения на фронте (текста вопроса и
вариантов ответа)
● Сохранение результатов ответа на вопрос по кнопке “Далее”. Принимает
выбранные радиобаттоны, возвращает айди следующего вопроса.
Подумать, какие URI должны быть у этих двух методов, каким типом http запроса
канонично к ним обратиться.
Апи и скрипт создания базы на SQL опубликовать в открытом репозитории на
github.com (или любом аналогичном хостинге кода).
Оцениваются
● Структурированность и минималистичность кода. Отсутствие ненужного
бойлерплейта (например, авторизации из стандартного шаблона VS).
● Организация схемы хранения данных в БД.
