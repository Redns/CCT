﻿namespace CCT.Common
{
    public class NFCheck
    {
        /// <summary>
        /// 判断浮点数是否为整数
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool IsInteger(double d) 
        {
            return (int)d == d;
        }
    }
}
