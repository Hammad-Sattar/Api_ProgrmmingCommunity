using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class Task
{
    public int Id { get; set; }

    public int? MinLevel { get; set; }

    public int? MaxLevel { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? UserId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<SubmittedTask> SubmittedTasks { get; set; } = new List<SubmittedTask>();

    public virtual ICollection<TaskQuestion> TaskQuestions { get; set; } = new List<TaskQuestion>();

    public virtual User? User { get; set; }
}
