namespace CCT.Common
{
    public class Math
    {
        /// <summary>
        /// 计算离散信源的熵
        /// </summary>
        /// <param name="probabilities">信源各符号的概率分布</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static double Entropy(double[] probabilities)
        {
            var entropy = 0.0;
            foreach(var probability in probabilities)
            {
                if(probability is < 0 or > 1) { throw new ArgumentException("符号概率不能小于0，也不能大于1!"); }
                else
                {
                    entropy -= probability * System.Math.Log2(probability);
                }
            }
            return entropy;
        }


        /// <summary>
        /// 信源编码效率计算
        /// </summary>
        /// <param name="sourceEntropy">信源的熵</param>
        /// <param name="codonAverageLength">编码后的平均码长</param>
        /// <param name="system">编码进制</param>
        /// <returns></returns>
        public static double SourceEncodingEfficient(double sourceEntropy, double codonAverageLength, int system = 2)
        {
            return sourceEntropy / (codonAverageLength * System.Math.Log2(system));
        }


        /// <summary>
        /// 计算平均码长
        /// </summary>
        /// <param name="probabilities">概率数组</param>
        /// <param name="codons">码字数组</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static double SourceEncodingAverageLength(double[] probabilities, string[] codons)
        {
            if((probabilities.Length != codons.Length) || (probabilities.Length == 0))
            {
                throw new ArgumentException("概率数组与码字数组长度不等，或两者均为空!");
            }
            else
            {
                var codonAverageLength = 0.0;
                for (int i = 0; i < probabilities.Length; i++)
                {
                    if ((probabilities[i] < 0) || (probabilities[i] > 1)) { throw new ArgumentException("符号概率不能小于0，也不能大于1!"); }
                    else
                    {
                        codonAverageLength += probabilities[i] * codons[i].Length;
                    }
                }
                return codonAverageLength;
            }
        }
    }
}
