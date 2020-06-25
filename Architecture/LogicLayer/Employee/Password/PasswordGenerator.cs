using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar
{
    public class PasswordGenerator
    {
        public List<string> GeneratePasswords(int length, int count, bool includeSymbols)
        {
            List<string> pwds = new List<string>();
            StringBuilder pass;
            int remainder = length;
            string noSymbolsPass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string symbolsPass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890~!@#$%^&*()-+";
            if (includeSymbols != true)
            {
                Random rnd = new Random();
                while (count > 0)
                {
                    pass = new StringBuilder(length);
                    while (remainder > 0)
                    {
                        pass.Append(noSymbolsPass[rnd.Next(noSymbolsPass.Length)]);
                        remainder--;
                    }
                    remainder = length;
                    pwds.Add(pass.ToString());
                    count--;
                }
                return pwds;
            }
            else
            {
                Random rnd = new Random();
                while (count > 0)
                {
                    pass = new StringBuilder(length);
                    while (remainder > 0)
                    {
                        pass.Append(symbolsPass[rnd.Next(symbolsPass.Length)]);
                        remainder--;
                    }
                    remainder = length;
                    pwds.Add(pass.ToString());
                    count--;
                }
                return pwds;
            }
        }
    }
}
