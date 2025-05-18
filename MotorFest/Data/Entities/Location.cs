using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest;

public partial class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; } = null!;

    public string FullAddress { get; set; } = null!;

    public string Country { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
    [Column("21180022_LastUpdate")]
    public DateTime LastUpdate { get; set; } =DateTime.Now;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
