using DataStruct.BinTree;
using DataStruct.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Heap
{
    public class HeapLogNode<T> : LogNode<T>
    {
        public int index;

        public HeapLogNode(T t, int index)
        {
            this.element = t;
            this.index = index;
        }

        public override int ParentIndex()
        {
            int parentIndex = (index - 1) >> 1;
            return parentIndex;
        }

        public override int LeftChildIndex()
        {
            int leftChildIndex = index * 2 + 1;
            return leftChildIndex;
        }

        public override int RightChildIndex()
        {
            int rightChildIndex = index * 2 + 2;
            return rightChildIndex;
        }

        public override string ToString()
        {
            return element.ToString();
        }

        public override bool IsRedBlack()
        {
            return false;
        }

        public override Color GetColor()
        {
            return Color.Red;
        }
    }

    class HeapHelper<T>
    {
        public static void Log(List<int> list)
        {
            HeapLogNode<int>[] arr = new HeapLogNode<int>[list.Count];
            for (int i = 0; i < list.Count; ++i)
            {
                int value = list[i];
                HeapLogNode<int> heapLogNode = new HeapLogNode<int>(value, i);
                arr[i] = heapLogNode;
            }

            LogBinTree<int>.Log(arr);
        }
    }

}
