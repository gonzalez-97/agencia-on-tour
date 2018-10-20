using agencia_lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace web_agencia.Models.Servicios
{
    public class Colecciones
    {
        HttpClient client;
        //The URL of the WEB API Service
        Uri url = new Uri("http://localhost:49868/api");

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public Colecciones()
        {
            client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Mantenedor_Ruta> ListaRutasMantenedores()
        {
            List<Mantenedor_Ruta> salida = new List<Mantenedor_Ruta>();
            salida.Add(new Mantenedor_Ruta { IdPerfil = 1, ControllerName = "Administrador", ActionName = "Index" });
            return salida;
        }

        public async Task<List<Usuario_Web>> ListaUsuariosAsync()
        {
            List<Usuario_Web> salida = new List<Usuario_Web>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "usuario"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Usuario_Web>>(responseData);
            } 
            return salida;
        }

        public async Task<List<Perfil>> ListaPerfilesAsync()
        {
            List<Perfil> salida = new List<Perfil>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "perfil"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Perfil>>(responseData);
            }
            return salida;
        }

        public async Task<List<Actividad>> ListaActividades()
        {
            List<Actividad> salida = new List<Actividad>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "actividad"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Actividad>>(responseData);
            }
            return salida;
        }
    }
}