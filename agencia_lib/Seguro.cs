﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Seguro
    {
        public int Id { get; set; }
        public int Id_Tipo_Seguro { get; set; }
        public string Nombre { get; set; }
        public double Valor_Uf { get; set; }
        public string Poliza_Descripcion { get; set; }
    }
}
