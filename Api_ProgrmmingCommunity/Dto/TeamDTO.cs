namespace Api_ProgrmmingCommunity.Dto
    {
    public class TeamDTO
        {
        public int TeamId { get; set; }

        public string? TeamName { get; set; }

        public int? Member1Id { get; set; }

        public int? Member2Id { get; set; }

        public int? Member3Id { get; set; }

        public bool? IsDeleted { get; set; }

        }
    }
