using CCT.Common;

namespace CCT.ISource
{
    public class Encoder
    {
        /// <summary>
        /// 信源编码结果
        /// </summary>s
        public struct EncodeResult
        {
            public Dictionary<string, string> Codons { get; set; }  // 码字
            public double AverageLength { get; set; }               // 平均码长
            public double Efficient { get; set; }                   // 编码效率

            public EncodeResult()
            {
                Codons = new Dictionary<string, string>();
                AverageLength = 0;
                Efficient = 0;
            }

            public EncodeResult(Dictionary<string, string> codons, double averageLength, double efficient)
            {
                Codons = codons;
                AverageLength = averageLength;
                Efficient = efficient;
            }
        }


        /// <summary>
        /// 使用特定码集对信源进行转换
        /// </summary>
        /// <param name="msg">信源字符序列</param>
        /// <param name="codeSet">码集</param>
        /// <returns></returns>
        public static string Encode(string msg, Dictionary<string, string> codeSet)
        {
            foreach (var character in codeSet.Keys)
            {
                msg = msg.Replace(character, codeSet[character]);
            }
            return msg;
        }


        /// <summary>
        /// 霍夫曼编码(二进制)
        /// </summary>
        /// <param name="probabilities">信源概率分布数组</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static (string[] Codons, double AverageLength, double Efficient) Hoffman(double[] probabilities)
        {
            // 检查输入参数
            foreach (var probability in probabilities)
            {
                if (probability < 0 || probability > 1)
                {
                    throw new ArgumentException("符号概率不能小于0，也不能大于1!");
                }
            }

            // 迭代编码
            if (probabilities.Length == 2)
            {
                if (probabilities[0] >= probabilities[1])
                {
                    return (new string[] { "1", "0" }, 1, Common.Math.Entropy(probabilities));
                }
                else
                {
                    return (new string[] { "0", "1" }, 1, Common.Math.Entropy(probabilities));
                }
            }
            else
            {
                var codons = new string[probabilities.Length];
                var averageLength = 0.0;
                var efficient = 0.0;

                // 将概率数组降序排列
                var probabilitiesSortResult = SortPlus.Sort(probabilities);

                // 合并概率最小的两项
                var combineProbabilities = new double[probabilitiesSortResult.Data.Length - 1];
                Array.Copy(probabilitiesSortResult.Data, combineProbabilities, combineProbabilities.Length - 1);
                combineProbabilities[^1] = probabilitiesSortResult.Data[^2] + probabilitiesSortResult.Data[^1];

                // 降序排列合并后的数组
                var combineProbabilitiesSortResult = SortPlus.Sort(combineProbabilities);
                var combineProbabilitiesEncodingResult = Hoffman(combineProbabilitiesSortResult.Data);
                var combineProbabilitiesCondons = new string[combineProbabilities.Length];
                for (int i = 0; i < combineProbabilitiesSortResult.Index.Length; i++)
                {
                    combineProbabilitiesCondons[combineProbabilitiesSortResult.Index[i]] = combineProbabilitiesEncodingResult.Codons[i];
                }

                // 合并编码
                Array.Copy(combineProbabilitiesCondons, codons, codons.Length - 2);
                codons[^2] = combineProbabilitiesCondons.Last() + "1";
                codons[^1] = combineProbabilitiesCondons.Last() + "0";

                // 计算平均码长和编码效率
                averageLength = Common.Math.SourceEncodingAverageLength(probabilitiesSortResult.Data, codons);
                efficient = Common.Math.SourceEncodingEfficient(Common.Math.Entropy(probabilitiesSortResult.Data), averageLength);

                // 还原概率数组的原始顺序
                var resCodons = new string[codons.Length];
                for (int i = 0; i < codons.Length; i++)
                {
                    resCodons[probabilitiesSortResult.Index[i]] = codons[i];
                }
                codons = resCodons;

                return (codons, averageLength, efficient);
            }
        }


        /// <summary>
        /// 霍夫曼编码(二进制)
        /// </summary>
        /// <param name="countResult">信源统计结果</param>
        /// <returns></returns>
        public static EncodeResult Hoffman(Counter.CountResult countResult)
        {
            var res = new EncodeResult();
            var characters = countResult.Symbols.Keys.ToArray();
            var characterProbabilities = new double[characters.Length];
            for(int i = 0; i < characters.Length; i++)
            {
                characterProbabilities[i] = countResult.Symbols[characters[i]];
            }

            string[] codons;
            (codons, res.AverageLength, res.Efficient) = Hoffman(characterProbabilities);

            for(int i = 0; i < characters.Length; i++)
            {
                res.Codons.Add($"{characters[i]}", codons[i]);
            }

            return res;
        }


