using System;
using System.Collections.Generic;

namespace Obshta_Kultura.Models
{
    public class Question
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public List<string>? Answers { get; set; }
        public int? CorrectAnswerIndex { get; set; }
        public string? CorrectAnswerText { get; set; }
        public required string Category { get; set; }
        public string? Explanation { get; set; }
        public string? Difficulty { get; set; }
    }
} 