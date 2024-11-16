using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class ExpertSubject
{
    public int Id { get; set; }

    public int? ExpertId { get; set; }

    public int? SubjectCode { get; set; }

    public virtual User? Expert { get; set; }

    public virtual Subject? SubjectCodeNavigation { get; set; }
}
