using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Globus.Utilities.Common
{
    public class GenericUtil
    {

        public static string FetchStateLGA()
        {
            string path1 = @"StateLGA.json";

            string fullPath = Path.GetFullPath(path1);

            return fullPath;
        }

        public static string generatePrimaryId()
        {
            try
            {
                Guid guid = Guid.NewGuid();
                return guid.ToString("N");
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public static string GenerateOTP()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            

            string otp = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } 
                while (otp.IndexOf(character) != -1);
                otp += character;
            }

            return otp;
        }
    }
}