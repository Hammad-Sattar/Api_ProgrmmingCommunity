namespace Api_ProgrmmingCommunity.Dto
    {
    public class SubmittedTaskDTO
        {
        public int Id { get; set; }

        public int? TaskquestionId { get; set; }

        public int? UserId { get; set; }

        public string? Answer { get; set; }
        }
    }
