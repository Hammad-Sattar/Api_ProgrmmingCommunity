using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class CompetitionAttemptedQuestion
{
    public int Id { get; set; }

    public int? CompetitionRoundQuestionId { get; set; }

    public int? TeamId { get; set; }

    public string? Answer { get; set; }

    public int? Score { get; set; }

    public TimeOnly? SubmissionTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual CompetitionRoundQuestion? CompetitionRoundQuestion { get; set; }

    public virtual Team? Team { get; set; }
}
