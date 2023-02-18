using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Read_and_Learn.Variable
{
    class global
    {
        public static int id_user; //айди пользователя
        public static string user_group; //группа пользователя
        public static bool indecs = false; //индефикация входа студента\учителя
        public static bool atantion = false; //выполнение действий пользователя
    }
    class connection
    {
        public static string connect = "Server=31.31.198.171;Database=u1602320_default;User=u1602_320default;Password=Q7Lqf6^ds1Uornyv;Charset=utf8";        
    }
    class testing
    {
        public static int id_test; //айди выбранного тест
        public static int id_question = 1; //айди выбранного вопроса
    }
    class crypto
    {
        public static ushort secretKey = 0x00005; //Secret key
    }
}
