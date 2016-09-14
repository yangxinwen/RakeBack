using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RakeBack.Helper
{
    public class DesSecurityCommon
    {
        //private static string key = "dongbinhuiasxiny";
        /// <summary>  
        /// 获取密钥  
        /// </summary>  
        private static string Key
        {
            get { return @"f*&k!23456&*9qqq"; }
        }

        /// <summary>  
        /// 获取向量  
        /// </summary>  
        private static string IV
        {
            get { return @"L+\~f4,Ir)b$=pkf"; }
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="value">要加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            return Encrypt(value);
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Value">加密后的字符串</param>
        /// <returns>解密后的字符串</returns>
        public string DecryptString(string value, Action<object> outPutErrMsgHandler = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            string result = string.Empty;

            try
            {
                result = Decrypt(value);
            }
            catch (Exception ex)
            {
                if (outPutErrMsgHandler != null)
                {
                    outPutErrMsgHandler(ex);
                }
            }

            return result;
        }
        /// <summary>
        /// 字符串的加密
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        /// <param name="sKey">密钥，必须32位</param>
        /// <param name="sIV">向量，必须是12个字符</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptString(string value, string sKey, string sIV)
        {
            try
            {
                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
                byte[] byt;
                mCSP.Key = Convert.FromBase64String(sKey);
                mCSP.IV = Convert.FromBase64String(sIV);
                //指定加密的运算模式
                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                //获取或设置加密算法的填充模式
                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);//创建加密对象
                byt = Encoding.UTF8.GetBytes(value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ("Error in Encrypting " + ex.Message);
            }
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Value">加密后的字符串</param>
        /// <param name="sKey">密钥，必须32位</param>
        /// <param name="sIV">向量，必须是12个字符</param>
        /// <returns>解密后的字符串</returns>
        public string DecryptString(string Value, string sKey, string sIV)
        {
            try
            {
                ICryptoTransform ct;//加密转换运算
                MemoryStream ms;//内存流
                CryptoStream cs;//数据流连接到数据加密转换的流
                SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
                byte[] byt;
                //将3DES的密钥转换成byte
                mCSP.Key = Convert.FromBase64String(sKey);
                //将3DES的向量转换成byte
                mCSP.IV = Convert.FromBase64String(sIV);
                mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
                mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);//创建对称解密对象
                byt = Convert.FromBase64String(Value);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();
                cs.Close();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "出现异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ("Error in Decrypting " + ex.Message);
            }
        }


        /// <summary>
        /// AES Rijndael算法加密
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(Key);
            // byte[] ivArray = UTF8Encoding.UTF8.GetBytes(IV);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            //rDel.IV = null;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.KeySize = 128;

            //ICryptoTransform cTransform = rDel.CreateEncryptor();
            //byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //return Convert.ToBase64String(resultArray, 0, resultArray.Length);

            string encrypt = null;
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, rDel.CreateEncryptor(keyArray, null), CryptoStreamMode.Write))
                {
                    cStream.Write(toEncryptArray, 0, toEncryptArray.Length);
                    cStream.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(mStream.ToArray());
                }
            }
            rDel.Clear();
            return encrypt;
        }

        /// <summary>
        /// AES Rijndael算法解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(Key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(IV);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            //rDel.IV = ivArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            rDel.KeySize = 128;

            //ICryptoTransform cTransform = rDel.CreateDecryptor();
            //byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //return UTF8Encoding.UTF8.GetString(resultArray);

            string decrypt = null;
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, rDel.CreateDecryptor(keyArray, null), CryptoStreamMode.Write))
                {
                    cStream.Write(toEncryptArray, 0, toEncryptArray.Length);
                    cStream.FlushFinalBlock();
                    decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                }
            }
            rDel.Clear();
            return decrypt;
        }

        //默认密钥向量
        //private static byte[] IV1 = { 0x67, 0x51, 0x40, 0x2F, 0xDE, 0x0F, 0x3E,0x1A};
        //private static string key1 = "@guoxind";

        public static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public static string keyUse = "12345678";
        public static string keyDe = "12345678";

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string DESEncrypt(string toEncryptString)
        {
            EncryptDES(toEncryptString, System.Text.Encoding.Default.GetString(Keys));
            return EncryptDES(toEncryptString, keyUse);
            //try
            //{
            //    if (toEncryptString == null || toEncryptString == "" || toEncryptString == string.Empty)
            //    {
            //        return "";
            //    }
            //    byte[] rgbKey = Encoding.UTF8.GetBytes(key1.Substring(0, 8));//
            //    byte[] rgbIV = IV1;
            //    byte[] inputByteArray = Encoding.UTF8.GetBytes(toEncryptString);
            //    DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            //    MemoryStream mStream = new MemoryStream();
            //    CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            //    cStream.Write(inputByteArray, 0, inputByteArray.Length);
            //    cStream.FlushFinalBlock();
            //    return Convert.ToBase64String(mStream.ToArray());
            //}
            //catch
            //{
            //    return toEncryptString;
            //}
        }
 
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="toDecryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DESDecrypt(string toDecryptString)
        {
            return DecryptDES(toDecryptString, keyDe);
            //try
            //{
            //    if (toDecryptString == null || toDecryptString == "" || toDecryptString == string.Empty)
            //    {
            //        return "";
            //    }
            //    byte[] rgbKey = Encoding.UTF8.GetBytes(key1);
            //    byte[] rgbIV = IV1;
            //    byte[] inputByteArray = Convert.FromBase64String(toDecryptString);
            //    DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            //    MemoryStream mStream = new MemoryStream();
            //    CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            //    cStream.Write(inputByteArray, 0, inputByteArray.Length);
            //    cStream.FlushFinalBlock();
            //    return Encoding.UTF8.GetString(mStream.ToArray(), 0, mStream.ToArray().Length);
            //}
            //catch
            //{
            //    return toDecryptString;
            //}
        }


        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串
        /// <param name="encryptKey">加密密钥,要求为8位
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        } 

 
    }
}
