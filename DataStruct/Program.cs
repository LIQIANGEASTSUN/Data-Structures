﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.Heap;

namespace DataStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            Heap.Heap heap = new Heap.Heap();
            heap.TestInsert();
            heap.TestHeapCreate();

            //Sort.HeapSort heapSort = new Sort.HeapSort();
            //heapSort.Test();

            Console.ReadLine();
        }
    }
}
