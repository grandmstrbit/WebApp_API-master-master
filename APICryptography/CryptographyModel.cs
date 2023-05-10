using System.Security.Cryptography;
using System.Text;

namespace APICryptography;

internal class Cryptography
{
    private static void Main(string[] args)
    {
        string text = "Hello World!";
        byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }.Select(x => x).ToArray();
        Console.WriteLine(Encrypt(text, key));

        string base64 = Encrypt(text, key);
        Console.WriteLine(base64);
        //Console.WriteLine(FromAes256(base64, key));
        Console.ReadKey();
        
    }

    private static string Encrypt(string text, byte[] key)
    {
        //Объявляем объект класса AES
        using Aes aes = Aes.Create();
        aes.Key = key;
        //Генерируем соль
        //aes.GenerateIV();
        //Присваиваем ключ. aeskey - переменная (массив байт), сгенерированная методом GenerateKey() класса AES
        //aes.Key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }; // todo: initial key
        byte[] encrypted;
        //ICryptoTransform crypt = aes.CreateEncryptor(aes.Key, aes.IV);
        using MemoryStream ms = new();
        ms.Write(aes.IV);
        using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write, true))
        {
            cs.Write(Encoding.UTF8.GetBytes(text));
        }
        //Записываем в переменную encrypted зашиврованный поток байтов
        encrypted = ms.ToArray();

        return Convert.ToBase64String(ms.ToArray());
    }
}


            //Возвращаем поток байт + крепим соль
            //return encrypted.Concat(aes.IV).ToArray();


/*
/// <summary>
/// Расшифровывает криптованного сообщения
/// </summary>
/// <param name="shifrtext">Шифротекст в байтах</param>
/// <returns>Возвращает исходную строку</returns>
public static string FromAes256(byte[] shifrtext)
{
    byte[] bytesIv = new byte[16];
    byte[] mess = new byte[shifrtext.Length - 16];
    //Списываем соль
    for (int i = shifrtext.Length - 16, j = 0; i < shifrtext.Length; i++, j++)
        bytesIv[j] = shifrtext[i];
    //Списываем оставшуюся часть сообщения
    for (int i = 0; i < shifrtext.Length - 16; i++)
        mess[i] = shifrtext[i];
    //Объект класса Aes
    Aes aes = Aes.Create();
    //Задаем тот же ключ, что и для шифрования
    aes.Key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }; // todo: initial key
    //Задаем соль
    aes.IV = bytesIv;
    //Строковая переменная для результата
    string text = "";
    byte[] data = mess;
    ICryptoTransform crypt = aes.CreateDecryptor(aes.Key, aes.IV);
    using (MemoryStream ms = new MemoryStream(data))
    {
        using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Read))
        {
            using (StreamReader sr = new StreamReader(cs))
            {
                //Результат записываем в переменную text в виде исходной строки
                text = sr.ReadToEnd();
            }
        }
    }
    return text;
*/

