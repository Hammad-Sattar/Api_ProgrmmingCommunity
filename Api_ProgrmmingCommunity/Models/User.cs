using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Password { get; set; }

    public string? Profimage { get; set; }

    public int? Role { get; set; }

    public string? RegNum { get; set; }

    public string? Section { get; set; }

    public int? Semester { get; set; }

    public string? Email { get; set; }

    public string? Phonenum { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public virtual ICollection<CompetitionAttemptedQuestion> CompetitionAttemptedQuestions { get; set; } = new List<CompetitionAttemptedQuestion>();

    public virtual ICollection<CompetitionMember> CompetitionMembers { get; set; } = new List<CompetitionMember>();

    public virtual ICollection<ExpertSubject> ExpertSubjects { get; set; } = new List<ExpertSubject>();

    public virtual ICollection<ExpertTopic> ExpertTopics { get; set; } = new List<ExpertTopic>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<RoundResult> RoundResults { get; set; } = new List<RoundResult>();

    public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

    public virtual ICollection<SubmittedTask> SubmittedTasks { get; set; } = new List<SubmittedTask>();
}
