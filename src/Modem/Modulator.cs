using static CCT.Modem.Demodulator;

namespace CCT.Modem
{
    public class Modulator
    {
        /// <summary>
        /// BPSK调制
        /// </summary>
        /// <param name="bitStream">输入比特流</param>
        /// <returns></returns>
        public static (double[] i, double[] q) BPSK(string bitStream)
        {
            if (string.IsNullOrEmpty(bitStream)) { throw new ArgumentException("[BPSK]调制失败，输入比特流为空"); }
            else
            {
                var i = new double[bitStream.Length];
                var q = new double[bitStream.Length];

                for (int t = 0; t < bitStream.Length; t++)
                {
                    if (bitStream[t] == '1') { i[t] = 1; q[t] = 0; }
                    else if (bitStream[t] == '0') { i[t] = -1; q[t] = 0; }
                    else { throw new ArgumentException("[BPSK]调制失败，输入比特流错误"); }
                }

                return (i, q);
            }
        }


        /// <summary>
        /// QPSK调制
        /// </summary>
        /// <param name="bitStream">输入比特流</param>
        /// <returns></returns>
        public static (double[] i, double[] q) QPSK(string bitStream, QPSK_MAP_MODE mode = QPSK_MAP_MODE.B)
        {
            if (string.IsNullOrEmpty(bitStream) || (bitStream.Length % 2 != 0)) { throw new ArgumentException("[QPSK]调制失败，输入比特流长度非法"); }
            else
            {
                var i = new double[bitStream.Length / 2];
                var q = new double[bitStream.Length / 2];
                var signalAmp = Math.Sqrt(2) / 2;
                
                if(mode == QPSK_MAP_MODE.A)
                {
                    for (int t = 0; t < i.Length; t++)
                    {
                        switch (bitStream[(t * 2)..(t * 2 + 2)])
                        {
                            case "00": i[t] = -1; q[t] = 0; break;
                            case "01": i[t] = 0; q[t] = -1; break;
                            case "10": i[t] = 0; q[t] = 1; break;
                            case "11": i[t] = 1; q[t] = 0; break;
                            default: throw new ArgumentException("[QPSK]调制失败，输入比特流非法");
                        }
                    }    
                }
                else if(mode == QPSK_MAP_MODE.B)
                {
                    for (int t = 0; t < i.Length; t++)
                    {
                        switch (bitStream[(t * 2)..(t * 2 + 2)])
                        {
                            case "00": i[t] = -signalAmp; q[t] = -signalAmp; break;
                            case "01": i[t] = signalAmp; q[t] = -signalAmp; break;
                            case "10": i[t] = -signalAmp; q[t] = signalAmp; break;
                            case "11": i[t] = signalAmp; q[t] = signalAmp; break;
                            default: throw new ArgumentException("[QPSK]调制失败，输入比特流非法");
                        }
                    }   
                }

                return (i, q);
            }
        }


        /// <summary>
        /// 2ASK调制
        /// </summary>
        /// <param name="bitStream">输入比特流</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static (double[] i, double[] q) BASK(string bitStream)
        {
            if (string.IsNullOrEmpty(bitStream)) { throw new ArgumentException("[2ASK]调制失败，输入比特流为空"); }
            else
            {
                var i = new double[bitStream.Length];
                var q = new double[bitStream.Length];

                for(int t = 0; t < bitStream.Length; t++)
                {
                    i[t] = bitStream[t] - '0';
                    q[t] = 0;
                }

                return (i, q);
            }
            
        }


        ///// <summary>
        ///// 多元相位调制（M > 4）
        ///// </summary>
        ///// <param name="bitStream">输入比特流</param>
        ///// <returns></returns>
        //public static (double[] i, double[] q) MPSK(string bitStream, int m = 8)
        //{
        //    if(string.IsNullOrEmpty(bitStream) || (m < 8) || !NFCheck.IsInteger(System.Math.Log2(m)) || (bitStream.Length % System.Math.Log2(m) != 0))
        //    {
        //        throw new ArgumentException("[MPSK]调制失败，输入参数有误");
        //    }
        //    else
        //    {

        //    }
        //}
    }
}
