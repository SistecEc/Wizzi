using System;

namespace Wizzi.Helpers
{
    public static class utils
    {
        public static string generarCodigoFecha()
        {
            return DateTime.Now.ToString("yyMMddHHmmssf");
        }
    }
}
