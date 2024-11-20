using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class Question
{
    public int Id { get; set; }

    public string? SubjectCode { get; set; }

    public int? TopicId { get; set; }

    public int? UserId { get; set; }

    public int? Difficulty { get; set; }

    public string? Text { get; set; }

    public int? Type { get; set; }

   

    public virtual ICollection<CompetitionRoundQuestion> CompetitionRoundQuestions { get; set; } = new List<CompetitionRoundQuestion>();

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual Subject? SubjectCodeNavigation { get; set; }

    public virtual ICollection<TaskQuestion> TaskQuestions { get; set; } = new List<TaskQuestion>();

    public virtual Topic? Topic { get; set; }

    public virtual User? User { get; set; }
}
