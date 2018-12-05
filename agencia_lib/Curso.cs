using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? TotalReunido { get; set; }
        public int? TotalPagar { get; set; }
        public Colegio Colegio { get; set; }

        public Curso()
        {
            this.Id = 0;
            this.Nombre = string.Empty;
            this.Colegio = new Colegio();
        }
    }
}
