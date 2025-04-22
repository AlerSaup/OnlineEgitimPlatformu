using System;
using System.Collections.Generic;

namespace _231103015_Ali_Yahya_Atasever.DAL;

public partial class Video
{
    public int Id { get; set; }

    public string? Videolink { get; set; }

    public int? CategoryId { get; set; }

    public string? Videotitle { get; set; }

    public int? SinifId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Izlenme> Izlenmes { get; set; } = new List<Izlenme>();

    public virtual Sinif? Sinif { get; set; }
}
