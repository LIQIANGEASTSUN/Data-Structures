using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Sort
{
    // 归并排序
    class MergeSort
    {
        public void TestSort()
        {
            int[] arr = new int[] { 5, 9, 2, 0, 7, 6 };
            MergeSortFun(arr, 0, arr.Length - 1);
        }

        public void MergeSortFun(int[] arr, int left, int right)
        {
            int[] tempArr = new int[arr.Length];
            MergeSortFun(arr, left, right, tempArr);
        }

        private void MergeSortFun(int[] arr, int left, int right, int[] tempArr)
        {
            if (right <= left)
            {
                return;
            }

            int mid = (left + right) >> 1;

            MergeSortFun(arr, left, mid, tempArr);
            MergeSortFun(arr, mid + 1, right, tempArr);
            Merge(arr, left, mid, right, tempArr);
        }

        private void Merge(int[] arr, int left, int mid, int right, int[] tempArr)
        {
            // 将左侧数据拷贝到 tempArr
            for (int n = left; n <= mid; ++n)
            {
                tempArr[n] = arr[n];
            }

            //令 i ： 从 tempArr 第 left    个元素开始,到 mid(包含mid)结束
            //令 j ： 从 arr     第 mid + 1 个元素开始,到 right(包含right)结束
            //令 t ： 从 arr     第 left    个元素开始,到 right(包含right)结束
            //按照上面顺序依次比较，将 tempArr[i]、arr[j] 中较小者放入到 arr[t] 中, t++, 较小者为 tempArr[i]则 i++，较小者为 arr[j]则 j++; 

            for (int i = left, t = left, j = mid + 1; i <= mid;)
            {
                //如果 j > right,说明已经到末尾了，剩下的无需在比较，则直接将 tempArr中从 i 到 mid 的数据拷贝到 arr 中即可
                //否则 如果tempArr[i] < arr[j],将 tempArr[i] 拷贝到 arr 中

                //完整判断应该如下：
                //if ((i <= mid) && (j > right || tempArr[i] < arr[j]))
                //因为 for 循环中已经限制了 (i <= mid)，所以此处该限制可以省略
                if ((j > right) || (tempArr[i] <= arr[j]))
                {
                    arr[t] = tempArr[i];
                    ++t;
                    ++i;
                }

                //如果 j 右侧没有到末尾，则将 满足条件的 arr[j] < tempArr[i]，拷贝到 arr[t]
                //完整判断应该如下：
                //因为 for 循环已经限制了 (i <= mid)，所以此处 不会触发 i > mid 的条件
                //if ((j <= right) && ((i > mid) || arr[j] < tempArr[i]))
                if ((j <= right) && (arr[j] <= tempArr[i]))
                {
                    arr[t] = arr[j];
                    ++t;
                    ++j;
                }
            }
        }
    }
}
