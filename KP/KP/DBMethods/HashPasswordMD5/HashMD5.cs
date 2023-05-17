using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace KP.DBMethods.HashPasswordMD5
{
    internal class HashMD5
    {
        public static string HashPasswordWithMD5(string _password)
        {
            MD5 mD5 = MD5.Create();
            byte[] b = Encoding.ASCII.GetBytes(_password);
            byte[] hash = mD5.ComputeHash(b);
            StringBuilder sb = new StringBuilder();
            foreach (var a in hash)
                sb.Append(a.ToString("X2"));
            return sb.ToString();
        }
    }
}
