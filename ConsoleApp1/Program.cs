using System;
using System.Security.Cryptography;
using System.Text;

public class KeyGenerator
{
    public static string GenerateRandomKey(int keySizeInBytes)
    {
        // El tamaño de la clave se proporciona en bytes, pero necesitamos convertirlo a bits
        int keySizeInBits = keySizeInBytes * 8;

        // Creamos un generador de números aleatorios criptográficamente seguro
        using (var rng = new RNGCryptoServiceProvider())
        {
            // Creamos un array de bytes para almacenar la clave generada
            byte[] keyBytes = new byte[keySizeInBytes];

            // Generamos la clave aleatoria
            rng.GetBytes(keyBytes);

            // Convertimos los bytes a una cadena base64 para obtener una representación legible
            string base64Key = Convert.ToBase64String(keyBytes);

            return base64Key;
        }
    }

    public static void Main(string[] args)
    {
        // Generamos una clave con al menos 256 bits de longitud (32 bytes)
        string randomKey = GenerateRandomKey(32);

        Console.WriteLine("Clave aleatoria generada:");
        Console.WriteLine(randomKey);
    }
}

