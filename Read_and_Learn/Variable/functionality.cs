using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_and_Learn.Variable
{
    class functionality
    {
        public static char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey); //Производим XOR операцию
            return character;
        }

        public static string EncodeDecrypt(string str, ushort secretKey)
        {
            var ch = str.ToArray(); //преобразуем строку в символы
            string newStr = "";      //переменная которая будет содержать зашифрованную строку
            foreach (var c in ch)  //выбираем каждый элемент из массива символов нашей строки
                newStr += TopSecret(c, secretKey);  //производим шифрование каждого отдельного элемента и сохраняем его в строку
            return newStr;
        }

        public static string randomb(int len)
        {
            string s = "", symb = "1234567890abcdefghijklmnopqrstuvwxyz";
            Random rnd = new Random();

            for (int i = 0; i < len; i++)
                s += symb[rnd.Next(0, symb.Length)];
            return s;
        }
    }
}
