using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aseguradora_web_api.Models
{
    public class ConexionDB
    {
        private static Aseguradora_Entidades _instancia;

        public static Aseguradora_Entidades Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new Aseguradora_Entidades();
                return _instancia;
            }
            set { _instancia = value; }
        }

    }
}