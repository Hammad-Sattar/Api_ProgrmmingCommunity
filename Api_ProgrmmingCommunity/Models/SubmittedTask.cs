using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class SubmittedTask
{
    public int Id { get; set; }

    public int? TaskquestionId { get; set; }

    public int? UserId { get; set; }

    public string? Answer { get; set; }

    public DateOnly? SubmissionDate { get; set; }

    public TimeOnly? SubmissionTime { get; set; }

    public int? Score { get; set; }

   

    public virtual TaskQuestion? Taskquestion { get; set; }

    public virtual User? User { get; set; }
}
