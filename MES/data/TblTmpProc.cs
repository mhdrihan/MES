using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TblTmpProc
{
    public string? StationId { get; set; }

    public int? TmpPass { get; set; }

    public int? TmpFail { get; set; }

    public int? TmpTotalCount { get; set; }

    public int? TmpValFail { get; set; }
}
