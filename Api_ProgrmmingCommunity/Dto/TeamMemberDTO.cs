namespace Api_ProgrmmingCommunity.Dto
    {
    public class TeamMemberDTO
        {

        public int Id { get; set; }

        public int? TeamId { get; set; }

        public int? UserId { get; set; }

     

        }

    public class TeamMemberDTOWithUser
        {
        public int TeamId { get; set; }

        public string? TeamName { get; set; }

        public List<TeamMemberDTO>? Users { get; set; }

        }
    }
