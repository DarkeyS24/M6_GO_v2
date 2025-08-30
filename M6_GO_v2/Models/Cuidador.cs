using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace M6_GO_v2.Models;

public partial class Cuidador
{
    public int Id { get; set; }

    public string? Telefone { get; set; }

    [JsonIgnore]
    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    public virtual Usuario IdNavigation { get; set; } = null!;
}
