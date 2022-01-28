using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace Paises2.Models
{
    public class Tokenize
    {

        public static string CreateToken(string email, string pass) 
        {
            string tok ="";
            SHA256 sHA256 = SHA256.Create($"{email}:CM1987+{pass}");
            Task.Run( () =>  nombremetodo());
            //PErsistir el sha en la base de datos para este usuario

            return sHA256.ToString();
        }

        public static bool ValidateToken(string token)
        {
            bool t = true;

            return t;
        
        }

        public static async Task nombremetodo() {
            string a = string.Empty;
            
        }

    }
}