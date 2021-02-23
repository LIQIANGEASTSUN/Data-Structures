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
        private List<int> list = new List<int>();

        public Heap()
        {
            //list = new List<int>() { 4, 6, 8, 15, 0};

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


            LogArr.Log(list.ToArray(), 0, list.Count);

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
            PercolateUp(list.Count - 1);
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
            PercolateDown(0);

            return max;
        }

        // 上虑
        private void PercolateUp(int index)
        {
            if (index >= list.Count)
            {
                return;
            }

            // 直到抵达堆顶
            while (0 < index)
            {
                // 获取 index 的父节点
                int parentIndex = Parent(index);
                // 逆序(父节点<子节点)则互换父/子节点的值
                if (list[parentIndex] >= list[index])
                {
                    break;
                }

                int temp = list[parentIndex];
                list[parentIndex] = list[index];
                list[index] = temp;

                index = parentIndex;
            }
        }

        // 下虑
        private void PercolateDown(int index)
        {
            if (index >= list.Count)
            {
                return;
            }

            // 令 index 位置的值 为其自身和子节点中最大者
            int maxIndex = 0;
            while (index != (maxIndex = ProperParent(index)))
            {
                // index 位置的值，不是其自身和子节点中的最大者，则互换自身与最大节点的值
                int temp = list[maxIndex];
                list[maxIndex] = list[index];
                list[index] = temp;

                // 互换位置，继续下虑
                index = maxIndex;
            }
        }

        private int ProperParent(int index)
        {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;

            if (list.Count > leftChildIndex)
            {
                index = list[index] >= list[leftChildIndex] ? index : leftChildIndex;
            }
            if (list.Count > rightChildIndex)
            {
                index = list[index] >= list[rightChildIndex] ? index : rightChildIndex;
            }
            return index;
        }

        private void LogChild()
        {
            for (int i = 0; i < list.Count; ++i)
            {
                int leftChildIndex = i * 2 + 1;
                int rightChildIndex = i * 2 + 2;

                if (leftChildIndex >= list.Count && rightChildIndex >= list.Count)
                {
                    continue;
                }

                string msg = list[i].ToString();
                if (leftChildIndex < list.Count)
                {
                    msg = string.Format("{0} lc:{1}  {2}", msg, list[leftChildIndex], list[i] >= list[leftChildIndex]);
                }
                if (rightChildIndex < list.Count)
                {
                    msg = string.Format("{0} rc:{1}  {2}", msg, list[rightChildIndex], list[i] >= list[rightChildIndex]);
                }
                Console.WriteLine(msg);
            }
        }
    }
}
