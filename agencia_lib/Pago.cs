using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Pago
    {
        public int Id { get; set; }
        public Alumno Alumno { get; set; }
        public int Valor_Pago { get; set; }
        public int Total_Cuenta { get; set; }
        public DateTime Fecha_Pago { get; set; }
    }
}
