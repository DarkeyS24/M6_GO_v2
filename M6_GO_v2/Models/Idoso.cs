using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace M6_GO_v2.Models;

public partial class Idoso
{
    public int Id { get; set; }

    public DateOnly Nascimento { get; set; }

    public decimal? PesoKg { get; set; }

    public string? Observacoes { get; set; }

    public string? Resumo { get; set; }

    [JsonIgnore]
    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    public virtual ICollection<ContatoEmergencium> ContatoEmergencia { get; set; } = new List<ContatoEmergencium>();

    public virtual Usuario IdNavigation { get; set; } = null!;
}
