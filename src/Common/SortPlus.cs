namespace CCT.Common
{
    public class SortPlus
    {
        /// <summary>
        /// 排序方式
        /// </summary>
        public enum SortOrder
        {
            Ascending = 0,      // 升序
            Descending = 1,     // 降序
        }


        /// <summary>
        /// 对浮点数组进行排序
        /// </summary>
        /// <param name="array">待排序的数组</param>
        /// <param name="order">排序方式</param>
        /// <returns>排序结果</returns>
        public static SortResult Sort(double[] array, SortOrder order = SortOrder.Descending)
        {
            SortResult res = new(new double[array.Length], new int[array.Length]);

            Array.Copy(array, res.Data, array.Length);
            for(int i = 0; i < res.Index.Length; i++)
            {
                res.Index[i] = i;
            }

            if(order == SortOrder.Ascending)
            {
                Array.Sort(res.Data, res.Index);
            }
            else if(order == SortOrder.Descending)
            {
                Array.Sort(res.Data, res.Index);
                Array.Reverse(res.Data);
                Array.Reverse(res.Index);
            }

            return res;
        }
    }


    /// <summary>
    /// 排序结果
    /// </summary>
    public class SortResult
    {
        public double[] Data { get; set; }      // 排序后的数组
        public int[] Index { get; set; }        // 数据原始索引

        public SortResult(double[] data, int[] index)
        {
            Data = data;
            Index = index;
        }
    }
}
