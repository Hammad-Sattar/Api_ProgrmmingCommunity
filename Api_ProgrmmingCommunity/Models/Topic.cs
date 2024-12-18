using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class Topic
{
    public int Id { get; set; }

    public string? SubjectCode { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<ExpertTopic> ExpertTopics { get; set; } = new List<ExpertTopic>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Subject? SubjectCodeNavigation { get; set; }
}
