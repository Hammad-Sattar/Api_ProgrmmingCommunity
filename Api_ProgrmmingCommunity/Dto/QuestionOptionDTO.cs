namespace Api_ProgrmmingCommunity.Dto
    {
    public class QuestionOptionDTO
        {
        public int Id { get; set; }

        public int? QuestionId { get; set; }

        public string? Option { get; set; }

        public bool? IsCorrect { get; set; }

        public bool? IsDeleted { get; set; }
        }
    }
