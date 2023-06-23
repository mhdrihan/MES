using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TableMasterBatch
{
    public string BatchId { get; set; } = null!;

    public string? WorkOrder { get; set; }

    public string? RefferenceName { get; set; }
}
