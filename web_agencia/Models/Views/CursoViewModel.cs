using agencia_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web_agencia.Models.Views
{
    public class CursoViewModel: Curso
    {
        public List<SelectListItem> ColegiosDisponibles { get; set; }
        public CursoViewModel()
        {
            this.ColegiosDisponibles = new List<SelectListItem>();
        }
    }
}