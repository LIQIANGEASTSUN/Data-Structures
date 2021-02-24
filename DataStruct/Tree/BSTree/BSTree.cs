using DataStruct.BinTree;
using System;

namespace DataStruct.BTree
{
    public class BSTreeTest
    {
        public static void Test()
        {
            BSTree<int> bSTree = new BSTree<int>();
            bSTree.Insert(8);
            bSTree.Insert(5);
            bSTree.Insert(3);
            bSTree.Insert(6);
            bSTree.Insert(10);
            bSTree.Insert(9);
            bSTree.Insert(11);
            bSTree.Insert(7);

            BinTreeLogHelper<int>.Log(bSTree.Root);
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

        public override bool Remove(BinNode<T> binNode)
        {
            binNode = null;
            return false;
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
