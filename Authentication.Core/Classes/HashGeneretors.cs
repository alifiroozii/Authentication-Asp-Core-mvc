﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Core.Classes
{
    public class HashGeneretors
    {
        public static string EncodingPassWithMd5(string password)
        {

            byte[] mainBytes;
            byte[] encodeBytes;

            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            mainBytes = ASCIIEncoding.Default.GetBytes(password);

            encodeBytes = md5.ComputeHash(mainBytes);
            return BitConverter.ToString(encodeBytes);


        }


    }
}
