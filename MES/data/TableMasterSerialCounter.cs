using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TableMasterSerialCounter
{
    public int? StationId { get; set; }

    public int? StationSuffix { get; set; }

    public int? YearCode { get; set; }

    public int? WeekCode { get; set; }

    public int? CounterCode { get; set; }
}
