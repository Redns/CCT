namespace CCT.ISource
{
    /// <summary>
    /// 数据流统计类                                                                 
    /// </summary>
    public class Counter
    {
        /// <summary>
        /// 统计字节数组中的符号概率分布
        /// </summary>
        /// <param name="data">输入字节数组</param>
        /// <returns></returns>
        public static Dictionary<byte, double> Count(byte[] data)
        {
            Dictionary<byte, double> symbols = new();

            // 统计数据流中的字符
            for(int i = 0; i < data.Length; i++)
            {
                if (symbols.ContainsKey(data[i]))
                {
                    symbols[data[i]] += 1; 
                }
                else
                {
                    symbols.Add(data[i], 1);
                }
            }

            // 归一化概率
            foreach(var symbolKey in symbols.Keys)
            {
                symbols[symbolKey] /= data.Length; 
            }

            return symbols;
        }


        /// <summary>
        /// 统计字符串中的符号概率分布
        /// </summary>
        /// <param name="data">输入字符串</param>
        /// <returns></returns>
        public static Dictionary<byte, double> Count(string data)
        {
            return Count(System.Text.Encoding.UTF8.GetBytes(data));
        }


        /// <summary>
        /// 统计数据流中的符号概率分布
        /// </summary>
        /// <param name="bitStream">输入数据流</param>
        /// <param name="bufferSize">缓冲区大小（字节）</param>
        /// <returns></returns>
        public static Dictionary<byte, double> Count(Stream bitStream, int bufferSize = 2048)
        {
            var buffer = new byte[bufferSize];
            var symbols = new Dictionary<byte, double>();

            int readinLen, totalLen = 0;
            while ((readinLen = bitStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                totalLen += readinLen;
                for (int i = 0; i < readinLen; i++)
                {
                    if (symbols.ContainsKey(buffer[i])) { symbols[buffer[i]] += 1; }
                    else { symbols.Add(buffer[i], 1); }
                }
            }

            // 归一化概率
            foreach (var symbolKey in symbols.Keys)
            {
                symbols[symbolKey] /= totalLen;
            }

            return symbols;
        }
    }
}
