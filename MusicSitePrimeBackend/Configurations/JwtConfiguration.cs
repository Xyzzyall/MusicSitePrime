using System.Text;

namespace MusicSitePrimeBackend.Configurations;

public class JwtConfiguration
{
    public string Secret { get; set; }

    public double HoursExpires { get; set; }

    public byte[] GetSecretInBytes()
    {
        return Encoding.ASCII.GetBytes(Secret);
    }
}