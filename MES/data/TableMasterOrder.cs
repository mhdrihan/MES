using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TableMasterOrder
{
    public string WorkOrder { get; set; } = null!;

    public string RefferenceName { get; set; } = null!;

    public string WorkPlan { get; set; } = null!;

    public int QtyOrder { get; set; }

    public int? QtyLaunching { get; set; }

    public string? StatusOrder { get; set; }

    public DateTime? DateOrder { get; set; }

    public DateTime? DateComplete { get; set; }

    public string? TransactBy { get; set; }

    public string? Username { get; set; }

    public string? WoComment { get; set; }

    public int? PriorityWo { get; set; }

    public int? StationId { get; set; }

    public int? StationSuffix { get; set; }
}
