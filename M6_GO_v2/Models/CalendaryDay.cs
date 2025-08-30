using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M6_GO_v2.Models
{
    public class CalendaryDay
    {
        public DateTime Data { get; set; }
        public bool MesAtual { get; set; }
        public bool TemAtendimento { get; set; }
        public string Day => Data.Day.ToString();
        public TipoAtendimeto Tipo { get; set; }
        public Color CorFundo { get; set; }
        public Color CorTexto { get; set; }
    }

    public enum TipoAtendimeto 
    {
        Passada,Presente,Futura
    }
}
