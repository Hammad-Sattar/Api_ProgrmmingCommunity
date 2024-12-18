using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class StudentSubject
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public string? SubjectCode { get; set; }

    public virtual User? Student { get; set; }

    public virtual Subject? SubjectCodeNavigation { get; set; }
}
