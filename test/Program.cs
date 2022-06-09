namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var s = CCT.ISource.EncodingAndDecoding.Hoffman.Encoding(new double[] { 0.45, 0.35, 0.2 });
        }
    }
}