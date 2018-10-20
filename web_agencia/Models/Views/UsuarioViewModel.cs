using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using agencia_lib;

namespace web_agencia.Models.Views
{
    public class UsuarioViewModel : Usuario
    {
        public List<SelectListItem> PerfilesDisponibles { get; set; }
        public List<string> PerfilesElegidos { get; set; }

        public UsuarioViewModel()
        {
            this.PerfilesDisponibles = new List<SelectListItem>();
            this.PerfilesElegidos = new List<string>();
        }
    }
}