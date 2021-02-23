using DataStruct.BTree;
using DataStruct.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.BinaryTree
{
    public class BTreeLogNode<T> : LogNode<T> where T : IComparable<T>
    {
        public int index;
        public int parentIndex;
        public int leftChildIndex = -1;
        public int rightChildIndex = -1;

        public override int ParentIndex()
        {
            return parentIndex;
        }

        public override int LeftChildIndex()
        {
            return leftChildIndex;
        }

        public override int RightChildIndex()
        {
            return rightChildIndex;
        }

        public override string ToString()
        {
            return t.ToString();
        }
    }

    class BTreeLogHelper<T> where T : IComparable<T>
    {

        public static void Log(BTNode<T> rootNode)
        {
            List<BTreeLogNode<T>> logNodeList = new List<BTreeLogNode<T>>();
            GetLogNode(rootNode, null, false, logNodeList);
            LogBTree<T>.Log(logNodeList.ToArray());
        }

        private static void GetLogNode(BTNode<T> node, BTreeLogNode<T> parent, bool left, List<BTreeLogNode<T>> logNodeList)
        {
            if (null == node)
            {
                return;
            }

            BTreeLogNode<T> logNode = new BTreeLogNode<T>();
            logNode.index = logNodeList.Count;
            logNode.t = node.Value;
            if (null != parent)
            {
                logNode.parentIndex = parent.index;
                if (left)
                {
                    parent.leftChildIndex = logNode.index;
                }
                else
                {
                    parent.rightChildIndex = logNode.index;
                }
            }
            logNodeList.Add(logNode);

            if (null != node.LeftChild)
            {
                GetLogNode(node.LeftChild, logNode, true, logNodeList);
            }
            if (null != node.RightChild)
            {
                GetLogNode(node.RightChild, logNode, false, logNodeList);
            }

        }

    }
}
