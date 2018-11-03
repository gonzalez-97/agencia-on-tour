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

        public async Task<bool> ExisteAlumnoAsync(int rut)
        {
            try
            {
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}/{2}", url, "alumno/existe-por-rut", rut));
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<bool>(responseData);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Create()
        {
            try
            {
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(string.Format("{0}/{1}", url, "alumno/crear"), this);
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> ReadPorRut(int rut)
        {
            try
            {
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}/{2}", url, "alumno/por-rut", rut));
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    Alumno_Web retorno = JsonConvert.DeserializeObject<Alumno_Web>(responseData);
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
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(string.Format("{0}/{1}", url, "alumno/actualizar"), this);
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
                HttpResponseMessage responseMessage = await client.DeleteAsync(string.Format("{0}/{1}/{2}", url, "alumno/borrar", this.Rut));
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private void MappingThisFromAnother(Alumno_Web objeto)
        {
            this.Rut = objeto.Rut;
            this.DigitoV = objeto.DigitoV;
            this.Nombre = objeto.Nombre;
            this.APaterno = objeto.APaterno;
            this.AMaterno = objeto.AMaterno;
            this.Apoderado = objeto.Apoderado;
            this.Curso = objeto.Curso;
        }
    }
}