﻿using DataStruct.BinTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Log
{
    class LogBinTreeCheck<T> where T : IComparable<T>
    {

        public static void Check(List<BinNode<T>> nodeList)
        {
            Console.WriteLine();
            Console.WriteLine();

            foreach (var node in nodeList)
            {
                if (null == node.ParentNode)
                {
                    continue;
                }

                bool success = true;
                if (node.IsLChild())
                {
                    success = (node.Value.CompareTo(node.ParentNode.LeftChild.Value) == 0);
                    //Console.WriteLine(node.Value.ToString() + " is:" + node.ParentNode.Value.ToString() + " Lc  " + success);
                }
                else if (node.IsRChild())
                {
                    success = (node.Value.CompareTo(node.ParentNode.RightChild.Value) == 0);
                    //Console.WriteLine(node.Value.ToString() + " is:" + node.ParentNode.Value.ToString() + " Rc  " + success);
                }
                else if (!node.ParentNode.HasChild())
                {
                    success = false;
                }

                if (!success)
                {
                    Console.WriteLine(node.Value.ToString() + "   Error:=========================:");
                }
            }
        }

    }
}
