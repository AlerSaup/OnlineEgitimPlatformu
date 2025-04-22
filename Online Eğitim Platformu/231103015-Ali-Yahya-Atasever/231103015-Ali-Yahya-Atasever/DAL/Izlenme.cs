using System;
using System.Collections.Generic;

namespace _231103015_Ali_Yahya_Atasever.DAL;

public partial class Izlenme
{
    public int Id { get; set; }

    public int? OgrencibilgiId { get; set; }

    public int? VideoId { get; set; }

    public int? Izlenmemiktari { get; set; }

    public virtual Ogrencibilgi? Ogrencibilgi { get; set; }

    public virtual Video? Video { get; set; }
}
