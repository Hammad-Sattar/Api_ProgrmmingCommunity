using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class CompetitionRoundQuestion
{
    public int Id { get; set; }

    public int? CompetitionRoundId { get; set; }

    public int? QuestionId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual CompetitionRound? CompetitionRound { get; set; }

    public virtual Question? Question { get; set; }
}
