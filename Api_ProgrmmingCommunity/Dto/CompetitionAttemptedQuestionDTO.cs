namespace Api_ProgrmmingCommunity.Dto
    {
    public class CompetitionAttemptedQuestionDTO
        {

        public int Id { get; set; }

        public int? CompetitionId { get; set; }

        public int? CompetitionRoundId { get; set; }

        public int? QuestionId { get; set; }

        public int? TeamId { get; set; }

        public string? Answer { get; set; }

        public int? Score { get; set; }

        public DateTime? SubmissionTime { get; set; }




        }
    }
