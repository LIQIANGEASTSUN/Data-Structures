using DataStruct.Log;
using System;

namespace DataStruct.Sort
{
    // 插入排序
    class InsertSort
    {
        public void Test()
        {
            Console.WriteLine("InsertSort");

            int[] arr = new int[] { 6, 2, 5, 0, 1, 9, 3, 7, 4 };
            InsertSortAll(arr);
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
        /// 插入排序一个乱序的数组
        /// </summary>
        /// <param name="arr"></param>
        private void InsertSortAll(int[] arr)
        {
            for (int i = 1; i < arr.Length; ++i)
            {
                Insert(arr, i);
            }
        }

        /// <summary>
        /// arr 前 i - 1 数是已经排序的序列，将第 i 个数排序到正确位置
        /// </summary>
        /// <param name="arr">已经排序的序列</param>
        /// <param name="i"></param>
        private void Insert(int[] arr, int i)
        {
            int j = i;
            int temp = arr[j];
            while ((j - 1) >= 0 && temp < arr[j - 1])
            {
                arr[j] = arr[j - 1];
                --j;
            }
            arr[j] = temp;
        }

    }
}
