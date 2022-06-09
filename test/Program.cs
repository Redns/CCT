namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 霍夫曼编码
            var s = CCT.ISource.EncodingAndDecoding.Hoffman.Encoding(new double[] { 0.35, 0.3, 0.2, 0.1, 0.04, 0.005, 0.005 });
        }
    }
}