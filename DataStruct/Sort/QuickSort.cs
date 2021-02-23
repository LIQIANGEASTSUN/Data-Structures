using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.Log;

namespace DataStruct.Sort
{
    // 快速排序
    class QuickSort
    {
        public void Test()
        {
            int[] arr = new int[] { 6, 1, 8, 0, 0, 9, 5, 3, 7 }; // 0, 1, 3, 5, 6, 7, 8, 9
            QuickSortFunc(arr);
            LogArr.Log(arr);
        }

        public void QuickSortFunc(int[] arr)
        {
            QuickSortFunc(arr, 0, arr.Length - 1);
        }

        private void QuickSortFunc(int[] arr, int low, int high)
        {
            if (low >= high)
            {
                return;
            }
            int mid = Partition(arr, low, high);
            QuickSortFunc(arr, low, mid);
            QuickSortFunc(arr, mid + 1, high);
        }

        private int Partition(int[] arr, int low, int high)
        {
            // 取最左侧的数据为轴点
            int pivot = arr[low];
            while (low < high)
            {
                while (low < high && pivot <= arr[high])
                {
                    --high;
                }
                arr[low] = arr[high];

                while (low < high && arr[low] < pivot)
                {
                    ++low;
                }
                arr[high] = arr[low];
            }
            arr[low] = pivot;

            return low;
        }
    }
}
