using Microsoft.AspNetCore.Mvc;
using Obshta_Kultura.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Obshta_Kultura.Controllers
{
    public class QuizController : Controller
    {
        private static List<Question> _questions = new List<Question>
        {
            // История
            new Question {
                Id = 1,
                Text = "Кога е основана българската държава?",
                Options = new List<string> { "681 г.", "855 г.", "864 г.", "632 г." },
                CorrectAnswerIndex = 0,
                Category = "История",
                Explanation = "България е основана през 681 г. от хан Аспарух."
            },
            new Question {
                Id = 2,
                Text = "Кой е първият български владетел?",
                Options = new List<string> { "Хан Кубрат", "Хан Аспарух", "Хан Тервел", "Хан Крум" },
                CorrectAnswerIndex = 1,
                Category = "История",
                Explanation = "Хан Аспарух е основателят на Дунавска България и първият ѝ владетел."
            },
            new Question {
                Id = 3,
                Text = "През коя година България влиза в Европейския съюз?",
                Options = new List<string> { "2004 г.", "2007 г.", "2009 г.", "2012 г." },
                CorrectAnswerIndex = 1,
                Category = "История",
                Explanation = "България става член на Европейския съюз на 1 януари 2007 г."
            },
            new Question {
                Id = 4,
                Text = "Кой български владетел въвежда християнството като официална религия?",
                Options = new List<string> { "Цар Симеон", "Хан Крум", "Княз Борис I", "Цар Самуил" },
                CorrectAnswerIndex = 2,
                Category = "История",
                Explanation = "Княз Борис I покръства българите през 864 г."
            },
            new Question {
                Id = 5,
                Text = "Кой е автор на \"История славянобългарска\"?",
                TextAnswer = "Паисий Хилендарски",
                Category = "История",
                Explanation = "Паисий Хилендарски написва \"История славянобългарска\" през 1762 г. в Хилендарския манастир."
            },

            // География
            new Question {
                Id = 6,
                Text = "Кой е най-високият връх в България?",
                Options = new List<string> { "Мусала", "Вихрен", "Ботев", "Черни връх" },
                CorrectAnswerIndex = 0,
                Category = "География",
                Explanation = "Връх Мусала (2925 м) е най-високият връх в България и на Балканския полуостров."
            },
            new Question {
                Id = 7,
                Text = "Коя е най-дългата река в България?",
                Options = new List<string> { "Дунав", "Марица", "Искър", "Струма" },
                CorrectAnswerIndex = 0,
                Category = "География",
                Explanation = "Река Дунав е най-дългата река, протичаща по територията на България."
            },
            new Question {
                Id = 8,
                Text = "Кое е най-голямото езеро в България?",
                Options = new List<string> { "Варненско езеро", "Бургаско езеро", "Сребърна", "Поморийско езеро" },
                CorrectAnswerIndex = 0,
                Category = "География",
                Explanation = "Варненското езеро е най-голямото езеро в България с площ от 17 км²."
            },
            new Question {
                Id = 9,
                Text = "Кой е най-старият действащ природен резерват в България?",
                TextAnswer = "Силкосия",
                Category = "География",
                Explanation = "Резерват \"Силкосия\" е обявен през 1931 г. и е първият официално обявен резерват в България."
            },

            // Литература
            new Question {
                Id = 10,
                Text = "Кой е авторът на \"Под игото\"?",
                Options = new List<string> { "Иван Вазов", "Христо Ботев", "Йордан Йовков", "Елин Пелин" },
                CorrectAnswerIndex = 0,
                Category = "Литература",
                Explanation = "\"Под игото\" е написан от Иван Вазов и е публикуван през 1894 г."
            },
            new Question {
                Id = 11,
                Text = "Кой е авторът на стихотворението \"На прощаване\"?",
                Options = new List<string> { "Христо Ботев", "Пейо Яворов", "Димчо Дебелянов", "Никола Вапцаров" },
                CorrectAnswerIndex = 0,
                Category = "Литература",
                Explanation = "Стихотворението \"На прощаване\" е написано от Христо Ботев през 1868 г."
            },
            new Question {
                Id = 12,
                Text = "Кое е първото печатно българско списание?",
                TextAnswer = "Любословие",
                Category = "Литература",
                Explanation = "\"Любословие\" е първото българско списание, издавано от Константин Фотинов в Смирна през 1844-1846 г."
            },

            // Изкуство
            new Question {
                Id = 13,
                Text = "Кой е авторът на картината \"Рожен\"?",
                Options = new List<string> { "Владимир Димитров - Майстора", "Златю Бояджиев", "Иван Мърквичка", "Ярослав Вешин" },
                CorrectAnswerIndex = 0,
                Category = "Изкуство",
                Explanation = "Картината \"Рожен\" е една от най-известните творби на Владимир Димитров - Майстора."
            },
            new Question {
                Id = 14,
                Text = "Кой е първият български професионален художник?",
                Options = new List<string> { "Станислав Доспевски", "Николай Павлович", "Захарий Зограф", "Димитър Зограф" },
                CorrectAnswerIndex = 0,
                Category = "Изкуство",
                Explanation = "Станислав Доспевски е първият български художник с академично образование."
            },
            new Question {
                Id = 15,
                Text = "Кой е архитектът на храм-паметника \"Александър Невски\"?",
                TextAnswer = "Александър Померанцев",
                Category = "Изкуство",
                Explanation = "Храм-паметникът е проектиран от руския архитект Александър Померанцев в неовизантийски стил."
            }
        };

        private static List<string> _selectedCategories = new List<string>();
        private static string _selectedDifficulty = "Лесно";
        private static Random _random = new Random();
        private static HashSet<int> _usedQuestions = new HashSet<int>();

        private static int GetSafeIndex(List<string> list, string item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == item)
                {
                    return i;
                }
            }
            return 0;
        }

        public IActionResult Index()
        {
            var categories = _questions
                .Where(q => q.Category != null)
                .Select(q => q.Category)
                .Distinct()
                .Select(c => new CategoryModel { Name = c })
                .ToList();

            return View(categories);
        }

        [HttpPost]
        public IActionResult SetCategories([FromBody] CategorySelectionModel model)
        {
            if (model.Categories == null || model.Difficulty == null)
            {
                return Json(new { success = false, error = "Невалидни данни" });
            }

            var session = HttpContext.Session;
            session.SetString("SelectedCategories", string.Join(",", model.Categories));
            session.SetString("Difficulty", model.Difficulty);
            session.SetInt32("CurrentQuestionIndex", 0);
            session.SetInt32("Score", 0);

            return Json(new { success = true });
        }

        public IActionResult GetCategories()
        {
            var categories = _questions
                .Select(q => q.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            return Json(categories);
        }

        public IActionResult GetQuestion()
        {
            var session = HttpContext.Session;
            var currentQuestionIndex = session.GetInt32("CurrentQuestionIndex") ?? 0;
            var selectedCategories = session.GetString("SelectedCategories")?.Split(',') ?? Array.Empty<string>();
            var difficulty = session.GetString("Difficulty") ?? "Средно";

            var availableQuestions = _questions
                .Where(q => selectedCategories.Contains(q.Category))
                .ToList();

            if (!availableQuestions.Any())
            {
                return RedirectToAction("Result");
            }

            var question = availableQuestions[currentQuestionIndex % availableQuestions.Count];
            
            // Създаваме копие на въпроса, за да не променяме оригиналния
            var questionCopy = new Question
            {
                Id = question.Id,
                Text = question.Text,
                Category = question.Category,
                Explanation = question.Explanation,
                TextAnswer = question.TextAnswer
            };
            
            // Преобразуваме въпроса според избраната трудност
            if (difficulty == "Лесно")
            {
                // За лесно ниво показваме само първите две опции
                if (question.Options != null && question.Options.Count > 2)
                {
                    var correctOption = question.Options[question.CorrectAnswerIndex ?? 0];
                    var wrongOptions = question.Options.Where((opt, idx) => idx != question.CorrectAnswerIndex).ToList();
                    var randomWrongOption = wrongOptions[_random.Next(wrongOptions.Count)];
                    
                    questionCopy.Options = new List<string> { correctOption, randomWrongOption };
                    questionCopy.CorrectAnswerIndex = 0; // Винаги първата опция е правилната
                }
                else
                {
                    questionCopy.Options = question.Options;
                    questionCopy.CorrectAnswerIndex = question.CorrectAnswerIndex;
                }
            }
            else if (difficulty == "Трудно")
            {
                // За трудно ниво преобразуваме в текстов въпрос
                if (question.Options != null && question.CorrectAnswerIndex.HasValue)
                {
                    questionCopy.TextAnswer = question.Options[question.CorrectAnswerIndex.Value];
                    questionCopy.Options = null;
                    questionCopy.CorrectAnswerIndex = null;
                }
            }
            else
            {
                // За средно ниво показваме всички опции
                questionCopy.Options = question.Options;
                questionCopy.CorrectAnswerIndex = question.CorrectAnswerIndex;
            }

            // Увеличаваме брояча на въпросите
            session.SetInt32("CurrentQuestionIndex", currentQuestionIndex + 1);

            return Json(questionCopy);
        }

        [HttpPost]
        public IActionResult CheckAnswer(int questionId, string answer)
        {
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
            {
                return Json(new { success = false, message = "Въпросът не е намерен!" });
            }

            var isCorrect = false;
            var correctAnswer = "";
            var difficulty = HttpContext.Session.GetString("Difficulty") ?? "Средно";

            if (difficulty == "Лесно")
            {
                // За лесно ниво проверяваме с оригиналния правилен отговор
                if (question.Options != null && question.Options.Count > 2)
                {
                    var correctOption = question.Options[question.CorrectAnswerIndex ?? 0];
                    isCorrect = answer == correctOption;
                    correctAnswer = correctOption;
                }
                else
                {
                    isCorrect = answer == question.Options?[question.CorrectAnswerIndex ?? 0];
                    correctAnswer = question.Options?[question.CorrectAnswerIndex ?? 0] ?? "";
                }
            }
            else if (question.CorrectAnswerIndex.HasValue && question.Options != null)
            {
                // За въпроси с опции
                isCorrect = answer == question.Options[question.CorrectAnswerIndex.Value];
                correctAnswer = question.Options[question.CorrectAnswerIndex.Value];
            }
            else if (!string.IsNullOrEmpty(question.TextAnswer))
            {
                // За текстови въпроси
                isCorrect = answer.Trim().Equals(question.TextAnswer.Trim(), StringComparison.OrdinalIgnoreCase);
                correctAnswer = question.TextAnswer;
            }

            var session = HttpContext.Session;
            var currentScore = session.GetInt32("Score") ?? 0;
            if (isCorrect)
            {
                currentScore++;
                session.SetInt32("Score", currentScore);
            }

            return Json(new
            {
                success = true,
                isCorrect = isCorrect,
                correctAnswer = correctAnswer,
                explanation = question.Explanation,
                score = currentScore
            });
        }

        public class CategorySelectionModel
        {
            public List<string>? Categories { get; set; }
            public string? Difficulty { get; set; }
        }

        public class AnswerModel
        {
            public int QuestionId { get; set; }
            public object? SelectedAnswer { get; set; }
            public bool IsTextAnswer { get; set; }
        }
    }
} 