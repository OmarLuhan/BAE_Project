using System.Security.Cryptography;
using System.Text;
using CapstoneG14.Services.Interfaces;

namespace CapstoneG14.Services.Implementations
{
    public class UtilidadesService : IUtilidadesService
    {

        public string ConvertirSha256(string input)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] bytes = hash.ComputeHash(enc.GetBytes(input));
                foreach (byte b in bytes)
                {
                    Sb.Append(b.ToString("x2"));
                }
            }
            return Sb.ToString();
        }

        public string GenerarClave()
        {
            var clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }
    }
}