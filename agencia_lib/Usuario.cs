using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace agencia_lib
{
    public class Usuario
    {
        public int Rut { get; set; }
        public string DigitoV { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set;  }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public List<Perfil> Lista_Perfiles { get; set; }


        public string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
    }
}
