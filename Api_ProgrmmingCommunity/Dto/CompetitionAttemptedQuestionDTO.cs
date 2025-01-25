namespace Api_ProgrmmingCommunity.Dto
    {
    public class CompetitionAttemptedQuestionDTO
        {

        public int Id { get; set; }

        public int? CompetitionRoundQuestionId { get; set; }

        public int? TeamId { get; set; }

        public string? Answer { get; set; }

        public int? Score { get; set; }

        public TimeOnly? SubmissionTime { get; set; }



        }
    }
