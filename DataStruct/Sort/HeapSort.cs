using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.Heap;

namespace DataStruct.Sort
{
    class HeapSort
    {

        public void Test()
        {
            List<int> list = new List<int>() { 4, 6, 8, 5, 9, 3, 1, 0, 20, 10, 16 };
            HeapSortFunc(list);

            Log.LogArr.Log(list.ToArray());
        }

        public void HeapSortFunc(List<int> list)
        {
            Heap.Heap heap = new Heap.Heap();
            heap.list = list;
            // 构建堆
            heap.HeapCreate();

            // 初始尾部元素为末尾元素 int i = list.Count - 1
            // 一：每次将堆顶(最大元素)和尾部元素(--i)进行交换
            // 二：尾部前边的数据重新构建为堆，再次执行 一、二
            //     直到所有元素都执行结束
            for (int i = list.Count - 1; i >= 0; --i)
            {
                // 将堆顶元素与末尾元素进行交换
                int temp = list[0];
                list[0] = list[i];
                list[i] = temp;

                // 将新的堆顶元素下虑
                heap.PercolateDown(list, 0, i);
            }
        }

    }
}
