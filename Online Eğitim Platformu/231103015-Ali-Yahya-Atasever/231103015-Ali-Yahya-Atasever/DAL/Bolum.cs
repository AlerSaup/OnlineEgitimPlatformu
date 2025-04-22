using System;
using System.Collections.Generic;

namespace _231103015_Ali_Yahya_Atasever.DAL;

public partial class Bolum
{
    public int Id { get; set; }

    public string? BolumAdi { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Ogrencibilgi> Ogrencibilgis { get; set; } = new List<Ogrencibilgi>();
}
