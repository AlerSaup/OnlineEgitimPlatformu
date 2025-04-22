using System;
using System.Collections.Generic;

namespace _231103015_Ali_Yahya_Atasever.DAL;

public partial class Rank
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
}
