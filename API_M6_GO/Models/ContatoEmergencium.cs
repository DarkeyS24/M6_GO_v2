using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_M6_GO.Models;

public partial class ContatoEmergencium
{
    public int Id { get; set; }

    public int IdosoId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Telefone { get; set; }

    [JsonIgnore]
    public virtual Idoso Idoso { get; set; } = null!;
}
