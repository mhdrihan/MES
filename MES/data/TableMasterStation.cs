using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TableMasterStation
{
    public int Id { get; set; }

    public int? StationId { get; set; }

    public int? StationSuffix { get; set; }

    public string? StationName { get; set; }

    public int? ProcessId { get; set; }

    public int? LineId { get; set; }

    public int? TargetOutput { get; set; }

    public int? TargetYield { get; set; }

    public DateTime? LastModify { get; set; }

    public string? TransactBy { get; set; }

    public string? TesterName { get; set; }
}
