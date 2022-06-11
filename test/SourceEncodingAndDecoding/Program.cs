namespace SourceEncodingAndDecoding
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 原始数据
            var msg = "This is a test txt.";

            // 1.数据统计
            var countResult = CCT.ISource.Counter.Count(msg);

            // 2.信源编码（霍夫曼、费诺、香农）
            var hoffmanResult = CCT.ISource.Encoder.Hoffman(countResult);

            // 3.转换
            var encodeMsg = CCT.ISource.Encoder.Encode(msg, hoffmanResult.Codons);

            // 4.译码
            var decodeMsg = CCT.ISource.Decoder.Decode(encodeMsg, hoffmanResult.Codons);
        }
    }
}