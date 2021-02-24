using DataStruct.BinTree;
using System;

namespace DataStruct.BTree
{
    public class BSTreeTest
    {
        public static void Test()
        {
            BSTree<int> bSTree = new BSTree<int>();

            BinNode<int> node = bSTree.Insert(18);
            bSTree.Insert(10);
            bSTree.Insert(8);
            bSTree.Insert(15);
            bSTree.Insert(17);
            bSTree.Insert(20);
            bSTree.Insert(19);
            bSTree.Insert(21);
            bSTree.Insert(12);
            bSTree.Insert(6);
            bSTree.Insert(9);
            bSTree.Insert(16);
            bSTree.Insert(22);

            BinTreeLogHelper<int>.Log(bSTree.Root);

            Console.WriteLine();
            bSTree.Remove(20);
            BinTreeLogHelper<int>.Log(bSTree.Root);

            BinNode<int> succ = bSTree.NodeSucc(node);
            int a = 0;
        }
    }

    /// <summary>
    /// 二叉搜索树
    /// </summary>
    public class BSTree<T> : BinTree<T> where T : IComparable<T>
    {
        private BinNode<T> _hot;

        public BSTree()
        {

        }

        public virtual BinNode<T> Insert(T t)
        {
            if (null == Root)
            {
                Root = new BinNode<T>(t);
                return Root;
            }

            BinNode<T> node = Search(t);
            if (null != node)
            {
                return node;
            }

            node = (t.CompareTo(_hot.Value) > 0) ? _hot.InsertAsRc(t) : _hot.InsertAsLc(t);
            return node;
        }

        public bool Remove(T t)
        {
            BinNode<T> node = Search(t);
            if (null == node)
            {
                return false;
            }

            BinNode<T> w = node;    // 实际要被删除的节点
            BinNode<T> succ = null; // 实际要删除节点的接替者
            if (!node.HasLChild())      // 如果节点没有左孩子，则直接以其有孩子代替
            {
                succ = node.RightChild;
            }
            else if (!node.HasRChild()) // 如果节点没有右孩子，则直接以其左孩子代替
            {
                succ = node.LeftChild;
            }
            else
            {
                w = NodeSucc(node);
                T temp = w.Value;
                w.Value = node.Value;
                node.Value = temp;

                BinNode<T> u = w.ParentNode;
                succ = w.RightChild;
                if (u == node)
                {
                    u.RightChild = succ;
                }
                else
                {
                    u.LeftChild = succ;
                }
            }

            _hot = w.ParentNode;//要删除节点的父节点
            if (node.IsLChild())
            {
                _hot.LeftChild = succ;
            }
            else if (node.IsRChild())
            {
                _hot.RightChild = succ;
            }

            if (null != succ)
            {
                succ.ParentNode = _hot;
            }

            return false;
        }

        // 节点的直接后继
        public BinNode<T> NodeSucc(BinNode<T> node)
        {
            if (node.HasRChild())
            {
                node = node.RightChild;
                while (node.HasLChild())
                {
                    node = node.LeftChild;
                }
            }
            else
            {
                node = node.LeftChild;
                while (null != node && node.HasRChild())
                {
                    node = node.RightChild;
                }
            }

            return node;
        }

        public virtual BinNode<T> Search(T t)
        {
            if (null == Root || t.CompareTo(Root.Value) == 0)
            {
                _hot = null;
                return Root;
            }

            _hot = Root;
            while (null != _hot)
            {
                BinNode<T> c = _hot.Value.CompareTo(t) > 0 ? _hot.LeftChild : _hot.RightChild;
                if (null == c || c.Value.CompareTo(t) == 0)
                {
                    return c;
                }
                _hot = c;
            }

            return _hot;
        }
    }
}
