using agencia_lib;
using System.Collections.Generic;
using System.Web.Mvc;

namespace web_agencia.Models.Views
{
    public class ActividadAsociadaViewModel : Actividad_Asociada
    {
        public List<SelectListItem> CursosDisponibles { get; set; }
        public List<SelectListItem> ActividadesDisponibles { get; set; }

        public ActividadAsociadaViewModel()
        {
            CursosDisponibles = new List<SelectListItem>();
            ActividadesDisponibles = new List<SelectListItem>();
        }
    }
}