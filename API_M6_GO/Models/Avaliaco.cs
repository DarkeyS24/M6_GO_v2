using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_M6_GO.Models;

public partial class Avaliaco
{
    public int Id { get; set; }

    public int? Avaliacao { get; set; }

    [JsonIgnore]
    public virtual Atendimento IdNavigation { get; set; } = null!;
}
