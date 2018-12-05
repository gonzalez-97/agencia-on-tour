using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Actividad_Asociada
    {
        public int Id { get; set; }
        public Actividad Actividad { get; set; }
        public Curso Curso { get; set; }
        public int Total_Recaudado { get; set; }
        public int Prorrateo { get; set; }
    }
}
