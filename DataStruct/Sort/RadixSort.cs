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


逻辑二 部分
for (int i = 1; i < countArr.Length; ++i)
{
    // 计算每个索引上的数据存放的最大位置下标
    countArr[i] += countArr[i - 1];
}

先说第一轮排序后的结果
90，1，23，83，33，324，45，876， 26， 567，67，57，78

最终
countArr[0] = 1   ：个位为0 的数有 1 个，90， 最后一个，个位包含 0 的 90 应该排在第 (1 - 1） = 0 个位置
countArr[1] = 2   ：个位为1 的数有 1 个， 1， 最后一个，个位包含 1 的 1 应该排在第 (2 - 1） = 1 个位置
countArr[2] = 2   ：个位为2 的数有 0 个，     最后一个，个位包含 2 的应该排在第 (2 - 1） = 2 个位置，其实没有个位包含 2 的

说一下：
个位为0 的90，占了 1 个位置
个位为1 的1，占了 1 个位置
个位为3 的23，83，33，占了 3 个位置，所以最后一个，个位包含 3 的 33 应该排在第 （5 - 1）= 4 个位置

countArr[3] = 5   ：个位为3 的数有 3 个， 23，83，33， 最后一个，个位包含 3 的 33 应该排在第 （5 - 1）= 4 个位置
countArr[4] = 6   ：个位为4 的数有 1 个， 324，        最后一个，个位包含 4 的 324 应该排在第 （6 - 1）= 5 个位置
countArr[5] = 7   ：个位为5 的数有 1 个， 45，         最后一个，个位包含 5 的 45 应该排在第 （7 - 1）= 6 个位置
countArr[6] = 9   ：个位为6 的数有 2 个， 876， 26     最后一个，个位包含 6 的 26 应该排在第 （9 - 1）= 8 个位置
countArr[7] = 12   ：个位为7 的数有 3 个， 567，67，57 最后一个，个位包含 7 的 57 应该排在第 （12 - 1）= 11 个位置
countArr[8] = 13   ：个位为8 的数有 1 个， 78          最后一个，个位包含 8 的 78 应该排在第 （13 - 1）= 12 个位置
countArr[9] = 13   ：个位为9 的数有 0 个，             最后一个，个位包含 9 的应该排在第 （13 - 1）= 12 个位置，其实没有个位包含 9 的



*/
