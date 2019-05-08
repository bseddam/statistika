using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

/// <summary>
/// Summary description for Cryptography
/// </summary>
public class Cryptography
{   

    private static byte[] EncryptionKey { get; set; }
    private static byte[] Global_IV { get; set; }

    public static void TripleDESImplementation(string encryptionKey, string IV)
    { 
        if (string.IsNullOrEmpty(encryptionKey))
        {
            throw new ArgumentNullException("'encryptionKey' parameter cannot be null.", "encryptionKey");
        }
        if (string.IsNullOrEmpty(IV))
        {
            throw new ArgumentException("'IV' parameter cannot be null or empty.", "IV");
        }
        EncryptionKey = UTF8Encoding.UTF8.GetBytes(encryptionKey);
        // Ensures length of 24 for encryption key
        Trace.Assert(EncryptionKey.Length == 24, "Encryption key must be exactly 24 characters of ASCII text (24 bytes)");
        Global_IV = UTF8Encoding.UTF8.GetBytes(IV);
        // Ensures length of 8 for init. vector
        Trace.Assert(Global_IV.Length == 8, "Init. vector must be exactly 8 characters of ASCII text (8 bytes)");
    }

    /// Encrypts a text block
    public static string Encrypt(string textToEncrypt)
    {
        textToEncrypt += "#" + DateTime.Now.Ticks.ToString();
        TripleDESImplementation("fblsQBxfNs6nQ10wsRcMFwCN", "25ywte53");       
        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = EncryptionKey;
        tdes.IV = Global_IV;
        byte[] buffer = UTF8Encoding.UTF8.GetBytes(textToEncrypt); //Encoding.ASCII.GetBytes(textToEncrypt);
        return Convert.ToBase64String(tdes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length)).Replace("+", "plus").Replace("=", "equal").Replace("/", "slAsh");

    } 

    /// Decrypts an encrypted text block
    public static string Decrypt(string textToDecrypt)
    {
        try
        {
          
            TripleDESImplementation("fblsQBxfNs6nQ10wsRcMFwCN", "25ywte53");

            byte[] buffer = Convert.FromBase64String(textToDecrypt.Replace("plus", "+").Replace("equal", "=").Replace("slAsh", "/"));
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = EncryptionKey;
            des.IV = Global_IV;
            string token = UTF8Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            return token.Split('#')[0];
        }
        catch
        {
            return "-1";
        }


    }

    public static string TokenGeneration(string Value)
    {
        TripleDESImplementation("fblsQBxfNs6nQ10wsRcMFwCN", "25ywte53");

        return Encrypt(Value + "#" + DateTime.Now.Ticks.ToString());
    }
}