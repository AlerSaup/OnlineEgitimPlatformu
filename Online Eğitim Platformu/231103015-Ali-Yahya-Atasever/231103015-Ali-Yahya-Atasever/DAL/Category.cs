using System;
using System.Collections.Generic;

namespace _231103015_Ali_Yahya_Atasever.DAL;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? BolumId { get; set; }

    public virtual Bolum? Bolum { get; set; }

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
