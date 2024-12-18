using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class TaskQuestion
{
    public int Id { get; set; }

    public int? TaskId { get; set; }

    public int? QuestionId { get; set; }

    public virtual Question? Question { get; set; }

    public virtual ICollection<SubmittedTask> SubmittedTasks { get; set; } = new List<SubmittedTask>();

    public virtual Task? Task { get; set; }
}
