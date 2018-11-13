using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Contrato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha_Viaje { get; set; }
        public int Valor { get; set; }
        public Curso Curso { get; set; }
        public bool Estado { get; set; }
        public List<Seguro_Asociado> ListaSeguroAsociados {get; set;}
        public List<Servicio_Asociado> ListaServiciosAsociados { get; set; }
        public List<Destino_Asociado> ListaDestinosAsociados { get; set; }
        public List<Archivo> ListaArchivos { get; set; }

    }

}
