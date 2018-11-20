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
    public class Tipo_Seguro_Web : Tipo_Seguro
    {
        HttpClient client;
        Uri url = new Uri(Utiles.RutaWebAPI());

        public Tipo_Seguro_Web()
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
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(string.Format("{0}/{1}", url, "seguro/crear"), this);
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
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}/{2}", url, "seguro", id));
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    Tipo_Seguro_Web retorno = JsonConvert.DeserializeObject<Tipo_Seguro_Web>(responseData);

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

        private async Task<bool> Update()
        {
            try
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(string.Format("{0}/{1}", url, "seguro/actualizar"), this);
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
                HttpResponseMessage responseMessage = await client.DeleteAsync(string.Format("{0}/{1}/{2}", url, "seguro/borrar", this.Id));
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private void MappingThisFromAnother(Tipo_Seguro_Web objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.Tipo_Aseguradora = objeto.Tipo_Aseguradora;
        }
    }
}