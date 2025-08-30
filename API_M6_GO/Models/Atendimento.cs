using System;
using System.Collections.Generic;

namespace API_M6_GO.Models;

public partial class Atendimento
{
    public int Id { get; set; }

    public int IdosoId { get; set; }

    public int? CuidadorId { get; set; }

    public DateTime DataHora { get; set; }

    public int ProcedimentoId { get; set; }

    public string? Observacoes { get; set; }

    public int StatusId { get; set; }

    public virtual Avaliaco? Avaliaco { get; set; }

    public virtual Cuidador? Cuidador { get; set; }

    public virtual Idoso Idoso { get; set; } = null!;

    public virtual ICollection<MedicamentoAplicado> MedicamentoAplicados { get; set; } = new List<MedicamentoAplicado>();

    public virtual Procedimento Procedimento { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
