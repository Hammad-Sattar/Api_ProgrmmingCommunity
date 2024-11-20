using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class CompetitionMember
{
    public int Id { get; set; }

    public int? CompetitionId { get; set; }

    public int? UserId { get; set; }

   

    public virtual Competition? Competition { get; set; }

    public virtual User? User { get; set; }
}
