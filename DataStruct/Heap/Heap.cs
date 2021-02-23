using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.Log;

namespace DataStruct.Heap
{
    class Heap
    {
        // 此处使用 List 是为了偷懒，因为涉及到 插入 insert 和删除 delet
        // 如果使用数组首先开辟多大空间不确定假设开辟 N 个空间，则还需要
        // 记录当前已经使用到哪个下标索引了记为 size。且当 size >= N 时
        // 还需要手动再次开辟空间
        public List<int> list = new List<int>();

        public Heap()
        {

        }

        public void TestInsert()
        {
            Insert(4);
            Insert(6);
            Insert(8);
            Insert(5);
            Insert(9);
            Insert(3);
            Insert(1);
            Insert(0);
            Insert(20);
            Insert(10);
            Insert(16);

            LogHeap.Log(list.ToArray());
        }

        public void TestHeapCreate()
        {
            list = new List<int>() { 4, 6, 8, 5, 9, 3, 1, 0, 20, 10, 16 };
            HeapCreate();

            LogHeap.Log(list.ToArray());
        }

        private int Parent(int index)
        {
            index = (index - 1) >> 1;
            return index;
        }

        public void Insert(int value)
        {
            list.Add(value);
            PercolateUp(list, list.Count - 1);
        }

        public int GetMax()
        {
            if (list.Count <= 0)
            {
                return -1;
            }
            return list[0];
        }

        // 删除最大元素
        public int DelMax()
        {
            if (list.Count <= 0)
            {
                return -1;
            }

            int max = list[0];
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
        private void PercolateUp(List<int> dataList, int index)
        {
            if (index >= dataList.Count)
            {
                return;
            }

            // 直到抵达堆顶
            while (0 < index)
            {
                // 获取 index 的父节点
                int parentIndex = Parent(index);
                // 逆序(父节点<子节点)则互换父/子节点的值
                if (dataList[parentIndex] >= dataList[index])
                {
                    break;
                }

                int temp = dataList[parentIndex];
                dataList[parentIndex] = dataList[index];
                dataList[index] = temp;

                index = parentIndex;
            }
        }

        // 下虑
        public void PercolateDown(List<int> dataList, int index, int length)
        {
            if (index >= dataList.Count)
            {
                return;
            }

            // 令 index 位置的值 为其自身和子节点中最大者
            int maxIndex = 0;
            while (index != (maxIndex = ProperParent(dataList, index, length)))
            {
                // index 位置的值，比子节点的值小，则互换自身与最大节点的值
                int temp = dataList[maxIndex];
                dataList[maxIndex] = dataList[index];
                dataList[index] = temp;

                // 互换位置，继续下虑
                index = maxIndex;
            }
        }

        // 自己和左右两个子节点中最大者
        public int ProperParent(List<int> dataList, int index, int length)
        {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;

            if (length > leftChildIndex)
            {
                index = dataList[index] >= dataList[leftChildIndex] ? index : leftChildIndex;
            }
            if (length > rightChildIndex)
            {
                index = dataList[index] >= dataList[rightChildIndex] ? index : rightChildIndex;
            }
            return index;
        }

    }
}
