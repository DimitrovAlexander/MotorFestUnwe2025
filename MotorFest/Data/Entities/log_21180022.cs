using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest;

public partial class log_21180022
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public string OperationType { get; set; } = null!;

    public DateTime OperationDateTime { get; set; }

    [Column("21180022_LastUpdate")]

    public DateTime LastUpdate { get; set; } =DateTime.Now;

}
