namespace CCT.ISource
{
    public class Transformer
    {
        /// <summary>
        /// 使用特定码集对信源进行转换
        /// </summary>
        /// <param name="msg">信源字符序列</param>
        /// <param name="codeSet">码集</param>
        /// <returns></returns>
        public static string Transform(string msg, Dictionary<string, string> codeSet)
        {
            foreach(var character in codeSet.Keys)
            {
                msg = msg.Replace(character, codeSet[character]);
            }
            return msg;
        }
    }
}
