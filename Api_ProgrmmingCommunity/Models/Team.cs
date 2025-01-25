using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public string? TeamName { get; set; }

    public int? Member1Id { get; set; }

    public int? Member2Id { get; set; }

    public int? Member3Id { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<CompetitionAttemptedQuestion> CompetitionAttemptedQuestions { get; set; } = new List<CompetitionAttemptedQuestion>();

    public virtual ICollection<CompetitionTeam> CompetitionTeams { get; set; } = new List<CompetitionTeam>();

    public virtual User? Member1 { get; set; }

    public virtual User? Member2 { get; set; }

    public virtual User? Member3 { get; set; }

    public virtual ICollection<RoundResult> RoundResults { get; set; } = new List<RoundResult>();

    public virtual ICollection<WinnerBoard> WinnerBoards { get; set; } = new List<WinnerBoard>();
}
