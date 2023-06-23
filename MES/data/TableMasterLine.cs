using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TableMasterLine
{
    public int LineId { get; set; }

    public string LineName { get; set; } = null!;

    public string LineLocation { get; set; } = null!;

    public string LineDescription { get; set; } = null!;

    public DateTime? LastModify { get; set; }

    public string? TransactBy { get; set; }
}
