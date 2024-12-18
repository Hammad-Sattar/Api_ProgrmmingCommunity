using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class Subject
{
    public string Code { get; set; } = null!;

    public string? Title { get; set; }

    public virtual ICollection<ExpertSubject> ExpertSubjects { get; set; } = new List<ExpertSubject>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
