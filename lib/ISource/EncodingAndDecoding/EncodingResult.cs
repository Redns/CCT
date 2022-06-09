namespace CCT.ISource.EncodingAndDecoding
{
    /// <summary>
    /// 信源编码结果
    /// </summary>
    public class EncodingResult
    {
        public string[] Codons { get; set; }            // 码字 
        public double AverageLength { get; set; }       // 平均码长
        public double Efficient { get; set; }           // 编码效率

        public EncodingResult()
        {
            Codons = Array.Empty<string>();
            AverageLength = 0;
            Efficient = 0;
        }

        public EncodingResult(string[] codons, double averageLength, double efficient)
        {
            Codons = codons;
            AverageLength = averageLength;
            Efficient = efficient;
        }
    }
}
