using System;

namespace VipCRM.Data.BusinessAdo
{
    public class EncryptUtils
    {
        // Methods
        private static string crypt(string s, int tipoChave, bool decrypto)
        {
            long num2;
            char ch;
            string str = "";
            string str2 = "";
            string str3 = "";
            string chave = "";
            string str5 = "";
            string str6 = "";
            chave = GetChave(tipoChave);
            if (decrypto)
            {
                for (int k = 0; k <= (s.Length - 1); k += 2)
                {
                    num2 = Convert.ToInt64("0x" + s.Substring(k, 2), 0x10);
                    str = str + ((char)((ushort)num2));
                }
            }
            else
            {
                str = s;
            }
            int startIndex = 0;
            for (int i = 0; i <= (str.Length - 1); i++)
            {
                ch = str.Substring(i, 1)[0];
                char ch2 = chave.Substring(startIndex, 1)[0];
                str5 = str5 + ((char)(ch ^ ch2));
                startIndex++;
                if (startIndex > (chave.Length - 1))
                {
                    startIndex = 0;
                }
                str6 = str5;
            }
            if (decrypto)
            {
                return str6;
            }
            for (int j = 0; j <= (str5.Length - 1); j++)
            {
                ch = str5.Substring(j, 1)[0];
                num2 = (long)ch;
                str3 = string.Format("{0:x2}", num2).ToUpper();
                str2 = str2 + str3.Substring(str3.Length - 2, str3.Length);
            }
            return str2;
        }

        public static string Crypt(string text, int key)
        {
            return crypt(text, key, false);
        }

        public static string Decrypt(string text, int key)
        {
            return crypt(text, key, true);
        }

        protected static string GetChave(int tipoChave)
        {
            switch (tipoChave)
            {
                case 0:
                    return "!@#$%&*()<>!&*$#@45F15AFBC32FF5516AFDFEB";

                case 1:
                    return "%&*(\x00a8$#%\x00a8*()(+<>hred75418!@!@##&*(";

                case 2:
                    return "*)+RTrtr\x00a8&%%$#@%&*(\x00a8$#%\x00a8*()fjk5*8956t67H";

                case 3:
                    return "$%&\x00a8&%$#!@#*%\x00a8#$09BDbdKCT789CU2%\x00a8xtdt%\x00a8#!%#%&&$@&0978&*5645$%&5##%%#@5$#@5#@!!25@$!54$%85@";
            }
            throw new Exception("Criptografia: Chave inv\x00e1lida.");
        }
    }
}