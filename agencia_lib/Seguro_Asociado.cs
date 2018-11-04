using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Seguro_Asociado
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public int Tipo_Seguro { get; set; }
        public Seguro Seguro { get; set; }
        public Contrato Contrato { get; set; }
    }
}
