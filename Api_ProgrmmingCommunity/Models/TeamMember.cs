using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class TeamMember
{
    public int Id { get; set; }

    public int? TeamId { get; set; }

    public int? UserId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Team? Team { get; set; }

    public virtual User? User { get; set; }
}
