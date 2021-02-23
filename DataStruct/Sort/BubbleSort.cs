using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.Log;

namespace DataStruct.Sort
{
    // 冒泡排序
    class BubbleSort
    {
        public void Test()
        {
            //int[] arr = new int[] { 5, 3, 6, 1, 0};
            int[] arr = new int[] { 9, 1, 6, 0 };

            LogArr.Log(arr);
            BubbleSortFunc(arr);
            LogArr.Log(arr);
        }

        public void BubbleSortFunc(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                for (int j = 0; j < arr.Length - i - 1; ++j)
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
