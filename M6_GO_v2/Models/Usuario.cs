using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace M6_GO_v2.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string? FotoPath { get; set; }

    public string Nome { get; set; } = null!;

    [JsonIgnore]
    public virtual Cuidador? Cuidador { get; set; }

    [JsonIgnore]
    public virtual Idoso? Idoso { get; set; }
}
