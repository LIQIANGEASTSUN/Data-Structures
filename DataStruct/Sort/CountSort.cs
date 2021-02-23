using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.Log;

namespace DataStruct.Sort
{
    // 计数排序
    class CountSort
    {
        public void Test()
        {
            int[] arr = new int[] { 108, 100, 105, 110, 105, 105, 100 };
            arr = CountSortFunc(arr);
            LogArr.Log(arr, 0, arr.Length);
        }

        public int[] CountSortFunc(int[] arr)
        {
            int max = arr[0];
            int min = arr[0];
            // 获取数组中最大和最小的值
            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }

            // 实例数组 { 108, 100, 105, 110, 105, 105, 100 }
            // 以 最大值(110) - 最小值(100) = 10 的区间数作为 统计数组的长度
            int length = max - min + 1;
            int[] countArr = new int[length];
            for (int i = 0; i < arr.Length; ++i)
            {
                // arr[i] 和 min = 100 的插值作为 统计数组的 索引下标
                // 如 105 - 100 = 5 作为索引下标
                // 标记一：
                int index = arr[i] - min;
                countArr[index]++;
            }

            // 结果数组的长度取原数组 arr.Length
            int[] resultArr = new int[arr.Length];
            int resultIndex = 0;
            for (int i = 0; i < countArr.Length; ++i)
            {
                // 遍历统计数组，只要是数值大于 0 的都添加进 结果数组
                while (countArr[i] > 0)
                {
                    // i 为索引 = arr[下标] - min，如不理解找上边代码注释 //标记一： 
                    // 所以此处 i + min 即转换为 arr[下标] 原数据的值
                    resultArr[resultIndex] = i + min;
                    countArr[i]--;
                    resultIndex++;
                }
            }

            return resultArr;
        }
    }
}
