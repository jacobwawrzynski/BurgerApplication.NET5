﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DataBaseContext.Security
{
    /// <summary>
    /// Encryption class
    /// </summary>
    public class Encryption
   {
        /// <summary>
        /// encrypts the string
        /// </summary>
        /// <param name="plainText">string to encrypt</param>
        /// <param name="hashAlgorithm">"SHA384" or "SHA512" or "MD5". Default is "MD5" </param>
        /// <param name="saltBytes">"salt is added to the hashing process to force their uniqueness, increase their complexity without increasing user requirements, and to mitigate password attacks like hash tables"</param>
        /// <returns>encrypt string</returns>
        public static string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes)
      {
         if (saltBytes == null)
         {
            int minSaltSize = 4;
            int maxSaltSize = 8;

            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            saltBytes = new byte[saltSize];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(saltBytes);
         }

         byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

         byte[] plainTextWithSaltBytes =
         new byte[plainTextBytes.Length + saltBytes.Length];

         for (int i = 0; i < plainTextBytes.Length; i++)
            plainTextWithSaltBytes[i] = plainTextBytes[i];

         for (int i = 0; i < saltBytes.Length; i++)
            plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

         HashAlgorithm hash;

         if (hashAlgorithm == null)
            hashAlgorithm = "";

         switch (hashAlgorithm.ToUpper())
         {

            case "SHA384":
               hash = new SHA384Managed();
               break;

            case "SHA512":
               hash = new SHA512Managed();
               break;

            default:
               hash = new MD5CryptoServiceProvider();
               break;
         }

         byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

         byte[] hashWithSaltBytes = new byte[hashBytes.Length +
         saltBytes.Length];

         for (int i = 0; i < hashBytes.Length; i++)
            hashWithSaltBytes[i] = hashBytes[i];

         for (int i = 0; i < saltBytes.Length; i++)
            hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];


         string hashValue = Convert.ToBase64String(hashWithSaltBytes);

         return hashValue;
      }
        /// <summary>
        /// compare uncrypted string to encrypted string
        /// </summary>
        /// <param name="plainText">uncrypted string</param>
        /// <param name="hashAlgorithm">"SHA384" or "SHA512" or "MD5". Default is "MD5" </param>
        /// <param name="hashValue">encrypted string</param>
        /// <returns>true if encrypted string equals uncrypted string</returns>
        public static bool VerifyHash(string plainText, string hashAlgorithm, string hashValue)
      {

         byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

         int hashSizeInBits, hashSizeInBytes;

         if (hashAlgorithm == null)
            hashAlgorithm = "";

         switch (hashAlgorithm.ToUpper())
         {

            case "SHA384":
               hashSizeInBits = 384;
               break;

            case "SHA512":
               hashSizeInBits = 512;
               break;

            default:
               hashSizeInBits = 128;
               break;
         }


         hashSizeInBytes = hashSizeInBits / 8;

         if (hashWithSaltBytes.Length < hashSizeInBytes)
            return false;

         byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

         for (int i = 0; i < saltBytes.Length; i++)
            saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

         string expectedHashString = ComputeHash(plainText, hashAlgorithm, saltBytes);

         return (hashValue == expectedHashString);
      }
   }
}
