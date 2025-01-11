namespace Api_ProgrmmingCommunity.Dto
{
    public  class QuestionDTO
    {
        public int Id { get; set; }

        public string? SubjectCode { get; set; }

        public int? TopicId { get; set; }

        public int? UserId { get; set; }

        public int? Difficulty { get; set; }

        public string? Text { get; set; }

        public int? Type { get; set; }

        public bool? IsDeleted { get; set; }
        }
}
