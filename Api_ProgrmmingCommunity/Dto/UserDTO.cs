using System.Text.Json.Serialization;

namespace Api_ProgrmmingCommunity.Dto
{
    public class UserDTO
    {

        public int Id { get; set; }

        public string? Password { get; set; }

      public int? Level { get; set; }

        public int? Role { get; set; }

        public string? RegNum { get; set; }

        public string? Section { get; set; }

        public int? Semester { get; set; }

        public string? Email { get; set; }

        public string? Phonenum { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }
       
      

        }
    }
