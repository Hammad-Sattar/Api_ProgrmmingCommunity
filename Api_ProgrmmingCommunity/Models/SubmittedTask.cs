using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class SubmittedTask
{
    public int Id { get; set; }

    public int? TaskId { get; set; }

    public int? QuestionId { get; set; }

    public int? UserId { get; set; }

    public string? Answer { get; set; }

    public DateOnly? SubmissionDate { get; set; }

    public TimeOnly? SubmissionTime { get; set; }

    public int? Score { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Question? Question { get; set; }

    public virtual Task? Task { get; set; }

    public virtual User? User { get; set; }
}
