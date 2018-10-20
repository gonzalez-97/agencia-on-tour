using agencia_lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using web_agencia.Models.Servicios;
using web_agencia.Models.Views;

namespace web_agencia.Models
{
    public class Usuario_Web : Usuario
    {
        public List<Mantenedor_Ruta> MantenedoresRutas { get; set; }

        HttpClient client;
        //The URL of the WEB API Service
        Uri url = new Uri("http://localhost:49868/api");

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter
        public Usuario_Web()
        {
            this.Lista_Perfiles = new List<Perfil>();

            client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> ExisteUsuarioAsync(UsuarioViewModel user)
        {
            try
            {
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(string.Format("{0}/{1}", url, "usuario/existe"), user);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    bool retorno = JsonConvert.DeserializeObject<bool>(responseData);
                    if (retorno) await CreateSesionAsync(user);
                    return retorno;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task CreateSesionAsync(UsuarioViewModel user)
        {
            if (await Read(user.Rut))
            {
                SessionUser user_sesion = new SessionUser();
                user_sesion.SesionWeb = this;
            }
        }

        public async Task<bool> Read(int rut)
        {
            try
            {
                HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}/{2}", url, "usuario/por-rut", rut));
                if (responseMessage.IsSuccessStatusCode)
                {
                    Colecciones col_web = new Colecciones();
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    Usuario_Web retorno = JsonConvert.DeserializeObject<Usuario_Web>(responseData);
                    retorno.MantenedoresRutas = col_web.ListaRutasMantenedores().Where(n =>
                                                        retorno.Lista_Perfiles.Select(m => m.Id).
                                                        Contains(n.IdPerfil)).ToList();

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

        public void MappingThisFromAnother(Usuario_Web userFrom)
        {
            this.Rut = userFrom.Rut;
            this.DigitoV = userFrom.DigitoV;
            this.Correo = userFrom.Correo;
            this.Nombre = userFrom.Nombre;
            this.APaterno = userFrom.APaterno;
            this.AMaterno = userFrom.AMaterno;
            this.Password = userFrom.Password;
            this.Lista_Perfiles = userFrom.Lista_Perfiles;
            this.MantenedoresRutas = userFrom.MantenedoresRutas;
        }

        public void MappingThisFromUsuarioViewModel(UsuarioViewModel user)
        {
            this.Rut = user.Rut;
            this.Nombre = user.Nombre;
            this.APaterno = user.APaterno;
            this.AMaterno = user.AMaterno;
            this.DigitoV = Dv(user.Rut.ToString());
            this.Password = user.Password;
            this.Correo = user.Correo;
            this.Lista_Perfiles = user.PerfilesElegidos.Select(n => new Perfil() { Id = int.Parse(n) }).ToList();
        }

        public async Task<bool> CreateFromViewAsync(UsuarioViewModel user)
        {
            MappingThisFromUsuarioViewModel(user);
            return await CreateThisAsync();
        }

        public async Task<bool> UpdateFromViewAsync(UsuarioViewModel user)
        {
            MappingThisFromUsuarioViewModel(user);
            return await UpdateThisAsync();
        }

        private string Dv(string r)
        {
            int suma = 0;
            for (int x = r.Length - 1; x >= 0; x--)
                suma += int.Parse(char.IsDigit(r[x]) ? r[x].ToString() : "0") * (((r.Length - (x + 1)) % 6) + 2);
            int numericDigito = (11 - suma % 11);
            string digito = numericDigito == 11 ? "0" : numericDigito == 10 ? "K" : numericDigito.ToString();
            return digito;
        }

        private async Task<bool> CreateThisAsync()
        {
            try
            {
                bool salida = false;

                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(string.Format("{0}/{1}", url, "usuario/crear"), this);
                if (responseMessage.IsSuccessStatusCode) salida = await AsociaPerfilesUsuarioAsync();

                return salida;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private async Task<bool> UpdateThisAsync()
        {
            try
            {
                bool salida = false;

                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(string.Format("{0}/{1}", url, "usuario/actualizar"), this);
                if (responseMessage.IsSuccessStatusCode)
                {
                    salida = await EliminarTodosPerfiles();
                    if (salida) salida = await AsociaPerfilesUsuarioAsync();
                }  
                return salida;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> DeleteThisAsync()
        {
            try
            {
                HttpResponseMessage responseMessage = await client.DeleteAsync(string.Format("{0}/{1}/{2}", url, "usuario/borrar", this.Rut));
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> AsociaPerfilesUsuarioAsync()
        {
            try
            {
                foreach (var item in this.Lista_Perfiles)
                {

                    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(
                                                            string.Format("{0}/{1}", url, "perfil/asocia-perfil"),
                                                            new { Perfil = item.Id, Rut = this.Rut });

                    if (!responseMessage.IsSuccessStatusCode) return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> EliminarTodosPerfiles()
        {
            try
            {
                 HttpResponseMessage responseMessage = await client.DeleteAsync(string.Format("{0}/{1}/{2}", url, "perfil/elimina-todo-perfil", this.Rut));
                 return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

    }
}