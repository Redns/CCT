namespace CCT.ISource
{
    /// <summary>
    /// 数据流统计类，对数据流中字符的概率分布进行统计                                                                       
    /// </summary>
    public class Counter
    {
        /// <summary>
        /// 字符概率分布统计结果
        /// </summary>
        public struct CountResult
        {
            public string Msg { get; set; }                         // 数据流
            public long Length { get; set; }                        // 输入字符串长度
            public Dictionary<char, double> Symbols { get; set; }   // 字符串中字符的概率分布(格式为：字符-概率)

            public CountResult(string msg, long length, Dictionary<char, double> characters)
            {
                Msg = msg;
                Length = length;
                Symbols = characters;
            }
        }


        /// <summary>
        /// 统计数据流中的字符概率分布
        /// </summary>
        /// <param name="msg">数据流</param>
        /// <returns></returns>
        public static CountResult Count(string msg)
        {
            var res = new CountResult(msg, msg.Length, new Dictionary<char, double>());
            
            // 统计数据流中的字符
            for(int i = 0; i < msg.Length; i++)
            {
                if (res.Symbols.ContainsKey(msg[i]))
                {
                    res.Symbols[msg[i]] += 1; 
                }
                else
                {
                    res.Symbols.Add(msg[i], 1);
                }
            }

            // 归一化概率
            foreach(var character in res.Symbols.Keys)
            {
                res.Symbols[character] /= res.Length; 
            }

            return res;
        }
    }
}
