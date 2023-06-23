using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TableMasterProcess
{
    public int ProcessId { get; set; }

    public string ProcessName { get; set; } = null!;

    public string ProcessBy { get; set; } = null!;

    public string ProcessDescription { get; set; } = null!;

    public DateTime? LastModify { get; set; }

    public string? TransactBy { get; set; }
}
