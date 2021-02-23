using DataStruct.BinaryTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.BTree
{
    /// <summary>
    /// 二叉搜索树
    /// </summary>
    public class BSTree<T> : BTree<T> where T : IComparable<T>
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

            BTreeLogHelper<int>.Log(bSTree.Root);
        }

        private BTNode<T> _hot;

        public override BTNode<T> Insert(T t)
        {
            BTNode<T> node = null;
            if (null == Root)
            {
                node = new BTNode<T>(t);
                Root = node;
            }
            node = Search(t);
            if (null != node)
            {
                return node;
            }

            node = new BTNode<T>(t, _hot);
            if (node.Value.CompareTo(_hot.Value) > 0)
            {
                _hot.RightChild = node;
            }
            else
            {
                _hot.LeftChild = node;
            }
            if (null == Root)
            {
            }

            return node;
        }

        public override bool Remove(T t)
        {
            return false;
        }

        public override BTNode<T> Search(T t)
        {
            if (null == Root || t.CompareTo(Root.Value) == 0)
            {
                _hot = null;
                return Root;
            }

            _hot = Root;
            while (null != _hot)
            {
                BTNode<T> c = _hot.Value.CompareTo(t) > 0 ? _hot.LeftChild : _hot.RightChild;
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
