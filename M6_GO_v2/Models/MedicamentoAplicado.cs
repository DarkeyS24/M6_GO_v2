using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace M6_GO_v2.Models;

public partial class MedicamentoAplicado
{
    public int Id { get; set; }

    public int? AtendimentoId { get; set; }

    public int MedicamentoId { get; set; }

    public string? Dose { get; set; }

    public DateTime DataHora { get; set; }

    public string? Observacoes { get; set; }

    [JsonIgnore]
    public virtual Atendimento? Atendimento { get; set; }

    public virtual Medicamento Medicamento { get; set; } = null!;
}
