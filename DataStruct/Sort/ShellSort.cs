using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void ShellSort1(int[] arr)
        {
            for (int gap = arr.Length / 2; gap > 0; gap /= 2)
            {
                /**下面部分是插入排序的一个衍变，基本逻辑就是插入排序*********/
                for (int i = gap; i < arr.Length; ++i)
                {
                    int j = i;
                    int temp = arr[i];
                    while (j - gap >= 0 && temp < arr[j - gap])
                    {
                        arr[j] = arr[j - gap];
                        j -= gap;
                    }
                    arr[j] = temp;
                }
                /**上面部分是插入排序的一个衍变，基本逻辑就是插入排序*********/
            }
        }

    }
}
