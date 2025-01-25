namespace Api_ProgrmmingCommunity.Dto
{
    public class RoundResultDTO
    {

        public int Id { get; set; }

        public int? CompetitionRoundId { get; set; }

        public int? TeamId { get; set; }

        public int? Score { get; set; }

        public int? CompetitionId { get; set; }

        public bool? IsQualified { get; set; }

       

        }
    }
