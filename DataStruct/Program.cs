using System;
using System.Collections.Generic;

namespace DataStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            //HeapTest();
            BinTreeTest();

            Console.ReadLine();
        }

        // 堆测试
        private static void HeapTest()
        {
            DataStruct.Heap.Heap heap = new DataStruct.Heap.Heap();
            heap.TestInsert();
            heap.TestHeapCreate();
        }

        private static void BinTreeTest()
        {
            DataStruct.BinTree.BinTreeTest.Test();
        }

        private static void BSTreeTest()
        {
            BTree.BSTreeTest.Test();
        }
    }
}
