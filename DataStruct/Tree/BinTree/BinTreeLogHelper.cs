using DataStruct.BSTree;
using DataStruct.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.BinTree
{
    public class BTreeLogNode<T> : LogNode<T> where T : IComparable<T>
    {
        public int index;
        public int parentIndex;
        public int leftChildIndex = -1;
        public int rightChildIndex = -1;
        public Color Color;
        public bool isRedBlackTree;

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
            return element.ToString();
        }

        public override bool IsRedBlack()
        {
            return isRedBlackTree;
        }

        public override Color GetColor()
        {
            return Color;
        }
    }

    class BinTreeLogHelper<T> where T : IComparable<T>
    {

        public static void Log(BinNode<T> rootNode, bool isRedBlackTree, bool showParent)
        {
            List<BTreeLogNode<T>> logNodeList = new List<BTreeLogNode<T>>();
            GetLogNode(rootNode, null, false, logNodeList, isRedBlackTree);
            LogBinTree<T>.Log(logNodeList.ToArray(), showParent);
        }

        private static void GetLogNode(BinNode<T> node, BTreeLogNode<T> parent, bool left, List<BTreeLogNode<T>> logNodeList, bool isRedBlackTree)
        {
            if (null == node)
            {
                return;
            }

            BTreeLogNode<T> logNode = new BTreeLogNode<T>();
            logNode.index = logNodeList.Count;
            logNode.element = node.Element;
            logNode.Color = node.Color;
            logNode.isRedBlackTree = isRedBlackTree;
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
                GetLogNode(node.LeftChild, logNode, true, logNodeList, isRedBlackTree);
            }
            if (null != node.RightChild)
            {
                GetLogNode(node.RightChild, logNode, false, logNodeList, isRedBlackTree);
            }

        }

    }
}
