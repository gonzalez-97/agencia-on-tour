using agencia_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using web_agencia.Models.Servicios;

namespace web_agencia.Models
{
    public class Alumno_Web : Alumno
    {
        HttpClient client;
        //The URL of the WEB API Service
        Uri url = new Uri(Utiles.RutaWebAPI());

        public Alumno_Web()
        {
            client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



    }
}