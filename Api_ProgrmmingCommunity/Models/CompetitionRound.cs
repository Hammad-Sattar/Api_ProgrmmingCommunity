using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class CompetitionRound
{
    public int Id { get; set; }

    public int? CompetitionId { get; set; }

    public int? RoundNumber { get; set; }

    public int? RoundType { get; set; }

    public DateOnly? Date { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual ICollection<CompetitionAttemptedQuestion> CompetitionAttemptedQuestions { get; set; } = new List<CompetitionAttemptedQuestion>();

    public virtual ICollection<CompetitionRoundQuestion> CompetitionRoundQuestions { get; set; } = new List<CompetitionRoundQuestion>();

    public virtual ICollection<RoundQualificationCriterion> RoundQualificationCriterionFromRounds { get; set; } = new List<RoundQualificationCriterion>();

    public virtual ICollection<RoundQualificationCriterion> RoundQualificationCriterionToRounds { get; set; } = new List<RoundQualificationCriterion>();

    public virtual ICollection<RoundResult> RoundResults { get; set; } = new List<RoundResult>();
}
