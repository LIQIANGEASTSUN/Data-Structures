using System;
using DataStruct.Log;

namespace DataStruct.Sort
{
    // 希尔排序
    class ShellSort
    {
        public void Test()
        {
            int[] arr = new int[] { 6, 9, 2, 5, 0, 1, 7, 3, 8 };
            ShellSort1(arr);

            LogArr.Log(arr);
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                if (arr[i] > arr[i + 1])
                {
                    Console.WriteLine("Error");
                }
            }
        }

        /// <summary>
        /// 希尔排序
        /// </summary>
        /// <param name="arr"></param>
        private void ShellSort1(int[] arr)
        {
            for (int gap = arr.Length / 2; gap > 0; gap /= 2)
            {
                InsertSort(arr, gap);
            }
        }

        /// <summary>
        /// 插入排序一个乱序的数组
        /// 下面部分是插入排序的一个衍变
        /// 将 gap 赋值 为 1 就是一个标准的插入排序
        /// </summary>
        /// <param name="arr"></param>
        private void InsertSort(int[] arr, int gap)
        {
            for (int i = gap; i < arr.Length; ++i)
            {
                Insert(arr, i, gap);
            }
        }

        /// <summary>
        /// arr 前 i - 1 个数据，每隔 gap 个位置是已经排序的序列，将第 i 个数排序到正确位置
        /// 每隔 gap 个位置是已经排序的序列，解释如下
        /// 0，0 + gap, 0 + 2 * gap, ... , 0 + N * gap  这些位置的数是已排好序的数列  且 0 + N * gap   < i
        /// 1，1 + gap, 1 + 2 * gap, ... , 1 + N * gap  这些位置的数是已排好序的数列  且 1 + N * gap   < i
        /// m, m + gap, m + 2 * gap, ... , m + N * gap  这些位置的数是已排好序的数列  且 (m < gap) && (m + N * gap < i)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="i"></param>
        private void Insert(int[] arr, int i, int gap)
        {
            int j = i;
            int temp = arr[j];
            while ((j - gap) >= 0 && temp < arr[j - gap])
            {
                arr[j] = arr[j - gap];
                j -= gap;
            }
            arr[j] = temp;
        }

    }
}
