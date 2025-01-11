namespace Api_ProgrmmingCommunity.Dto
    {
    public class RoundQualificationCreteriaDTO
        {
        
            public int Id { get; set; }

            public int? FromRoundId { get; set; }

            public int? ToRoundId { get; set; }

            public int? TopTeams { get; set; }

            public bool? IsDeleted { get; set; }
            }
    }
