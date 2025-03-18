using Microsoft.AspNetCore.Mvc;
using Obshta_Kultura.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using Obshta_Kultura.Services;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Obshta_Kultura.Controllers
{
    public class QuizController : Controller
    {
        private readonly EmailService _emailService;
        private readonly EmailSettings _emailSettings;

        private static List<Question> _questions = new List<Question>
        {
            // История
            new Question { Id = 1, Text = "Кога е основана България?", Answers = new List<string> { "681", "681", "681", "681" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "Първата българска държава е основана през 681 година." },
            new Question { Id = 2, Text = "Кой е първият български цар?", Answers = new List<string> { "Симеон I", "Борис I", "Асен I", "Петър I" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "Симеон I е първият български цар, управлявал от 893 до 927 година." },
            new Question { Id = 3, Text = "Кога е Освобождението на България?", Answers = new List<string> { "1878", "1878", "1878", "1878" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "България е освободена от османско робство на 3 март 1878 година." },
            new Question { Id = 4, Text = "Кой е първият български патриарх?", Answers = new List<string> { "Дамян", "Евтимий", "Климент", "Наум" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "Дамян е първият български патриарх, избран през 927 година." },
            new Question { Id = 5, Text = "Кога е създадена Първата българска конституция?", Answers = new List<string> { "1879", "1879", "1879", "1879" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "Търновската конституция е приета на 16 април 1879 година." },
            new Question { Id = 6, Text = "Кой е първият български княз след Освобождението?", Answers = new List<string> { "Александър I Батенберг", "Фердинанд I", "Борис III", "Симеон II" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "Александър I Батенберг е първият български княз (1879-1886)." },
            new Question { Id = 7, Text = "Кога е създадена Българската академия на науките?", Answers = new List<string> { "1869", "1869", "1869", "1869" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "Българското книжовно дружество (днес БАН) е създадено през 1869 година." },
            new Question { Id = 8, Text = "Кой е първият български екзарх?", Answers = new List<string> { "Антим I", "Стефан I", "Кирил", "Максим" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "Антим I е първият български екзарх (1872-1877)." },
            new Question { Id = 9, Text = "Кога е създадена Българската армия?", Answers = new List<string> { "1878", "1878", "1878", "1878" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "Българската армия е създадена през 1878 година." },
            new Question { Id = 10, Text = "Кой е първият български премиер?", Answers = new List<string> { "Тодор Бурмов", "Стефан Стамболов", "Драган Цанков", "Петко Каравелов" }, CorrectAnswerIndex = 0, Category = "История", Explanation = "Тодор Бурмов е първият български министър-председател (1879)." },

            // География
            new Question { Id = 11, Text = "Коя е най-високата планина в България?", Answers = new List<string> { "Рила", "Пирин", "Стара планина", "Родопи" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "Рила е най-високата планина в България с връх Мусала (2925 м)." },
            new Question { Id = 12, Text = "Коя е най-дългата река в България?", Answers = new List<string> { "Дунав", "Марица", "Струма", "Искър" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "Река Дунав е най-дългата река, преминаваща през България." },
            new Question { Id = 13, Text = "Кой е най-големият български град по население?", Answers = new List<string> { "София", "Пловдив", "Варна", "Бургас" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "София е най-големият град в България с население над 1.2 милиона души." },
            new Question { Id = 14, Text = "Кое е най-голямото езеро в България?", Answers = new List<string> { "Бургаско езеро", "Варненско езеро", "Сребърна", "Панчаревско езеро" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "Бургаското езеро е най-голямото естествено езеро в България." },
            new Question { Id = 15, Text = "Кой е най-високият връх в България?", Answers = new List<string> { "Мусала", "Вихрен", "Ботев", "Черни връх" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "Връх Мусала (2925 м) е най-високият връх в България." },
            new Question { Id = 16, Text = "Кой е най-големият български остров в Черно море?", Answers = new List<string> { "Свети Иван", "Свети Кирик", "Свети Петър", "Змийски остров" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "Остров Свети Иван е най-големият български остров в Черно море." },
            new Question { Id = 17, Text = "Кой е най-старият природен парк в България?", Answers = new List<string> { "Витоша", "Рила", "Пирин", "Странджа" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "Природен парк Витоша е първият природен парк в България, обявен през 1934 г." },
            new Question { Id = 18, Text = "Кой е най-големият полуостров в България?", Answers = new List<string> { "Калиакра", "Емине", "Маслен нос", "Галата" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "Нос Калиакра е най-големият полуостров по българското Черноморие." },
            new Question { Id = 19, Text = "Коя е най-дългата пещера в България?", Answers = new List<string> { "Духлата", "Съева дупка", "Магура", "Деветашка пещера" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "Пещера Духлата е най-дългата пещера в България с дължина над 18 км." },
            new Question { Id = 20, Text = "Кой е най-големият залив по българското Черноморие?", Answers = new List<string> { "Бургаски залив", "Варненски залив", "Несебърски залив", "Созополски залив" }, CorrectAnswerIndex = 0, Category = "География", Explanation = "Бургаският залив е най-големият залив по българското Черноморие." },

            // Литература
            new Question { Id = 21, Text = "Кой е авторът на \"Под игото\"?", Answers = new List<string> { "Иван Вазов", "Христо Ботев", "Пейо Яворов", "Димчо Дебелянов" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Иван Вазов е авторът на романа \"Под игото\"." },
            new Question { Id = 22, Text = "Кой е авторът на \"Тютюн\"?", Answers = new List<string> { "Димитър Димов", "Димитър Талев", "Емилиян Станев", "Антон Дончев" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Димитър Димов е авторът на романа \"Тютюн\"." },
            new Question { Id = 23, Text = "Кой е авторът на \"Железният светилник\"?", Answers = new List<string> { "Димитър Талев", "Димитър Димов", "Емилиян Станев", "Антон Дончев" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Димитър Талев е авторът на романа \"Железният светилник\"." },
            new Question { Id = 24, Text = "Кой е авторът на \"Тихият дунав\"?", Answers = new List<string> { "Антон Дончев", "Димитър Талев", "Емилиян Станев", "Димитър Димов" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Антон Дончев е авторът на романа \"Тихият дунав\"." },
            new Question { Id = 25, Text = "Кой е авторът на \"Време разделно\"?", Answers = new List<string> { "Антон Дончев", "Димитър Талев", "Емилиян Станев", "Димитър Димов" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Антон Дончев е авторът на романа \"Време разделно\"." },
            new Question { Id = 26, Text = "Кой е авторът на \"Сиромашко лято\"?", Answers = new List<string> { "Емилиян Станев", "Димитър Талев", "Антон Дончев", "Димитър Димов" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Емилиян Станев е авторът на романа \"Сиромашко лято\"." },
            new Question { Id = 27, Text = "Кой е авторът на \"Времеубежище\"?", Answers = new List<string> { "Емилиян Станев", "Димитър Талев", "Антон Дончев", "Димитър Димов" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Емилиян Станев е авторът на романа \"Времеубежище\"." },
            new Question { Id = 28, Text = "Кой е авторът на \"Крадецът на праскови\"?", Answers = new List<string> { "Емилиян Станев", "Димитър Талев", "Антон Дончев", "Димитър Димов" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Емилиян Станев е авторът на романа \"Крадецът на праскови\"." },
            new Question { Id = 29, Text = "Кой е авторът на \"Железният светилник\"?", Answers = new List<string> { "Димитър Талев", "Димитър Димов", "Емилиян Станев", "Антон Дончев" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Димитър Талев е авторът на романа \"Железният светилник\"." },
            new Question { Id = 30, Text = "Кой е авторът на \"Тихият дунав\"?", Answers = new List<string> { "Антон Дончев", "Димитър Талев", "Емилиян Станев", "Димитър Димов" }, CorrectAnswerIndex = 0, Category = "Литература", Explanation = "Антон Дончев е авторът на романа \"Тихият дунав\"." },

            // Наука
            new Question { Id = 31, Text = "Кой е открил периодичната система на елементите?", Answers = new List<string> { "Дмитрий Менделеев", "Антоан Лавоазие", "Джон Далтон", "Роберт Бойл" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Дмитрий Менделеев е открил периодичната система на елементите." },
            new Question { Id = 32, Text = "Кой е открил теорията за относителността?", Answers = new List<string> { "Алберт Айнщайн", "Исак Нютон", "Никола Тесла", "Макс Планк" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Алберт Айнщайн е открил теорията за относителността." },
            new Question { Id = 33, Text = "Кой е открил закона за гравитацията?", Answers = new List<string> { "Исак Нютон", "Алберт Айнщайн", "Галилео Галилей", "Йоханес Кеплер" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Исак Нютон е открил закона за гравитацията." },
            new Question { Id = 34, Text = "Кой е открил закона за запазване на енергията?", Answers = new List<string> { "Юлиус Роберт фон Майер", "Херман фон Хелмхолц", "Джеймс Прескот Джаул", "Съди Карно" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Юлиус Роберт фон Майер е открил закона за запазване на енергията." },
            new Question { Id = 35, Text = "Кой е открил закона за запазване на масата?", Answers = new List<string> { "Антоан Лавоазие", "Джон Далтон", "Роберт Бойл", "Джозеф Пристли" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Антоан Лавоазие е открил закона за запазване на масата." },
            new Question { Id = 36, Text = "Кой е открил закона за запазване на импулса?", Answers = new List<string> { "Исак Нютон", "Галилео Галилей", "Йоханес Кеплер", "Рене Декарт" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Исак Нютон е открил закона за запазване на импулса." },
            new Question { Id = 37, Text = "Кой е открил закона за запазване на заряда?", Answers = new List<string> { "Бенджамин Франклин", "Майкъл Фарадей", "Андре-Мари Ампер", "Ханс Кристиан Ерстед" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Бенджамин Франклин е открил закона за запазване на заряда." },
            new Question { Id = 38, Text = "Кой е открил закона за запазване на момента на импулса?", Answers = new List<string> { "Исак Нютон", "Галилео Галилей", "Йоханес Кеплер", "Рене Декарт" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Исак Нютон е открил закона за запазване на момента на импулса." },
            new Question { Id = 39, Text = "Кой е открил закона за запазване на ентропията?", Answers = new List<string> { "Рудолф Клаузиус", "Лорд Келвин", "Съди Карно", "Джеймс Прескот Джаул" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Рудолф Клаузиус е открил закона за запазване на ентропията." },
            new Question { Id = 40, Text = "Кой е открил закона за запазване на масата и енергията?", Answers = new List<string> { "Алберт Айнщайн", "Исак Нютон", "Макс Планк", "Ниелс Бор" }, CorrectAnswerIndex = 0, Category = "Наука", Explanation = "Алберт Айнщайн е открил закона за запазване на масата и енергията." },

            // Изкуство
            new Question { Id = 41, Text = "Кой е авторът на картината \"Мона Лиза\"?", Answers = new List<string> { "Леонардо да Винчи", "Рафаело", "Микеланджело", "Тициан" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Леонардо да Винчи е авторът на картината \"Мона Лиза\"." },
            new Question { Id = 42, Text = "Кой е авторът на картината \"Създанието на Адам\"?", Answers = new List<string> { "Микеланджело", "Рафаело", "Леонардо да Винчи", "Тициан" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Микеланджело е авторът на картината \"Създанието на Адам\"." },
            new Question { Id = 43, Text = "Кой е авторът на картината \"Скулптурата на Давид\"?", Answers = new List<string> { "Микеланджело", "Рафаело", "Леонардо да Винчи", "Тициан" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Микеланджело е авторът на скулптурата на Давид." },
            new Question { Id = 44, Text = "Кой е авторът на картината \"Звездна нощ\"?", Answers = new List<string> { "Винсент ван Гог", "Питър Паул Рубенс", "Рембранд", "Клод Моне" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Винсент ван Гог е авторът на картината \"Звездна нощ\"." },
            new Question { Id = 45, Text = "Кой е авторът на картината \"Слънчогледи\"?", Answers = new List<string> { "Винсент ван Гог", "Питър Паул Рубенс", "Рембранд", "Клод Моне" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Винсент ван Гог е авторът на картината \"Слънчогледи\"." },
            new Question { Id = 46, Text = "Кой е авторът на операта \"Кармен\"?", Answers = new List<string> { "Жорж Бизе", "Джузепе Верди", "Волфганг Амадеус Моцарт", "Рихард Вагнер" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Жорж Бизе е авторът на операта \"Кармен\"." },
            new Question { Id = 47, Text = "Кой е авторът на операта \"Травиата\"?", Answers = new List<string> { "Джузепе Верди", "Жорж Бизе", "Волфганг Амадеус Моцарт", "Рихард Вагнер" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Джузепе Верди е авторът на операта \"Травиата\"." },
            new Question { Id = 48, Text = "Кой е авторът на операта \"Вълшебната флейта\"?", Answers = new List<string> { "Волфганг Амадеус Моцарт", "Джузепе Верди", "Жорж Бизе", "Рихард Вагнер" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Волфганг Амадеус Моцарт е авторът на операта \"Вълшебната флейта\"." },
            new Question { Id = 49, Text = "Кой е авторът на операта \"Тангойзер\"?", Answers = new List<string> { "Рихард Вагнер", "Джузепе Верди", "Жорж Бизе", "Волфганг Амадеус Моцарт" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Рихард Вагнер е авторът на операта \"Тангойзер\"." },
            new Question { Id = 50, Text = "Кой е авторът на операта \"Аида\"?", Answers = new List<string> { "Джузепе Верди", "Жорж Бизе", "Волфганг Амадеус Моцарт", "Рихард Вагнер" }, CorrectAnswerIndex = 0, Category = "Изкуство", Explanation = "Джузепе Верди е авторът на операта \"Аида\"." },

            // Спорт
            new Question { Id = 51, Text = "Кой отбор е печелил най-много пъти българското футболно първенство?", Answers = new List<string> { "ЦСКА София", "Левски София", "Лудогорец", "Славия" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "ЦСКА София е печелил 31 пъти българското футболно първенство." },
            new Question { Id = 52, Text = "Кой е единственият български футболист, печелил Златната топка?", Answers = new List<string> { "Христо Стоичков", "Димитър Бербатов", "Любослав Пенев", "Красимир Балъков" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Христо Стоичков печели Златната топка през 1994 година." },
            new Question { Id = 53, Text = "Кой български отбор е играл на финал в Шампионската лига?", Answers = new List<string> { "ЦСКА София", "Левски София", "Лудогорец", "Славия" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "ЦСКА София достига до полуфинал в КЕШ през 1982 година." },
            new Question { Id = 54, Text = "Кой е най-успешният български волейболен отбор при мъжете?", Answers = new List<string> { "ЦСКА София", "Левски София", "Марек Дупница", "Миньор Перник" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "ЦСКА София е най-успешният български волейболен отбор при мъжете." },
            new Question { Id = 55, Text = "Кой е най-успешният български баскетболен отбор?", Answers = new List<string> { "Лукойл Академик", "ЦСКА София", "Левски София", "Балкан Ботевград" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Лукойл Академик е най-успешният български баскетболен отбор." },
            new Question { Id = 56, Text = "Кой български отбор е печелил най-много титли в мъжкия волейбол?", Answers = new List<string> { "ЦСКА София", "Левски София", "Марек Дупница", "Миньор Перник" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "ЦСКА София е печелил най-много титли в мъжкия волейбол." },
            new Question { Id = 57, Text = "Кой български отбор е печелил най-много титли в женския волейбол?", Answers = new List<string> { "Левски София", "ЦСКА София", "Марица Пловдив", "Славия" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Левски София е печелил най-много титли в женския волейбол." },
            new Question { Id = 58, Text = "Кой български отбор е печелил най-много титли в мъжкия баскетбол?", Answers = new List<string> { "Лукойл Академик", "ЦСКА София", "Левски София", "Балкан Ботевград" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Лукойл Академик е печелил най-много титли в мъжкия баскетбол." },
            new Question { Id = 59, Text = "Кой български отбор е печелил най-много титли в женския баскетбол?", Answers = new List<string> { "Марица Пловдив", "Левски София", "ЦСКА София", "Славия" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Марица Пловдив е печелил най-много титли в женския баскетбол." },
            new Question { Id = 60, Text = "Кой български отбор е печелил най-много титли в хандбала?", Answers = new List<string> { "Локомотив Варна", "ЦСКА София", "Левски София", "Фрегата Бургас" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Локомотив Варна е печелил най-много титли в хандбала." },
            new Question { Id = 61, Text = "Кой е най-успешният български олимпийски шампион?", Answers = new List<string> { "Мария Гроздева", "Йорданка Донкова", "Стефка Костадинова", "Румяна Нейкова" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Мария Гроздева е най-успешният български олимпийски шампион с 5 олимпийски медала." },
            new Question { Id = 62, Text = "Кой е най-успешният български борец?", Answers = new List<string> { "Валентин Йорданов", "Боян Радев", "Александър Томов", "Петър Киров" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Валентин Йорданов е най-успешният български борец с олимпийско злато и 7 световни титли." },
            new Question { Id = 63, Text = "Коя е най-успешната българска гимнастичка?", Answers = new List<string> { "Мария Петрова", "Симона Пейчева", "Боряна Стоянова", "Цветелина Найденова" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Мария Петрова е най-успешната българска гимнастичка с множество световни и европейски титли." },
            new Question { Id = 64, Text = "Кой е най-успешният български плувец?", Answers = new List<string> { "Петър Стойчев", "Таня Богомилова", "Норайр Нурикян", "Иван Банков" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Петър Стойчев е най-успешният български плувец с множество световни титли в маратонското плуване." },
            new Question { Id = 65, Text = "Коя е най-успешната българска лекоатлетка?", Answers = new List<string> { "Стефка Костадинова", "Йорданка Донкова", "Тереза Маринова", "Ивет Лалова" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Стефка Костадинова е най-успешната българска лекоатлетка и световна рекордьорка във високия скок." },
            new Question { Id = 66, Text = "Кой е най-успешният български боксьор?", Answers = new List<string> { "Кубрат Пулев", "Серафим Тодоров", "Детелин Далаклиев", "Даниел Петров" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Кубрат Пулев е най-успешният български професионален боксьор." },
            new Question { Id = 67, Text = "Кой е най-успешният български тенисист?", Answers = new List<string> { "Григор Димитров", "Цветана Пиронкова", "Мануела Малеева", "Катерина Малеева" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Григор Димитров е най-успешният български тенисист, достигал до №3 в световната ранглиста." },
            new Question { Id = 68, Text = "Кой е най-успешният български волейболист?", Answers = new List<string> { "Пламен Константинов", "Любо Ганев", "Владимир Николов", "Матей Казийски" }, CorrectAnswerIndex = 0, Category = "Спорт", Explanation = "Пламен Константинов е най-успешният български волейболист с множество отборни и индивидуални отличия." }
        };

        private static Random _random = new Random();

        public QuizController(EmailService emailService, IOptions<EmailSettings> emailSettings)
        {
            _emailService = emailService;
            _emailSettings = emailSettings.Value;
        }

        public IActionResult Index()
        {
            var categories = _questions.Select(q => q.Category).Distinct().ToList();
            return View(categories);
        }

        [HttpPost]
        public IActionResult StartQuiz(string difficulty, List<string> categories, int questionCount)
        {
            var session = HttpContext.Session;
            
            // Взимаме всички налични въпроси от избраните категории
            var availableQuestions = _questions
                .Where(q => categories.Contains(q.Category))
                .ToList();

            // Проверяваме дали имаме достатъчно въпроси
            var actualQuestionCount = Math.Min(questionCount, availableQuestions.Count);
            
            session.SetString("Difficulty", difficulty);
            session.SetString("Categories", string.Join(",", categories));
            session.SetInt32("Score", 0);
            session.SetInt32("QuestionIndex", 0);
            session.SetInt32("QuestionCount", actualQuestionCount);

            // Разбъркване на въпросите и избор на точния брой
            var selectedQuestions = availableQuestions
                .OrderBy(x => _random.Next())
                .Take(actualQuestionCount)
                .ToList();

            session.SetString("QuestionOrder", string.Join(",", selectedQuestions.Select(q => q.Id)));
            session.SetInt32("TotalQuestions", actualQuestionCount);

            return Json(new { 
                success = true,
                actualQuestionCount = actualQuestionCount,
                totalAvailable = availableQuestions.Count
            });
        }

        public IActionResult GetQuestion()
        {
            var session = HttpContext.Session;
            var difficulty = session.GetString("Difficulty");
            var categories = session.GetString("Categories")?.Split(',') ?? Array.Empty<string>();
            var questionIndex = session.GetInt32("QuestionIndex") ?? 0;
            var totalQuestions = session.GetInt32("TotalQuestions") ?? 0;

            // Проверка дали тестът е приключил
            if (questionIndex >= totalQuestions)
            {
                return Json(new { completed = true });
            }

            var questionOrder = session.GetString("QuestionOrder")?.Split(',').Select(int.Parse).ToList() ?? new List<int>();
            
            // Проверка дали имаме достатъчно въпроси
            if (questionIndex >= questionOrder.Count)
            {
                return Json(new { completed = true });
            }

            var questionId = questionOrder[questionIndex];
            var question = _questions.FirstOrDefault(q => q.Id == questionId);

            if (question == null)
            {
                return Json(new { error = "Въпросът не е намерен" });
            }

            var questionCopy = new Question
            {
                Id = question.Id,
                Text = question.Text,
                Category = question.Category,
                Explanation = question.Explanation,
                Difficulty = difficulty
            };

            // Трансформиране на въпроса според избраното ниво на трудност
            if (difficulty == "Лесно")
            {
                if (question.Answers != null && question.Answers.Count > 2)
                {
                    var correctAnswer = question.Answers[question.CorrectAnswerIndex ?? 0];
                    var wrongAnswers = question.Answers.Where((ans, idx) => idx != question.CorrectAnswerIndex).ToList();
                    var randomWrongAnswer = wrongAnswers[_random.Next(wrongAnswers.Count)];
                    
                    questionCopy.Answers = new List<string> { correctAnswer, randomWrongAnswer };
                    questionCopy.CorrectAnswerIndex = 0;
                }
                else
                {
                    questionCopy.Answers = question.Answers;
                    questionCopy.CorrectAnswerIndex = question.CorrectAnswerIndex;
                }
            }
            else if (difficulty == "Трудно")
            {
                if (question.Answers != null && question.CorrectAnswerIndex.HasValue)
                {
                    questionCopy.CorrectAnswerText = question.Answers[question.CorrectAnswerIndex.Value];
                    questionCopy.Answers = null;
                    questionCopy.CorrectAnswerIndex = null;
                }
            }
            else // Средно
            {
                questionCopy.Answers = question.Answers;
                questionCopy.CorrectAnswerIndex = question.CorrectAnswerIndex;
            }

            // Разбъркване на отговорите
            if (questionCopy.Answers != null)
            {
                var correctAnswer = questionCopy.Answers[questionCopy.CorrectAnswerIndex ?? 0];
                questionCopy.Answers = questionCopy.Answers.OrderBy(x => _random.Next()).ToList();
                questionCopy.CorrectAnswerIndex = questionCopy.Answers.IndexOf(correctAnswer);
            }

            session.SetInt32("CurrentQuestionId", question.Id);
            session.SetInt32("QuestionIndex", questionIndex + 1);

            return Json(new { 
                question = questionCopy,
                progress = new { current = questionIndex + 1, total = totalQuestions }
            });
        }

        [HttpPost]
        public IActionResult CheckAnswer(string answer)
        {
            var session = HttpContext.Session;
            var questionId = session.GetInt32("CurrentQuestionId") ?? 0;
            var difficulty = session.GetString("Difficulty");
            var question = _questions.FirstOrDefault(q => q.Id == questionId);

            if (question == null)
            {
                return Json(new { error = "Въпросът не е намерен" });
            }

            bool isCorrect;
            string correctAnswer;

            if (difficulty == "Трудно")
            {
                var cleanAnswer = answer.Trim().ToLower().Replace(" ", "");
                var cleanCorrectAnswer = question.Answers[question.CorrectAnswerIndex ?? 0].Trim().ToLower().Replace(" ", "");
                isCorrect = cleanAnswer == cleanCorrectAnswer;
                correctAnswer = question.Answers[question.CorrectAnswerIndex ?? 0];
            }
            else
            {
                var cleanAnswer = answer.Trim().ToLower().Replace(" ", "");
                var cleanCorrectAnswer = question.Answers[question.CorrectAnswerIndex ?? 0].Trim().ToLower().Replace(" ", "");
                isCorrect = cleanAnswer == cleanCorrectAnswer;
                correctAnswer = question.Answers[question.CorrectAnswerIndex ?? 0];
            }

            var currentScore = session.GetInt32("Score") ?? 0;
            if (isCorrect)
            {
                currentScore++;
                session.SetInt32("Score", currentScore);
            }

            return Json(new
            {
                isCorrect = isCorrect,
                correctAnswer = correctAnswer,
                explanation = question.Explanation,
                score = currentScore
            });
        }

        public IActionResult GetResult()
        {
            var session = HttpContext.Session;
            var score = session.GetInt32("Score") ?? 0;
            var totalQuestions = session.GetInt32("TotalQuestions") ?? 0;

            return Json(new { score = score, total = totalQuestions });
        }

        [HttpPost]
        public async Task<IActionResult> ReportProblem([FromBody] ReportData report)
        {
            try
            {
                var subject = $"Доклад за проблем в квиз - {report.ReportType}";
                var body = $@"
                    <h3>Доклад за проблем в квиз</h3>
                    <p><strong>Тип проблем:</strong> {report.ReportType}</p>
                    <p><strong>Въпрос ID:</strong> {report.QuestionId}</p>
                    <p><strong>Текст на въпроса:</strong> {report.QuestionText}</p>
                    <p><strong>Описание:</strong> {report.Description}</p>
                    <p><strong>Имейл на докладващия:</strong> {report.Email}</p>
                ";

                await _emailService.SendEmailAsync(_emailSettings.FromEmail, subject, body);
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SuggestQuestion([FromBody] QuestionSuggestion suggestion)
        {
            try
            {
                var subject = "Ново предложение за въпрос в квиз";
                var body = $@"
                    <h3>Ново предложение за въпрос</h3>
                    <p><strong>Въпрос:</strong> {suggestion.QuestionText}</p>
                    <p><strong>Правилен отговор:</strong> {suggestion.CorrectAnswer}</p>
                    <p><strong>Грешни отговори:</strong></p>
                    <ul>
                        {string.Join("", suggestion.WrongAnswers.Select(a => $"<li>{a}</li>"))}
                    </ul>
                    <p><strong>Категория:</strong> {suggestion.Category}</p>
                    <p><strong>Описание:</strong> {suggestion.Description}</p>
                    <p><strong>Имейл на предложителя:</strong> {suggestion.SuggestorEmail}</p>
                ";

                await _emailService.SendEmailAsync(_emailSettings.FromEmail, subject, body);
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }

    public class ReportData
    {
        public int QuestionId { get; set; }
        public required string QuestionText { get; set; }
        public required string ReportType { get; set; }
        public required string Description { get; set; }
        public required string Email { get; set; }
    }

    public class QuestionSuggestion
    {
        public required string QuestionText { get; set; }
        public required string CorrectAnswer { get; set; }
        public required string[] WrongAnswers { get; set; }
        public required string Category { get; set; }
        public string? Description { get; set; }
        public string? SuggestorEmail { get; set; }
    }
}