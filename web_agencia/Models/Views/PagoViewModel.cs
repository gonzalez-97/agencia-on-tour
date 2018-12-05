using agencia_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web_agencia.Models.Views
{
    public class PagoViewModel : Pago
    {
        public List<SelectListItem> MisAlumnos { get; set; }
        public PagoViewModel()
        {
            this.MisAlumnos = new List<SelectListItem>();
        }
    }
}