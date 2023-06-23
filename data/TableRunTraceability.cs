using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TableRunTraceability
{
    public string SerialNumber { get; set; } = null!;

    public string BatchId { get; set; } = null!;

    public string RefferenceName { get; set; } = null!;

    public string WorkOrder { get; set; } = null!;

    public int StationId { get; set; }

    public int StationSuffix { get; set; }

    public string StationName { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string CavityNumber { get; set; } = null!;

    public DateTime? TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public string StatusResult { get; set; } = null!;

    public string StatusRunning { get; set; } = null!;

    public string? TransactBy { get; set; }

    public string? FullRefference { get; set; }
}
