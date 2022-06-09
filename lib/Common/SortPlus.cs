using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            SortResult res = new(array, new int[array.Length]);

            // 拷贝原数组
            var arrayCopy = new double[array.Length];
            Array.Copy(array, arrayCopy, arrayCopy.Length);

            // 对数组进行排序
            if (order == SortOrder.Ascending) 
            { 
                Array.Sort(array); 
            }
            else 
            {
                Array.Sort(array);
                Array.Reverse(array); 
            }

            // 遍历拷贝数组，获取索引
            for(int i = 0; i < array.Length; i++)
            {
                for(int j = 0; j < arrayCopy.Length; j++)
                {
                    if(array[i] == arrayCopy[j])
                    {
                        res.Index[i] = j;
                        arrayCopy[j] = -65535;
                        break;
                    }
                }
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
