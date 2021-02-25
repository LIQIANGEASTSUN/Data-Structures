using DataStruct.BinTree;
using System;

namespace DataStruct.BTree
{
    public class BSTreeTest
    {
        public static void Test()
        {
            int[] arr = new int[] { 10, 8, 15, 17, 20, 19, 21, 12, 13, 6, 9, 16, 22,};
            for (int i = 0; i < arr.Length; ++i)
            {
                
            }
            TestRemove(arr, 0);
        }

        //删除叶子节点失败
        private static void TestRemove(int[] arr, int removeIndex)
        {
            BSTree<int> bSTree = new BSTree<int>();

            for (int i = 0; i < arr.Length; ++i)
            {
                bSTree.Insert(arr[i]);
            }
  
            BinTreeLogHelper<int>.Log(bSTree.Root);

            Console.WriteLine("Remove:" + arr[removeIndex]);
            bSTree.Remove(arr[removeIndex]);
            BinTreeLogHelper<int>.Log(bSTree.Root);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
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
            if (!node.HasLChild())      // 如果节点没有左孩子，则直接以其右孩子代替
            {
                succ = node.RightChild;
                Replace(succ, node);
            }
            else if (!node.HasRChild()) // 如果节点没有右孩子，则直接以其左孩子代替
            {
                succ = node.LeftChild;
                Replace(succ, node);
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
                    u.InsertAsRc(succ);
                }
                else
                {
                    u.InsertAsLc(succ);
                }
            }

            return false;
        }

        public void Replace(BinNode<T> node, BinNode<T> beReplace)
        {
            if (beReplace.IsRoot())
            {
                Root = node;
                return;
            }

            node.ParentNode = beReplace.ParentNode;
            if (beReplace.IsRChild())
            {
                beReplace.ParentNode.RightChild = node;
            }
            else
            {
                beReplace.ParentNode.LeftChild = node;
            }
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
