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
using web_agencia.Models.Views;

namespace web_agencia.Models
{
    public class Actividad_Asociada_Web : Actividad_Asociada
    {
        HttpClient client;
        //The URL of the WEB API Service
        Uri url = new Uri(Utiles.RutaWebAPI());
        public Dictionary<string, string> _dictionaryError { get; set; }
        public Actividad_Asociada_Web()
        {
            client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> CreateFromViewModel(ActividadAsociadaViewModel actividad)
        {
            MappingThisFromViewModel(actividad);
            return await Create();
        }

        public async Task<bool> Create()
        {
            try
            {
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(string.Format("{0}/{1}", url, "actividad-asociada/crear"), this);
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
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}/{2}", url, "actividad-asociada", id));
                if (responseMessage.IsSuccessStatusCode)
                {

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    Actividad_Asociada_Web retorno = JsonConvert.DeserializeObject<Actividad_Asociada_Web>(responseData);

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

        public async Task<bool> Delete()
        {
            try
            {
                HttpResponseMessage responseMessage = await client.DeleteAsync(string.Format("{0}/{1}/{2}", url, "actividad-asociada/borrar", this.Id));
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private void MappingThisFromAnother(Actividad_Asociada_Web retorno)
        {
            this.Id = retorno.Id;
            this.Actividad = retorno.Actividad;
            this.Curso = retorno.Curso;
            this.Total_Recaudado = retorno.Total_Recaudado;
            this.Prorrateo = retorno.Prorrateo;
        }

        private void MappingThisFromViewModel(ActividadAsociadaViewModel retorno)
        { 
            this.Actividad = retorno.Actividad;
            this.Curso = retorno.Curso;
            this.Total_Recaudado = retorno.Total_Recaudado;
            this.Prorrateo = retorno.Prorrateo;
        }
    }
}