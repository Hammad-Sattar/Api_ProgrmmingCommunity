using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Password { get; set; }

    public int? Role { get; set; }

    public string? RegNum { get; set; }

    public string? Section { get; set; }

    public int? Semester { get; set; }

    public string? Email { get; set; }

    public string? Phonenum { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public bool? IsDeleted { get; set; }

    public string? Empid { get; set; }

    public int? Level { get; set; }

    public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();

    public virtual ICollection<ExpertSubject> ExpertSubjects { get; set; } = new List<ExpertSubject>();

    public virtual ICollection<ExpertTopic> ExpertTopics { get; set; } = new List<ExpertTopic>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

    public virtual ICollection<SubmittedTask> SubmittedTasks { get; set; } = new List<SubmittedTask>();

    public virtual ICollection<Team> TeamMember1s { get; set; } = new List<Team>();

    public virtual ICollection<Team> TeamMember2s { get; set; } = new List<Team>();

    public virtual ICollection<Team> TeamMember3s { get; set; } = new List<Team>();
}
