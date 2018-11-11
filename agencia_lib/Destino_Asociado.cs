using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Destino_Asociado
    {
        public int Id { get; set; }
        public Destino Destino { get; set; }
        public Contrato Contrato { get; set; }
    }
}
