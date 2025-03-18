using System;
using System.Collections.Generic;

namespace Obshta_Kultura.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public List<string>? Options { get; set; }
        public int? CorrectAnswerIndex { get; set; }
        public string? TextAnswer { get; set; }
        public string? Category { get; set; }
        public string? Explanation { get; set; }
        public string? ImageUrl { get; set; }
    }
} 