        /// <summary>
        /// 香农编码(二进制)
        /// </summary>
        /// <param name="probabilities">信源概率分布数组</param>
        /// <returns></returns>
        public static (string[] Codons, double AverageLength, double Efficient) Shannon(double[] probabilities)
        {
            var resEfficient = 0.0;
            var resAverageLength = 0.0;
            var resCodons = new string[probabilities.Length];

            // 检查输入参数
            foreach (var probability in probabilities)
            {
                if (probability < 0 || probability > 1)
                {
                    throw new ArgumentException("符号概率不能小于0，也不能大于1!");
                }
            }

            // 将输入概率数组降序排列
            var probabilitiesSum = 0.0;
            var probabilitiesSortedResult = SortPlus.Sort(probabilities);
            var codons = new string[probabilitiesSortedResult.Data.Length];
            for (int i = 0; i < probabilitiesSortedResult.Data.Length; i++)
            {
                codons[i] = DoubleToBinstr(probabilitiesSum, (int)System.Math.Ceiling((-1) * System.Math.Log2(probabilitiesSortedResult.Data[i])));
                probabilitiesSum += probabilitiesSortedResult.Data[i];
            }

            // 还原原始编码顺序
            for(int j = 0; j < probabilitiesSortedResult.Index.Length; j++)
            {
                resCodons[probabilitiesSortedResult.Index[j]] = codons[j];
            }

            // 计算平均码长和编码效率
            resAverageLength = Common.Math.SourceEncodingAverageLength(probabilities, resCodons);
            resEfficient = Common.Math.SourceEncodingEfficient(Common.Math.Entropy(probabilities), resAverageLength);
            return (resCodons, resAverageLength, resEfficient);
        }


        /// <summary>
        /// 香农编码(二进制)
        /// </summary>
        /// <param name="countResult">信源统计结果</param>
        /// <returns></returns>
        public static EncodeResult Shannon(Counter.CountResult countResult)
        {
            var res = new EncodeResult();
            var characters = countResult.Symbols.Keys.ToArray();
            var characterProbabilities = new double[characters.Length];
            for (int i = 0; i < characters.Length; i++)
            {
                characterProbabilities[i] = countResult.Symbols[characters[i]];
            }

            string[] codons;
            (codons, res.AverageLength, res.Efficient) = Shannon(characterProbabilities);

            for (int i = 0; i < characters.Length; i++)
            {
                res.Codons.Add($"{characters[i]}", codons[i]);
            }

            return res;
        }


        /// <summary>
        /// 费诺编码(二进制)
        /// </summary>
        /// <param name="probabilities">信源概率分布数组</param>
        /// <returns></returns>
        public static (string[] Codons, double AverageLength, double Efficient) Feno(double[] probabilities)
        {
            // 检查输入参数
            foreach (var probability in probabilities)
            {
                if (probability < 0 || probability > 1)
                {
                    throw new ArgumentException("符号概率不能小于0，也不能大于1!");
                }
            }

            // 迭代编码
            if(probabilities.Length == 1) { throw new ArgumentException("概率数组长度不能小于2!"); }
            else if (probabilities.Length == 2)
            {
                if (probabilities[0] >= probabilities[1]) { return (new string[] { "1", "0" }, 1, Common.Math.Entropy(probabilities)); }
                else { return (new string[] { "0", "1" }, 1, Common.Math.Entropy(probabilities)); }
            }
            else
            {
                // 将输入概率数组降序排列
                var probabilitiesSortedResult = SortPlus.Sort(probabilities);
                var sortedCodons = new string[probabilitiesSortedResult.Data.Length];

                var resEfficient = 0.0;
                var resAverageLength = 0.0;
                var resCodons = new string[probabilities.Length];

                // 将输入概率数组均分为两份
                var probabilitiesEqualDivideResult = EqualDivide(probabilities);

                // 分别对上下两部分进行编码
                if(probabilitiesEqualDivideResult.Diff >= 0.0)
                {
                    for(int i = 0; i <= probabilitiesEqualDivideResult.Index; i++) { sortedCodons[i] = "1"; }
                    for(int i = probabilitiesEqualDivideResult.Index + 1; i < sortedCodons.Length; i++) { sortedCodons[i] = "0"; }
                }
                else
                {
                    for (int i = 0; i <= probabilitiesEqualDivideResult.Index; i++) { sortedCodons[i] = "0"; }
                    for (int i = probabilitiesEqualDivideResult.Index + 1; i < sortedCodons.Length; i++) { sortedCodons[i] = "1"; }
                }

                // 对上半部分进行编码
                // 只有当划分的两部分概率数组长度均大于2时，才进行编码
                if(probabilitiesEqualDivideResult.Upper.Length >= 2)
                {
                    var probabilitiesUpperEncodingResult = Feno(probabilitiesEqualDivideResult.Upper);
                    for (int i = 0; i < probabilitiesUpperEncodingResult.Codons.Length; i++)
                    {
                        sortedCodons[i] += probabilitiesUpperEncodingResult.Codons[i];
                    }
                }

                // 对下半部分进行编码
                if (probabilitiesEqualDivideResult.Lower.Length >=2 )
                {
                    var probabilitiesLowerEncodingResult = Feno(probabilitiesEqualDivideResult.Lower);
                    for (int i = 0; i < probabilitiesLowerEncodingResult.Codons.Length; i++)
                    {
                        sortedCodons[i + probabilitiesEqualDivideResult.Index + 1] += probabilitiesLowerEncodingResult.Codons[i];
                    }
                }

                // 还原输入概率数组的顺序
                for (int i = 0; i < sortedCodons.Length; i++)
                {
                    resCodons[probabilitiesSortedResult.Index[i]] = sortedCodons[i];
                }

                // 计算平均码长和编码效率
                resAverageLength = Common.Math.SourceEncodingAverageLength(probabilities, resCodons);
                resEfficient = Common.Math.SourceEncodingEfficient(Common.Math.Entropy(probabilities), resAverageLength);
                return (resCodons, resAverageLength, resEfficient);
            }
        }


