using DataStruct.Log;
using System;

namespace DataStruct.Sort
{
    // 冒泡排序
    class BubbleSort
    {
        public void Test()
        {
            int[] arr = new int[] { 6, 9,  7, 0, 5,1, 2, 3, 4, 8};
            //int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            LogArr.Log(arr);
            BubbleSortOptimize(arr);
            LogArr.Log(arr);

            Console.WriteLine("==================================================");

            arr = new int[] { 6, 9, 7, 0, 5, 1, 2, 3, 4, 8 };
            BubbleSortFunc(arr);
            LogArr.Log(arr);
        }

        public void BubbleSortOptimize(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                bool swap = false;
                for (int j = 0; j < arr.Length - 1 - i; ++j)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swap = true;
                    }
                }
                if (!swap)
                {
                    break;
                }
            }
        }

        public void BubbleSortFunc(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                for (int j = 0; j < arr.Length - 1 - i; ++j)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
        
    }
}
