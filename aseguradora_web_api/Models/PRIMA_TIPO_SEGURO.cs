//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace aseguradora_web_api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRIMA_TIPO_SEGURO
    {
        public int ID_TIPO { get; set; }
        public string NOMBRE_TIPO { get; set; }
        public int VALOR_PRIMA_INDIVIDUAL { get; set; }
        public Nullable<double> PORC_AUMENTO_DIA { get; set; }
    }
}
