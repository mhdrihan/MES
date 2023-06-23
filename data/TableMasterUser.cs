using System;
using System.Collections.Generic;

namespace MES.data;

public partial class TableMasterUser
{
    public int IdUser { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? StationId { get; set; }

    public int? UserLevel { get; set; }
}
