using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator
{
    public class PassGen
    {

        private static string lowerCase = "abcdefghijklmnopqrstuvwxyz";
        private static string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string numbers = "1234567890";
        private static string special = "`~!@#$%^&*()-=_+[]{}|;\':\",./<>?";
        private static string hex = "123456789ABCDEF";

        public enum PasswordSelector
        {
            /// <summary>
            /// A good password consting of lowercase, uppercase and numbers, with a length of 10.
            /// </summary>
            good_pw = 10,

            /// <summary>
            /// A strong password consting of lowercase, uppercase, numbers and special chars, with a length of 15.
            /// </summary>
            strong_pw = 15,

            /// <summary>
            /// A strong password consting of lowercase, uppercase, numbers and special chars, with a length of 30.
            /// </summary>
            super_pw = 30,

            /// <summary>
            /// A 256 bit key consting of lowercase, uppercase, and numbers, with a length of 32.
            /// </summary>
            key_256 = 32,

            /// <summary>
            /// A WPA 160 bit key
            /// </summary>
            wpa_160 = 20,

            /// <summary>
            /// /// A WPA 504 bit key
            /// </summary>
            wpa_504 = 63,

            /// <summary>
            /// A WEP 64 bit key
            /// </summary>
            wep_64 = 5,

            /// <summary>
            /// A WEP 128 bit key
            /// </summary>
            wep_128 = 13,
            /// <summary>
            /// A WEP 152 bit key
            /// </summary>
            wep_152 = 16,
            /// <summary>
            /// A WEP 256 bit key
            /// </summary>
            wep_256 = 29
        }


        public static string PasswordGenerator(PasswordSelector selector)
        {
            switch (selector)
            {
                case PasswordSelector.good_pw:
                    return keyGen((int)PasswordSelector.good_pw, true, true, true, false, false);
                case PasswordSelector.strong_pw:
                    return keyGen((int)PasswordSelector.strong_pw, true, true, true, true, false);
                case PasswordSelector.super_pw:
                    return keyGen((int)PasswordSelector.super_pw, true, true, true, true, false);
                case PasswordSelector.key_256:
                    return keyGen((int)PasswordSelector.key_256, true, true, true, false, false);
                case PasswordSelector.wpa_160:
                    return keyGen((int)PasswordSelector.wpa_160, true, true, true, true, false);
                case PasswordSelector.wpa_504:
                    return keyGen((int)PasswordSelector.wpa_504, true, true, true, true, false);
                case PasswordSelector.wep_64:
                    return keyGen((int)PasswordSelector.wep_64, false, false, false, false, true);
                case PasswordSelector.wep_128:
                    return keyGen((int)PasswordSelector.wep_128, false, false, false, false, true);
                case PasswordSelector.wep_152:
                    return keyGen((int)PasswordSelector.wep_152, false, false, false, false, true);
                case PasswordSelector.wep_256:
                    return keyGen((int)PasswordSelector.wep_256, false, false, false, false, true);
                default:
                    return ("Not a password type");
            }
        }

        public static string PasswordGenerator(int length = 8, bool useLowerCase = true, bool useUpperCase = true, bool useNumbers = true, bool useSpecial = true, bool useHex = false)
        {
            return keyGen(length, useLowerCase, useUpperCase, useNumbers, useSpecial, useHex);
        }

        private static string keyGen(int length, bool useLowerCase = true, bool useUpperCase = true, bool useNumbers = true, bool useSpecial = true, bool useHex = false)
        {
            string chars = "";
            string key = "";

            chars += useLowerCase ? lowerCase : "";
            chars += useUpperCase ? upperCase : "";
            chars += useNumbers ? upperCase : "";
            chars += useSpecial ? special : "";
            chars += useHex ? hex : "";

            for (int i = 0; i < length; i++)
            {
                key += chars[GetRandomPosition(chars.Length)];
            }
            return key;
        }

        private static int GetRandomPosition(int length)
        {
            using (RNGCryptoServiceProvider RND = new RNGCryptoServiceProvider())
            {
                byte[] r = new byte[4000];
                RND.GetBytes(r);
                int value = BitConverter.ToInt32(r, 0);
                Random random = new Random(value);
                return random.Next(0, length);
            }
        }
    }
}
