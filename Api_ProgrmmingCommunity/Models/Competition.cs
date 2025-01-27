using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class Competition
{
    public int CompetitionId { get; set; }

    public string? Title { get; set; }

    public int? Year { get; set; }

    public int? MinLevel { get; set; }

    public int? MaxLevel { get; set; }

    public string? Password { get; set; }

    public int? UserId { get; set; }

    public bool? IsDeleted { get; set; }

    public int? Rounds { get; set; }

    public virtual ICollection<CompetitionRound> CompetitionRounds { get; set; } = new List<CompetitionRound>();

    public virtual ICollection<CompetitionTeam> CompetitionTeams { get; set; } = new List<CompetitionTeam>();

    public virtual ICollection<RoundResult> RoundResults { get; set; } = new List<RoundResult>();

    public virtual User? User { get; set; }

    public virtual ICollection<WinnerBoard> WinnerBoards { get; set; } = new List<WinnerBoard>();
}
