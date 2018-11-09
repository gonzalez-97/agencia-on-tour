using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Prima_Seguro
    {
        public int Id_Tipo { get; set; }
        public string Nombre_Tipo { get; set; }
        public int Valor_Prima_Individual { get; set; }
        public double? Porc_Aumento_Dia { get; set; }
    }
}
