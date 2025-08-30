using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_M6_GO.Models;

public partial class Procedimento
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
}
