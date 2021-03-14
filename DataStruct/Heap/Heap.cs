using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.Log;

namespace DataStruct.Heap
{
    public class HeapTest
    {
        public static void Test()
        {
            Heap<int> heap = new Heap<int>();
            heap.Insert(4);
            heap.Insert(6);
            heap.Insert(8);
            heap.Insert(5);
            heap.Insert(9);
            heap.Insert(3);
            heap.Insert(1);
            heap.Insert(0);
            heap.Insert(20);
            heap.Insert(10);
            heap.Insert(16);

            HeapHelper<int>.Log(heap.list);

            TestHeapCreate();
        }

        public static void TestHeapCreate()
        {
            Heap<int> heap = new Heap<int>();
            Random random = new Random();
            heap.list = new List<int>() { 4, 6, 8, 5, 9, 3, 1, 0, 20, 10, 16 };
            for (int i = 0; i < 30; ++i)
            {
                int value = random.Next(10, 500);
                heap.list.Add(value);
            }
            heap.HeapCreate();

            Console.WriteLine();
            HeapHelper<int>.Log(heap.list);
        }
    }

    class Heap<T> where T : IComparable<T>
    {
        // 此处使用 List 是为了偷懒，因为涉及到 插入 insert 和删除 delet
        // 如果使用数组首先开辟多大空间不确定假设开辟 N 个空间，则还需要
        // 记录当前已经使用到哪个下标索引了记为 size。且当 size >= N 时
        // 还需要手动再次开辟空间
        public List<T> list = new List<T>();

        public Heap() {    }

        private int ParentIndex(int index)
        {
            index = (index - 1) >> 1;
            return index;
        }

        public void Insert(T value)
        {
            list.Add(value);
            PercolateUp(list, list.Count - 1);
        }

        public T GetMax()
        {
            if (list.Count <= 0)
            {
                return default(T);
            }
            return list[0];
        }

        // 删除最大元素
        public T DelMax()
        {
            if (list.Count <= 0)
            {
                return default(T);
            }

            T max = list[0];
            // 删除堆顶元素，将末元素填补到堆顶。
            list[0] = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);

            // 对堆顶元素下虑
            PercolateDown(list, 0, list.Count);

            return max;
        }

        // 批量建堆
        public void HeapCreate()
        {
            // 批量建堆思路为从最后一个非叶子节点开始下虑，一直到跟节点结束
            // 所有非叶子节点执行完下虑堆自然而成
            for (int i = (list.Count / 2) - 1; i >= 0; --i)
            {
                PercolateDown(list, i, list.Count);
            }
        }

        // 上虑
        private void PercolateUp(List<T> dataList, int index)
        {
            if (index >= dataList.Count)
            {
                return;
            }

            // 直到抵达堆顶
            while (0 < index)
            {
                // 获取 index 的父节点
                int parentIndex = ParentIndex(index);
                // 逆序(父节点<子节点)则互换父/子节点的值
                if (dataList[parentIndex].CompareTo(dataList[index]) >= 0)
                {
                    break;
                }

                T temp = dataList[parentIndex];
                dataList[parentIndex] = dataList[index];
                dataList[index] = temp;

                index = parentIndex;
            }
        }

        // 下虑
        public void PercolateDown(List<T> dataList, int index, int length)
        {
            if (index >= dataList.Count)
            {
                return;
            }

            // 令 index 位置的值 为自身和子节点中最大者
            int maxIndex = 0;
            while (index != (maxIndex = ProperParent(dataList, index, length)))
            {
                // index 位置的值，比子节点的值小，则互换自身与较大子节点的值
                T temp = dataList[maxIndex];
                dataList[maxIndex] = dataList[index];
                dataList[index] = temp;

                // 互换位置，继续下虑
                index = maxIndex;
            }
        }

        // 自己和左右两个子节点中最大者
        public int ProperParent(List<T> dataList, int index, int length)
        {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;

            if (length > leftChildIndex)
            {
                index = dataList[index].CompareTo(dataList[leftChildIndex]) >= 0 ? index : leftChildIndex;
            }
            if (length > rightChildIndex)
            {
                index = dataList[index].CompareTo(dataList[rightChildIndex]) >= 0 ? index : rightChildIndex;
            }
            return index;
        }

    }
}
