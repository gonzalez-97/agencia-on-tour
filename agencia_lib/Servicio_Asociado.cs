using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Servicio_Asociado
    {
        public int Id { get; set; }
        public Contrato Contrato { get; set; }
        public Servicio Servicio { get; set; }
    }
}
