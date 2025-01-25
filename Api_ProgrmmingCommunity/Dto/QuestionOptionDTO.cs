namespace MapProjectApi.Models.DTOs
    {
    public class QuestionOptionDTO
        {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public string? Option { get; set; }
        public bool? IsCorrect { get; set; }
        }
    }
