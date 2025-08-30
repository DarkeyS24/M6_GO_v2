using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace M6_GO_v2.Models;

public partial class Procedimento
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
}
