using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStruct.BTree;
using DataStruct.Heap;

namespace DataStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            Heap.Heap heap = new Heap.Heap();
            //heap.TestInsert();
            heap.TestHeapCreate();

            Console.WriteLine();
            //BSTree<int>.Test();

            Console.ReadLine();
        }
    }
}
