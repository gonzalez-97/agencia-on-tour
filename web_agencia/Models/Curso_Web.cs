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
    public class Curso_Web: Curso
    {
        HttpClient client;
        Uri url = new Uri(Utiles.RutaWebAPI());
        public Dictionary<string, string> _dictionaryError { get; set; }

        public Curso_Web()
        {
            client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> CreateFromViewAsync(CursoViewModel curso)
        {
            MappingThisFromViewModel(curso);
            return await Create();
        }

        public async Task<bool> UpdateFromViewAsync(CursoViewModel curso)
        {
            MappingThisFromViewModel(curso);
            return await Update();
        }

        public async Task<bool> Create()
        {
            try
            {
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(string.Format("{0}/{1}", url, "curso/crear"), this);
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
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}/{2}", url, "curso", id));
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    Curso_Web retorno = JsonConvert.DeserializeObject<Curso_Web>(responseData);

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
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(string.Format("{0}/{1}", url, "curso/actualizar"), this);
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
                HttpResponseMessage responseMessage = await client.DeleteAsync(string.Format("{0}/{1}/{2}", url, "curso/borrar", this.Id));
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void MappingThisFromAnother(Curso_Web objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.TotalReunido = objeto.TotalReunido;
            this.Colegio = objeto.Colegio;
        }

        private void MappingThisFromViewModel(CursoViewModel objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.TotalReunido = objeto.TotalReunido;
            this.Colegio = objeto.Colegio;
        }
        internal bool ValidarCursoViewModel(CursoViewModel curso, bool v)
        {
            Usuario_Web uw = new Usuario_Web();

            _dictionaryError = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(curso.Nombre) || string.IsNullOrWhiteSpace(curso.Nombre))
            {
                _dictionaryError.Add("Nombre", "Este campo es obligatorio.");
            }

            if (curso.TotalReunido == 0)
            {
                _dictionaryError.Add("TotalReunido", "Debe ingresar un monto.");
            }
            return _dictionaryError.Count == 0;
        }
    }
}