using System;
using System.Collections.Generic;

namespace _231103015_Ali_Yahya_Atasever.DAL;

public partial class Ogrencibilgi
{
    public int Id { get; set; }

    public int? OkulNo { get; set; }

    public string? Ad { get; set; }

    public string? Soyad { get; set; }

    public int? BolumId { get; set; }

    public int? SinifId { get; set; }

    public virtual Bolum? Bolum { get; set; }

    public virtual ICollection<Izlenme> Izlenmes { get; set; } = new List<Izlenme>();

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual Sinif? Sinif { get; set; }
}
