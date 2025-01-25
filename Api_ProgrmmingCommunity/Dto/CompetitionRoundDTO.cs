namespace Api_ProgrmmingCommunity.Dto
    {
    public class CompetitionRoundDTO
        {
        public int Id { get; set; }

        public int? CompetitionId { get; set; }

        public int? RoundNumber { get; set; }

        public int? RoundType { get; set; }

        public DateOnly? Date { get; set; }

        

        }
    }
