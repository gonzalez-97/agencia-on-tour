using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_agencia.Models
{
    public class Tarea_Terminada
    {
        public string LayoutNombre { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public string LinkTexto { get; set; }
    }
}