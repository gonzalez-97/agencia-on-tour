using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
   public  class Pago_Actividad
   {
        public int Id { get; set; }
        public Pago Pago { get; set; }
        public Actividad_Asociada Actividad_Asignada { get; set; }
   }
}
