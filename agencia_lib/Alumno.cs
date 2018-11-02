using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Alumno
    {
        public int Rut { get; set; }
        public string DigitoV { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public Curso Curso { get; set; }
        public Apoderado Apoderado { get; set; }
    }
}
