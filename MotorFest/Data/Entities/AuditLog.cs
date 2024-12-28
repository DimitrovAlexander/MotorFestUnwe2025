using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest;

public partial class AuditLog
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public string Action { get; set; } = null!;

    public int RecordId { get; set; }
    [Column("21180022_LastUpdate")]

    public DateTime LastUpdate { get; set; } =DateTime.Now;

    public string ChangedData { get; set; } = null!;
}
