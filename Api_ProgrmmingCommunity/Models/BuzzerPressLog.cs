using System;
using System.Collections.Generic;

namespace Api_ProgrmmingCommunity.Models;

public partial class BuzzerPressLog
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public int TeamId { get; set; }

    public string? TeamName { get; set; }

    public DateTime PressTime { get; set; }

    public string? PressTimeFormatted { get; set; }
}
