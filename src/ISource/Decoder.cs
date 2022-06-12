using System.Text;

namespace CCT.ISource
{
    public class Decoder
    {
        /// <summary>
        /// 使用指定码集对序列译码
        /// </summary>
        /// <param name="encodeBitStream">待译码的比特序列</param>
        /// <param name="symbols">符号集合</param>
        /// <returns></returns>
        public static byte[] Decode(string encodeBitStream, Dictionary<byte, string> symbols)
        {
            var buffer = new StringBuilder();
            var decodeMsg = new List<byte>();

            var symbolsReverse = new Dictionary<string, byte>();
            foreach(var character in symbols.Keys)
            {
                symbolsReverse.Add(symbols[character], character);
            }

            for(int i = 0; i < encodeBitStream.Length; i++)
            {
                buffer.Append(encodeBitStream[i]);
                if (symbolsReverse.ContainsKey(buffer.ToString()))
                {
                    decodeMsg.Add(symbolsReverse[buffer.ToString()]);
                    buffer.Clear();
                }
            }

            return decodeMsg.ToArray();
        }
    }
}
