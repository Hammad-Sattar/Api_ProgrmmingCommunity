using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class QuestionOutput
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string Output { get; set; } = null!;

    public bool? IsDeleted { get; set; }
}
