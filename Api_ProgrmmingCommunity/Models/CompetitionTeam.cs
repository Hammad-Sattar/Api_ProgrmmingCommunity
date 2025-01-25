using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class CompetitionTeam
{
    public int Id { get; set; }

    public int? CompetitionId { get; set; }

    public int? TeamId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual Team? Team { get; set; }
}
