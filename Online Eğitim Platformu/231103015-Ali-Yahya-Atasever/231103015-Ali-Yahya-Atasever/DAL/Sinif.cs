using System;
using System.Collections.Generic;

namespace _231103015_Ali_Yahya_Atasever.DAL;

public partial class Sinif
{
    public int Id { get; set; }

    public int? Sinif1 { get; set; }

    public virtual ICollection<Ogrencibilgi> Ogrencibilgis { get; set; } = new List<Ogrencibilgi>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
