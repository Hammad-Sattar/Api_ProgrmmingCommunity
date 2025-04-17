using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class CompetitionAttemptedQuestion
{
    public int Id { get; set; }

    public int? CompetitionId { get; set; }

    public int? CompetitionRoundId { get; set; }

    public int? QuestionId { get; set; }

    public int? TeamId { get; set; }

    public string? Answer { get; set; }

    public int? Score { get; set; }

    public DateTime? SubmissionTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual CompetitionRound? CompetitionRound { get; set; }

    public virtual Question? Question { get; set; }

    public virtual Team? Team { get; set; }
}
