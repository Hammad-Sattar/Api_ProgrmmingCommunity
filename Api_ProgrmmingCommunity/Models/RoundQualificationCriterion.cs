using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class RoundQualificationCriterion
{
    public int Id { get; set; }

    public int? FromRoundId { get; set; }

    public int? ToRoundId { get; set; }

    public int? TopTeams { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual CompetitionRound? FromRound { get; set; }

    public virtual CompetitionRound? ToRound { get; set; }
}
