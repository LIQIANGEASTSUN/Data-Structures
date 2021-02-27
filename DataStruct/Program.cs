﻿using System;
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

            AVLTest();

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

    }
}
