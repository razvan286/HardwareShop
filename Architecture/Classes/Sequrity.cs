using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Media_Bazaar.Classes
{
    //DO NOT MODIFY!!!
    //Hashing passwords for the login portal 
    public class Sequrity
    {
        public static String Hashing(string password)
        {
            byte[] solution = ASCIIEncoding.ASCII.GetBytes(password);
            SHA1CryptoServiceProvider crypto = new SHA1CryptoServiceProvider();

            byte[] output = crypto.ComputeHash(solution);

            return ASCIIEncoding.ASCII.GetString(output);
        }
    }
}
