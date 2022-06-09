using CCT.Common;

namespace CCT.ISource.EncodingAndDecoding
{
    public class Hoffman
    {
        public static EncodingResult Encoding(double[] probabilities)
        {
            // 检查输入参数
            foreach(var probability in probabilities)
            {
                if((probability < 0) || (probability > 1))
                {
                    throw new ArgumentException("符号概率不能小于0，也不能大于1!");
                }
            }

            // 迭代编码
            if (probabilities.Length == 2)
            {
                return new EncodingResult(new string[] { "1", "0" }, 1, Common.Math.Entropy(probabilities));
            }
            else
            {
                EncodingResult res = new(new string[probabilities.Length], 0, 0);

                // 合并概率最小的两项
                var combineProbabilities = new double[probabilities.Length - 1];
                Array.Copy(probabilities, combineProbabilities, combineProbabilities.Length - 2);
                combineProbabilities[^1] = probabilities[^2] + probabilities[^1];

                // 降序排列合并后的数组
                var combineProbabilitiesSortResult = SortPlus.Sort(combineProbabilities);
                var combineProbabilitiesEncodingResult = Encoding(combineProbabilitiesSortResult.Data);
                var combineProbabilitiesCondons = new string[combineProbabilities.Length];
                for(int i = 0; i < combineProbabilitiesSortResult.Index.Length; i++)
                {
                    combineProbabilitiesCondons[combineProbabilitiesSortResult.Index[i]] = combineProbabilitiesEncodingResult.Codons[i];
                }

                // 合并编码
                Array.Copy(combineProbabilitiesCondons, res.Codons, res.Codons.Length - 2);
                res.Codons[^2] = combineProbabilitiesCondons.Last() + "1";
                res.Codons[^1] = combineProbabilitiesCondons.Last() + "0";

                // 计算平均码长和编码效率
                res.AverageLength = Common.Math.SourceEncodingAverageLength(probabilities, res.Codons);
                res.Efficient = Common.Math.SourceEncodingEfficient(Common.Math.Entropy(probabilities), res.AverageLength);
                return res;
            }
        }
    }
}
