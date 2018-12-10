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
        Uri url = new Uri(Utiles.RutaWebAPI());

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

        public async Task<List<Seguro>> ListaSegurosAseguradora()
        {
            List<Seguro> salida = new List<Seguro>();

            Uri urlAseguradora = new Uri(Utiles.RutaWebAPI_Seguro());
            HttpClient clientAseguradora = new HttpClient();
            clientAseguradora.BaseAddress = urlAseguradora;
            clientAseguradora.DefaultRequestHeaders.Accept.Clear();
            clientAseguradora.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = await clientAseguradora.GetAsync(string.Format("{0}/{1}", urlAseguradora, "seguros"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Seguro>>(responseData);
            }
            return salida;
        }

        public List<Mantenedor_Ruta> ListaRutasMantenedores()
        {
            List<Mantenedor_Ruta> salida = new List<Mantenedor_Ruta>();
            /*Rutas en duro para redireccion de 5 perfiles */
            salida.Add(new Mantenedor_Ruta { IdPerfil = 1, ControllerName = "Administrador", ActionName = "Index" });
            salida.Add(new Mantenedor_Ruta { IdPerfil = 2, ControllerName = "Ejecutivo", ActionName = "Index" });
            salida.Add(new Mantenedor_Ruta { IdPerfil = 5, ControllerName = "Apoderado", ActionName = "Index" });
            salida.Add(new Mantenedor_Ruta { IdPerfil = 3, ControllerName = "Duenio", ActionName = "Index" });
            salida.Add(new Mantenedor_Ruta { IdPerfil = 4, ControllerName = "Encargado", ActionName = "Index" });
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

        public async Task<List<Destino>> ListaDestinos()
        {
            List<Destino> salida = new List<Destino>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "destino"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Destino>>(responseData);
            }
            return salida;
        }

        public async Task<List<Colegio>> ListaColegios()
        {
            List<Colegio> salida = new List<Colegio>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "colegio"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Colegio>>(responseData);
            }
            return salida;
        }

        public async Task<List<Curso>> ListaCursos()
        {
            List<Curso> salida = new List<Curso>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "curso"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Curso>>(responseData);
            }
            return salida;
        }

        public async Task<List<Apoderado>> ListaApoderados()
        {
            List<Apoderado> salida = new List<Apoderado>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "apoderado"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Apoderado>>(responseData);
            }
            return salida;
        }

        public async Task<List<Alumno>> ListaAlumnos()
        {
            List<Alumno> salida = new List<Alumno>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "alumno"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Alumno>>(responseData);
            }
            return salida;
        }

        public async Task<List<Tipo_Seguro>> ListaTiposSeguros()
        {
            List<Tipo_Seguro> salida = new List<Tipo_Seguro>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "seguro"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Tipo_Seguro>>(responseData);
            }
            return salida;
        }

        public async Task<List<Servicio>> ListaServicios()
        {
            List<Servicio> salida = new List<Servicio>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "servicio"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Servicio>>(responseData);
            }
            return salida;
        }

        public async Task<List<Contrato>> ListaContratos()
        {
            List<Contrato> salida = new List<Contrato>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "contrato"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Contrato>>(responseData);
            }
            return salida;
        }

        public async Task<List<Archivo>> ListaArchivos()
        {
            List<Archivo> salida = new List<Archivo>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "archivo"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Archivo>>(responseData);
            }
            return salida;
        }

        public async Task<List<Pago>> ListaPagos()
        {
            List<Pago> salida = new List<Pago>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "pago"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Pago>>(responseData);
            }
            return salida;
        }

        public async Task<List<Actividad_Asociada>> ListaActividadesAsociadas()
        {
            List<Actividad_Asociada> salida = new List<Actividad_Asociada>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "actividad-asociada"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Actividad_Asociada>>(responseData);
            }
            return salida;
        }

        public async Task<List<Pago_Actividad>> ListaPagosActividad()
        {
            List<Pago_Actividad> salida = new List<Pago_Actividad>();
            HttpResponseMessage responseMessage = await client.GetAsync(string.Format("{0}/{1}", url, "pago-actividad"));
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                salida = JsonConvert.DeserializeObject<List<Pago_Actividad>>(responseData);
            }
            return salida;
        }
    }
}