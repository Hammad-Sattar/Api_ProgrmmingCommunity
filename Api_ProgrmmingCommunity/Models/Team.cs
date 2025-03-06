using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public string? TeamName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<CompetitionAttemptedQuestion> CompetitionAttemptedQuestions { get; set; } = new List<CompetitionAttemptedQuestion>();

    public virtual ICollection<CompetitionTeam> CompetitionTeams { get; set; } = new List<CompetitionTeam>();

    public virtual ICollection<RoundResult> RoundResults { get; set; } = new List<RoundResult>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();

    public virtual ICollection<WinnerBoard> WinnerBoards { get; set; } = new List<WinnerBoard>();
}
