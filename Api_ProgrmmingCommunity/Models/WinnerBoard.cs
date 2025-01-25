using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class WinnerBoard
{
    public int Id { get; set; }

    public int? CompetitionId { get; set; }

    public int? TeamId { get; set; }

    public int? Score { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual Team? Team { get; set; }
}