        /// <summary>
        /// 费诺编码(二进制)
        /// </summary>
        /// <param name="countResult">信源统计结果</param>
        /// <returns></returns>
        public static EncodeResult Feno(Counter.CountResult countResult)
        {
            var res = new EncodeResult();
            var characters = countResult.Symbols.Keys.ToArray();
            var characterProbabilities = new double[characters.Length];
            for (int i = 0; i < characters.Length; i++)
            {
                characterProbabilities[i] = countResult.Symbols[characters[i]];
            }

            string[] codons;
            (codons, res.AverageLength, res.Efficient) = Feno(characterProbabilities);

            for (int i = 0; i < characters.Length; i++)
            {
                res.Codons.Add($"{characters[i]}", codons[i]);
            }

            return res;
        }


        /// <summary>
        /// 浮点数转二进制字符串
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        static string DoubleToBinstr(double d, int len = 8)
        {
            var res = string.Empty;
            if ((d >= 0) && (d <= 1))
            {
                for (int i = 0; i < len; i++)
                {
                    if(d * 2 >= 1) { res += "1"; }
                    else { res += "0"; }
                    d = d * 2 - (int)(d * 2);
                }
            }
            return res;
        }


        /// <summary>
        /// 平均分配一个数组（降序排列）
        /// </summary>
        /// <param name="d">待均分的数组</param>
        /// <returns></returns>
        static DivideResult EqualDivide(double[] d)
        {
            if(d.Length == 0) { throw new ArgumentException("输入数组不能为空!"); }
            else if(d.Length == 1) { return new DivideResult(0, d[0], d, Array.Empty<double>()); }
            else
            {
                int minDifferenceIndex = 0;
                double upperSum = 0.0, lowerSum = 0.0, minDifference = d[0];
                for(int i = 0; i < d.Length - 1; i++)
                {
                    lowerSum = 0.0;
                    upperSum += d[i];

                    // 计算下半部分之和
                    for(int j = i + 1; j < d.Length; j++)
                    {
                        lowerSum += d[j];
                    }

                    // 计算上下两部分之差
                    if(System.Math.Abs(upperSum - lowerSum) < System.Math.Abs(minDifference))
                    {
                        minDifference = upperSum - lowerSum;
                        minDifferenceIndex = i;
                    }
                }
                return new DivideResult(minDifferenceIndex, minDifference, d[0..(minDifferenceIndex + 1)], d[(minDifferenceIndex + 1)..d.Length]);
            }
        }


        /// <summary>
        /// 数组均分结果
        /// </summary>
        struct DivideResult
        {
            public int Index { get; set; }          // 均分位置索引（含该索引）
            public double Diff { get; set; }        // 上下两部分的差值
            public double[] Upper { get; set; }     // 上半部分
            public double[] Lower { get; set; }     // 下半部分

            public DivideResult(int index, double diff, double[] upper, double[] lower)
            {
                Index = index;
                Diff = diff;
                Upper = upper;
                Lower = lower;
            }
        }
    }
}
