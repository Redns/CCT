namespace SourceEncoding
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 信源概率分布
            var iSourceProbabilities = new double[] { 0.2, 0.19, 0.18, 0.17, 0.15, 0.1, 0.01 };

            // 1.霍夫曼编码(二进制)
            Console.WriteLine("[霍夫曼编码]\n**********************\n符号概率\t码字");

            var hoffmanEncodingResult = CCT.ISource.Encoder.Hoffman(iSourceProbabilities);
            for (int i = 0; i < iSourceProbabilities.Length; i++)
            {
                Console.WriteLine($"{iSourceProbabilities[i]}\t\t{hoffmanEncodingResult.Codons[i]}");
            }
            Console.WriteLine($"平均码长\t{hoffmanEncodingResult.AverageLength:#.##}");
            Console.WriteLine($"编码效率\t{(hoffmanEncodingResult.Efficient * 100):#.##}%");

            // 2.费诺编码
            Console.WriteLine("\n[费诺编码]\n**********************\n符号概率\t码字");

            var fenoEncodingResult = CCT.ISource.Encoder.Feno(iSourceProbabilities);
            for (int i = 0; i < iSourceProbabilities.Length; i++)
            {
                Console.WriteLine($"{iSourceProbabilities[i]}\t\t{fenoEncodingResult.Codons[i]}");
            }

            Console.WriteLine($"平均码长\t{fenoEncodingResult.AverageLength:#.##}");
            Console.WriteLine($"编码效率\t{(fenoEncodingResult.Efficient * 100):#.##}%");

            // 3.香农编码
            Console.WriteLine("\n[香农编码]\n**********************\n符号概率\t码字");

            var shannonEncodingResult = CCT.ISource.Encoder.Shannon(iSourceProbabilities);
            for (int i = 0; i < iSourceProbabilities.Length; i++)
            {
                Console.WriteLine($"{iSourceProbabilities[i]}\t\t{shannonEncodingResult.Codons[i]}");
            }

            Console.WriteLine($"平均码长\t{shannonEncodingResult.AverageLength:#.##}");
            Console.WriteLine($"编码效率\t{(shannonEncodingResult.Efficient * 100):#.##}%");
        }
    }
}