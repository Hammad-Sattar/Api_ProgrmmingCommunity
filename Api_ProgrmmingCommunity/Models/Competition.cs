using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class Competition
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Year { get; set; }

    public int? MinLevel { get; set; }

    public int? MaxLevel { get; set; }

    public virtual ICollection<CompetitionMember> CompetitionMembers { get; set; } = new List<CompetitionMember>();

    public virtual ICollection<CompetitionRound> CompetitionRounds { get; set; } = new List<CompetitionRound>();
}
