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
    public class Pago_Web: Pago
    {
        HttpClient client;
        //The URL of the WEB API Service
        Uri url = new Uri(Utiles.RutaWebAPI());

        public Pago_Web()
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
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(string.Format("{0}/{1}", url, "pago/crear"), this);
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> CreateFromViewAsync(PagoViewModel pago)
        {
            MappingThisFromViewModel(pago);
            return await Create();
        }

        public async Task<bool> UpdateFromViewAsync(PagoViewModel pago)
        {
            MappingThisFromViewModel(pago);
            return await Update();
        }

        private void MappingThisFromViewModel(PagoViewModel curso)
        {
            this.Id = curso.Id;
            this.Alumno = curso.Alumno;
            this.Total_Cuenta = curso.Total_Cuenta;
            this.Valor_Pago = curso.Valor_Pago;
            this.Fecha_Pago = curso.Fecha_Pago;
        }

        public async Task<bool> Read(int id)
        {
            try
            {
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}/{2}", url, "pago", id));
                if (responseMessage.IsSuccessStatusCode)
                {

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    Pago_Web retorno = JsonConvert.DeserializeObject<Pago_Web>(responseData);

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
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(string.Format("{0}/{1}", url, "pago/actualizar"), this);
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
                HttpResponseMessage responseMessage = await client.DeleteAsync(string.Format("{0}/{1}/{2}", url, "pago/borrar", this.Id));
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private void MappingThisFromAnother(Pago_Web retorno)
        {
            this.Id = retorno.Id;
            this.Alumno = retorno.Alumno;
            this.Total_Cuenta = retorno.Total_Cuenta;
            this.Valor_Pago = retorno.Valor_Pago;
            this.Fecha_Pago = retorno.Fecha_Pago;
        }
    }
}