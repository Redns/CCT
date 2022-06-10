namespace CCT.ISource
{
    public class Decoder
    {
        /// <summary>
        /// 使用指定码集对序列译码
        /// </summary>
        /// <param name="encodeMsg">编码后的序列</param>
        /// <param name="codeSet">码集</param>
        /// <returns></returns>
        public static string Decode(string encodeMsg, Dictionary<string, string> codeSet)
        {
            var buffer = string.Empty;
            var decodeMsg = string.Empty;

            var codeSetReverse = new Dictionary<string, string>();
            foreach(var character in codeSet.Keys)
            {
                codeSetReverse.Add(codeSet[character], character);
            }


            for(int i = 0; i < encodeMsg.Length; i++)
            {
                buffer += encodeMsg[i];
                if (codeSetReverse.ContainsKey(buffer))
                {
                    decodeMsg += codeSetReverse[buffer];
                    buffer = string.Empty;
                }
            }

            return decodeMsg;
        }
    }
}
