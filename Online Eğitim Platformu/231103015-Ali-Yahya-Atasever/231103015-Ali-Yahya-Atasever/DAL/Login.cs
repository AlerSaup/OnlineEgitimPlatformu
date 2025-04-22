using System;
using System.Collections.Generic;

namespace _231103015_Ali_Yahya_Atasever.DAL;

public partial class Login
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? OgrencibilgiId { get; set; }

    public int? RankId { get; set; }

    public virtual Ogrencibilgi? Ogrencibilgi { get; set; }

    public virtual Rank? Rank { get; set; }
}
