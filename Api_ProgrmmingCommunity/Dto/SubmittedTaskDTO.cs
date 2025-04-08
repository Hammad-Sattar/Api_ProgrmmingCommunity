namespace Api_ProgrmmingCommunity.Dto
    {
    public class SubmittedTaskDTO
        {
        public int Id { get; set; }

        public int? TaskId { get; set; }

        public int? QuestionId { get; set; }

        public int? UserId { get; set; }

        public string? Answer { get; set; }

        public DateOnly? SubmissionDate { get; set; }

        public TimeOnly? SubmissionTime { get; set; }

        public int? Score { get; set; }
        }
    }
