using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_agencia.Models.Servicios
{
    public class SessionUser
    {
       public Usuario_Web SesionWeb
       {
             get
             {
                  return (Usuario_Web)HttpContext.Current.Session["sesion-user"];
             }
             set
             {
                  HttpContext.Current.Session["sesion-user"] = value;
             }
       }

       public Tarea_Terminada SesionTareaTerminada
       {
            get
            {
                return (Tarea_Terminada)HttpContext.Current.Session["sesion-termino-tarea"];
            }
            set
            {
                HttpContext.Current.Session["sesion-termino-tarea"] = value;
            }
        }
    }
}