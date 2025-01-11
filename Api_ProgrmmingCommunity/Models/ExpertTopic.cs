using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class ExpertTopic
{
    public int Id { get; set; }

    public int? ExpertId { get; set; }

    public int? TopicId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User? Expert { get; set; }

    public virtual Topic? Topic { get; set; }
}
