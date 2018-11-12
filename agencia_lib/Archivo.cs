using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Archivo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Contrato Contrato { get; set; }
    }
}
