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
            int[] arr = new int[] { 1, 23, 90, 324, 567, 876, 26, 78, 83, 45, 67, 57, 33 };
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

                // 逻辑一
                for (int i = 0; i < arr.Length; ++i)
                {
                    // arr[i] 的 digit位(1 个位、10 十位、100 百位)上的值
                    // 以此值作为 countArr 的索引，索引数+1
                    int index = arr[i] / digit % 10;
                    countArr[index]++;
                }

                // 逻辑二
                for (int i = 1; i < countArr.Length; ++i)
                {
                    // 计算每个索引上的数据存放的最大位置下标
                    countArr[i] += countArr[i - 1];
                }

                for (int i = arr.Length - 1; i >= 0; --i)
                {
                    int index = arr[i] / digit % 10;
                    int v = countArr[index] - 1;
                    tempArr[v] = arr[i];
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


/*

逻辑讲解 

逻辑一 部分
for (int i = 0; i < arr.Length; ++i)
{
    // arr[i] 的 digit位(1 个位、10 十位、100 百位)上的值
    // 以此值作为 countArr 的索引，索引数+1
    int index = arr[i] / digit % 10;
    countArr[index]++;
}

按照每一位上的数值，统计该位置上的个数
1, 23, 90, 324, 567, 876, 26, 78, 83, 45, 67, 57, 33
以个位数为例：
index = 23 % 10 = 3
countArr[index]++;
就是记录 个位为 3 的添加一个

最终
countArr[0] = 1   ：个位为0 的数有 1 个， 90
countArr[1] = 1   ：个位为1 的数有 1 个， 1
countArr[2] = 0   ：个位为2 的数有 0 个，
countArr[3] = 3   ：个位为3 的数有 3 个， 23，83，33
countArr[4] = 1   ：个位为4 的数有 1 个， 324，
countArr[5] = 1   ：个位为5 的数有 1 个， 45，
countArr[6] = 2   ：个位为6 的数有 2 个， 876， 26
countArr[7] = 3   ：个位为7 的数有 3 个， 567，67，57
countArr[8] = 1   ：个位为8 的数有 1 个， 78
countArr[9] = 0   ：个位为9 的数有 0 个，




*/
