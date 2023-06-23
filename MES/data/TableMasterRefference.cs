using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TableMasterRefference
{
    public int RefferenceId { get; set; }

    public string RefferenceName { get; set; } = null!;

    public string RefferenceDescription { get; set; } = null!;

    public DateTime? LastModify { get; set; }

    public string? TransactBy { get; set; }
}
