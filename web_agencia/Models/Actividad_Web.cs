using agencia_lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using web_agencia.Models.Servicios;

namespace web_agencia.Models
{
    public class Actividad_Web: Actividad
    {
        HttpClient client;
        //The URL of the WEB API Service
        Uri url = new Uri(Utiles.RutaWebAPI());
        public Dictionary<string, string> _dictionaryError { get; set; }
        public Actividad_Web()
        {
            client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> Create()
        {
            try
            {
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(string.Format("{0}/{1}", url, "actividad/crear"), this);
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> Read(int id)
        {
            try
            {
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}/{2}", url, "actividad", id));
                if (responseMessage.IsSuccessStatusCode)
                {

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    Actividad_Web retorno = JsonConvert.DeserializeObject<Actividad_Web>(responseData);

                    MappingThisFromAnother(retorno);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public async Task<bool> Update()
        {
            try
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(string.Format("{0}/{1}", url, "actividad/actualizar"), this);
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> Delete()
        {
            try
            {
                HttpResponseMessage responseMessage = await client.DeleteAsync(string.Format("{0}/{1}/{2}", url, "actividad/borrar", this.Id));
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void MappingThisFromAnother(Actividad_Web objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.Descripcion = objeto.Descripcion;
        }

        internal bool ValidarActividad(Actividad_Web actividad, bool esCreacion)
        {
            _dictionaryError = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(actividad.Nombre) || string.IsNullOrWhiteSpace(actividad.Nombre))
            {
                _dictionaryError.Add("Nombre", "Este campo es obligatorio.");
            }

            if (string.IsNullOrEmpty(actividad.Descripcion) || string.IsNullOrWhiteSpace(actividad.Descripcion))
            {
                _dictionaryError.Add("Descripcion", "Este campo es obligatorio.");
            }
            return _dictionaryError.Count() == 0;
        }
    }
}