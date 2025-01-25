using Api_ProgrmmingCommunity.Dto;

namespace MapProjectApi.Models.DTOs
    {
    public class QuestionDto
        {
        public int Id { get; set; }
        public string? SubjectCode { get; set; }
        public int? TopicId { get; set; }
        public int? UserId { get; set; }
        public int? Difficulty { get; set; }
        public string? Text { get; set; }
        public int? Type { get; set; }
        }
    public class QuestionDtoList
        {
        public int Id { get; set; }
        public string? SubjectCode { get; set; }
        public int? TopicId { get; set; }
        public int? UserId { get; set; }
        public int? Difficulty { get; set; }
        public string? Text { get; set; }
        public int? Type { get; set; }

        public List<QuestionOptionDTO> Options { get; set; }
        }
    }
