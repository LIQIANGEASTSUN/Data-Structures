using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.Log;

namespace DataStruct.Sort
{
    // 插入排序
    class InsertSort
    {
        public void Test()
        {
            int[] arr = new int[] { 6, 2, 5, 0, 1 };
            InsertSortFunc(arr);
            LogArr.Log(arr);
        }

        private void InsertSortFunc(int[] arr)
        {
            for (int i = 1; i < arr.Length; ++i)
            {
                int temp = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    --j;
                }

                arr[j + 1] = temp;
            }
        }
    }
}
