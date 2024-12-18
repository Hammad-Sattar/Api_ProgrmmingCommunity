using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class RoundResult
{
    public int Id { get; set; }

    public int? CompetitionRoundId { get; set; }

    public int? UserId { get; set; }

    public int? Score { get; set; }

    public int? CompetitionId { get; set; }

    public bool? IsQualified { get; set; }

    public virtual CompetitionRound? CompetitionRound { get; set; }

    public virtual User? User { get; set; }
}
