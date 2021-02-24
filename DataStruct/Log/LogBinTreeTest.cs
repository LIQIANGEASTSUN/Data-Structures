using DataStruct.BinTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStruct.Log
{
    class LogBinTreeTest<T> where T : IComparable<T>
    {
        public void TraverseIn(BinNode<T> node)
        {
            int index = 0;
            List<StringBuilder> sbList = new List<StringBuilder>();
            TraverseiInRecursion(node, sbList, ref index);

            foreach(var sb in sbList)
            {
                Console.WriteLine(sb.ToString());
            }
        }

        public void TraverseiInRecursion(BinNode<T> node, List<StringBuilder> sbList, ref int index)
        {
            if (null == node)
            {
                return;
            }

            node.Deep = node.HasParent() ? node.ParentNode.Deep + 1 : 0;
            int line = node.Deep == 0 ? 1 : node.Deep * 3 + 1;
            while(sbList.Count <= line)
            {
                sbList.Add(new StringBuilder());
            }
            TraverseiInRecursion(node.LeftChild, sbList, ref index);
            Write(node, sbList, ref index);
            TraverseiInRecursion(node.RightChild, sbList, ref index);
        }

        private void Write(BinNode<T> node, List<StringBuilder> sbList, ref int index)
        {
            for (int i = 0; i < sbList.Count; ++i)
            {
                while (sbList[i].ToString().Length < index)
                {
                    sbList[i].Append(" ");
                }
                int line = (node.Deep == 0 ? 1 : node.Deep * 3 + 1) - 1;
                string msg = (line == i) ? node.Value.ToString() : " ";
                sbList[i].Append(msg);
                sbList[line + 1].Append("====");
            }
            index += 8;
            Console.WriteLine(node.Value.ToString() + "    " + node.Deep + "   index:" + index);
        }
    }
}
