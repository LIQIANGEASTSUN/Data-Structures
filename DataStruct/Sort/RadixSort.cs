using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.Log;

namespace DataStruct.Sort
{
    // 基数排序
    class RadixSort
    {
        public void Test()
        {
            int[] arr = new int[] { 543, 159, 343, 287, 694, 315, 873, 96 };
            RadixSortFunc(arr);
            LogArr.Log(arr);
        }

        public void RadixSortFunc(int[] arr)
        {
            int maxBit = 1;
            int value = 10;
            // 获取数组中最大元素位数
            for (int i = 0; i < arr.Length; ++i)
            {
                while (arr[i] >= value)
                {
                    value *= 10;
                    ++maxBit;
                }
            }

            int digit = 1;
            int[] tempArr = new int[arr.Length];
            while (maxBit > 0)
            {
                maxBit--;
                int[] countArr = new int[10];

                for (int i = 0; i < arr.Length; ++i)
                {
                    // arr[i] 的 digit位(1 个位、10 十位、100 百位)上的值
                    // 以此值作为 countArr 的索引，索引数+1
                    int index = arr[i] / digit % 10;
                    countArr[index]++;
                }

                for (int i = 1; i < countArr.Length; ++i)
                {
                    // 计算每个索引上的数据存放的最大位置下标
                    countArr[i] += countArr[i - 1];
                }

                for (int i = arr.Length - 1; i >= 0; --i)
                {
                    int index = arr[i] / digit % 10;
                    tempArr[countArr[index] - 1] = arr[i];
                    countArr[index]--;
                }

                // 将临时数据拷贝回 arr
                for (int i = 0; i < tempArr.Length; ++i)
                {
                    arr[i] = tempArr[i];
                }
                digit *= 10;
            }
        }
    }
}
