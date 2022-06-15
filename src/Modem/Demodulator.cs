using System.Text;

namespace CCT.Modem
{
    public class Demodulator
    {
        /// <summary>
        /// BPSK解调模块
        /// </summary>
        /// <param name="i">I路信号</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string BPSK(double[] i)
        {
            if(i.Length == 0)
            {
                throw new ArgumentException("[BPSK]解调失败，输入参数错误");
            }
            else
            {
                var bitStream = new StringBuilder();
                for(int index = 0; index < i.Length; index++)
                {
                    if (i[index] > 0) { bitStream.Append('1'); }
                    else { bitStream.Append('0'); }
                }
                return bitStream.ToString();
            }
        }


        /// <summary>
        /// QPSK星座图映射模式
        /// </summary>
        public enum QPSK_MAP_MODE
        {
            A = 0,  // 星座点在坐标轴上
            B       // 星座点在对角线上
        }


        /// <summary>
        /// QPSK解调模块
        /// </summary>
        /// <param name="i">I路信号</param>
        /// <param name="q">Q路信号</param>
        /// <returns></returns>
        public static string QPSK(double[] i, double[] q, QPSK_MAP_MODE mode = QPSK_MAP_MODE.B)
        {
            if((i.Length == 0) || (q.Length == 0) || (i.Length != q.Length))
            {
                throw new ArgumentException("[QPSK]解调失败，输入参数错误");
            }
            else
            {
                var bitStream = new StringBuilder();
                for(int index = 0; index < i.Length; index++)
                {
                    if(mode == QPSK_MAP_MODE.A)
                    {
                        if ((i[index] < 0) && (Math.Abs(i[index]) >= q[index])) { bitStream.Append("00"); }
                        else if ((q[index] < 0) && (Math.Abs(q[index]) > i[index])) { bitStream.Append("01"); }
                        else if ((i[index] > 0) && (i[index] >= q[index])){ bitStream.Append("11"); }
                        else { bitStream.Append("10"); }
                    }
                    else if(mode == QPSK_MAP_MODE.B)
                    {
                        if ((i[index] < 0) && (q[index] < 0)) { bitStream.Append("00"); }
                        else if ((i[index] > 0) && (q[index] < 0)) { bitStream.Append("01"); }
                        else if ((i[index] > 0) && (q[index] > 0)) { bitStream.Append("11"); }
                        else { bitStream.Append("10"); }
                    }
                }
                return bitStream.ToString();
            }
        }


        /// <summary>
        /// 2ASK解调
        /// </summary>
        /// <param name="i">I路信号</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string BASK(double[] i)
        {
            if(i.Length == 0) { throw new ArgumentException("[BASK]解调失败，输入参数为空"); }
            else
            {
                var bitStream = new StringBuilder();
                for(int t = 0; t < i.Length; t++)
                {
                    bitStream.Append(i[t]);
                }
                return bitStream.ToString();
            }
        }
    }
}
