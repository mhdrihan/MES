using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TblDaycount
{
    public string? StationId { get; set; }

    public string? CountDate { get; set; }

    public int? ValCount { get; set; }

    public int? FailCount { get; set; }
}
