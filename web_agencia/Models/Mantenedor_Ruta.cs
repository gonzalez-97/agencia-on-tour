using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_agencia.Models
{
    public class Mantenedor_Ruta
    {
        public int IdPerfil { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public Mantenedor_Ruta()
        {
            this.IdPerfil = 0;
            this.ControllerName = "Home";
            this.ActionName = "Index";
        }
    }
}