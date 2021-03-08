using System;
using System.Collections.Generic;

namespace DataStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            //HeapTest();
            //BinTreeTest();
            //BSTreeTest();

            //AVLTest();

            //SplayTreeTest();

            //BTreeTest();

            //RBTreeTest();

            //ListTest();

            //StackTest();

            QueueTest();

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
            BSTree.BSTreeTest.Test();
        }

        private static void AVLTest()
        {
            Tree.AVLTree.AVLTest.Test();
        }

        private static void SplayTreeTest()
        {
            DataStruct.Tree.SplayTree.SplayTreeTest.Test();
        }

        private static void BTreeTest()
        {
            DataStruct.Tree.BTree.BTreeTest.Test();
        }

        private static void RBTreeTest()
        {
            DataStruct.Tree.RedBlackTree.RedBlackTest.Test();
        }

        private static void ListTest()
        {
            DataStruct.List.DataListTest.Test();
        }

        private static void StackTest()
        {
            DataStruct.Stack.StackTest.Test();
        }

        private static void QueueTest()
        {
            DataStruct.Queue.QueueTest.Test();
        }


    }
}
