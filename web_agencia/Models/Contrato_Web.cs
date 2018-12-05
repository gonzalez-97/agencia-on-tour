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
    public class Contrato_Web : Contrato
    {
        HttpClient client;
        //The URL of the WEB API Service
        Uri url = new Uri(Utiles.RutaWebAPI());

        public Contrato_Web()
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
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(string.Format("{0}/{1}", url, "contrato/crear"), this);
                if (responseMessage.IsSuccessStatusCode)
                {

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    Contrato_Web retorno = JsonConvert.DeserializeObject<Contrato_Web>(responseData);
                    this.Id = retorno.Id;
                }
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
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}/{2}", url, "contrato", id));
                if (responseMessage.IsSuccessStatusCode)
                {

                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    Contrato_Web retorno = JsonConvert.DeserializeObject<Contrato_Web>(responseData);

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
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(string.Format("{0}/{1}", url, "contrato/actualizar"), this);
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
                HttpResponseMessage responseMessage = await client.DeleteAsync(string.Format("{0}/{1}/{2}", url, "contrato/borrar", this.Id));
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void MappingThisFromAnother(Contrato_Web objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.Descripcion = objeto.Descripcion;
            this.Fecha_Viaje = objeto.Fecha_Viaje;
            this.Valor = objeto.Valor;
            this.Curso = objeto.Curso;
            this.Estado = objeto.Estado;
            this.ListaSeguroAsociados = objeto.ListaSeguroAsociados;
            this.ListaServiciosAsociados = objeto.ListaServiciosAsociados;
            this.ListaDestinosAsociados = objeto.ListaDestinosAsociados;
            this.ListaArchivos = objeto.ListaArchivos;
        }
    }
}