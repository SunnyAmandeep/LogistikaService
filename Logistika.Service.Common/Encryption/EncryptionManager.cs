using System;
using System.IO;
using System.Security.Cryptography;
using System.Text; 
namespace Logistika.Service.Common.Encryption
{
    public class EncryptionManager
    {
        public static string BasicEncrypt(string text)
        {

            // Sending side
            byte[] data = new byte[0];
            try
            {
                data = Encoding.UTF8.GetBytes(text);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            string ret = Convert.ToBase64String(data);
            return ret;
        }
        public static String BasicDecrypt(String base64)
        {
            // Receiving side
            byte[] data = Convert.FromBase64String(base64);
            try
            {
                return Encoding.UTF8.GetString(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public static string Encrypt(string PlainText, string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                key = SiteConfigurationManager.GetAppSettingKey("EncryptionManagerDefaultKey");
            }
            string b = string.Empty;
            try
            {
                TripleDES des = CreateDES(key);
                ICryptoTransform ct = des.CreateEncryptor();
                byte[] input = Encoding.Unicode.GetBytes(PlainText);
                b = Convert.ToBase64String(ct.TransformFinalBlock(input, 0, input.Length));
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }

            return b;

        }
        public static string Decrypt(string CypherText, string key="")
        {
            if (string.IsNullOrEmpty(key))
            {
                key = SiteConfigurationManager.GetAppSettingKey("EncryptionManagerDefaultKey");
            }
            byte[] output;
            try
            {

                byte[] b = Convert.FromBase64String(CypherText);
                TripleDES des = CreateDES(key);
                ICryptoTransform ct = des.CreateDecryptor();
                output = ct.TransformFinalBlock(b, 0, b.Length);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex.InnerException);
            }
            return Encoding.Unicode.GetString(output);

        }
        private static TripleDES CreateDES(string key)
        {
            TripleDES des;
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                des = new TripleDESCryptoServiceProvider();
                des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
                des.IV = new byte[des.BlockSize / 8];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            return des;
        }
        #region WEB SERVICE PASSWORD Encryption
        public static string DecryptString(string InputText, string Password = "")
        {
            if (string.IsNullOrEmpty(Password))
            {
                Password = SiteConfigurationManager.GetAppSettingKey("EncryptionManagerDefaultKey");
            }
            InputText = InputText.Replace(' ', '+');
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.ToString());
            Rfc2898DeriveBytes SecretKey = new Rfc2898DeriveBytes(Password, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(EncryptedData);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
            byte[] PlainText = null;
            PlainText = new byte[EncryptedData.Length];

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();
            cryptoStream.Close();

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            return DecryptedData;
        }
         
        public static string EncryptString(string InputText,string Password = "")
        {
            if (string.IsNullOrEmpty(Password))
            {
                Password = SiteConfigurationManager.GetAppSettingKey("EncryptionManagerDefaultKey");
            }
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.ToString());
            Rfc2898DeriveBytes SecretKey = new Rfc2898DeriveBytes(Password, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);
            return EncryptedData;
        }
        #endregion
    }
}
