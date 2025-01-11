using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class QuestionOption
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public string? Option { get; set; }

    public bool? IsCorrect { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Question? Question { get; set; }
}
