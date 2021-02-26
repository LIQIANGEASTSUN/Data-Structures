using DataStruct.BinTree;
using DataStruct.Log;
using System;
using System.Collections.Generic;

namespace DataStruct.BSTree
{
    public class BSTreeTest
    {
        public static void Test()
        {
            List<BinNode<int>> list = new List<BinNode<int>>();

            //{
            //    int[] aaaRR = new int[] { 16, 9, 6, 22 };
            //    BSTree<int> bSTree = new BSTree<int>();
            //    for (int i = 0; i < aaaRR.Length; ++i)
            //    {
            //        bSTree.Insert(aaaRR[i]);
            //    }
            //    BinTreeLogHelper<int>.Log(bSTree.Root, false);
            //    Console.WriteLine();
            //    list = bSTree.TraverseLevel(bSTree.Root);
            //    Console.WriteLine();

            //    for (int n = 0; n < list.Count; ++n)
            //    {
            //        BinNode<int> node = list[n];
            //        Console.WriteLine(list[n].Value.ToString() + "   heigh:" + list[n].Height + "   deep:" + list[n].Deep);
            //        int deep = -1;
            //        while (null != node)
            //        {
            //            ++deep;
            //            node = node.ParentNode;
            //        }
            //        if (deep != list[n].Deep)
            //        {
            //            Console.WriteLine(list[n].Value.ToString() + "  deep:" + list[n].Deep + "    Error Error Error Error Error Error");
            //        }
            //    }

            //    bSTree.Remove(6);
            //    BinTreeLogHelper<int>.Log(bSTree.Root, false);

            //    Console.WriteLine();
            //    list = bSTree.TraverseLevel(bSTree.Root);
            //    Console.WriteLine();

            //    for (int n = 0; n < list.Count; ++n)
            //    {
            //        BinNode<int> node = list[n];
            //        Console.WriteLine(list[n].Value.ToString() + "   heigh:" + list[n].Height + "   deep:" + list[n].Deep);
            //        int deep = -1;
            //        while (null != node)
            //        {
            //            ++deep;
            //            node = node.ParentNode;
            //        }
            //        if (deep != list[n].Deep)
            //        {
            //            Console.WriteLine(list[n].Value.ToString() + "  deep:" + list[n].Deep + "    Error Error Error Error Error Error");
            //        }
            //    }
            //}

            int[] arr = new int[] { 10, 8, 15, 17, 20, 19, 21, 12, 13, 6, 9, 16, 22, };
            //for (int i = 0; i < arr.Length; ++i)
            //{
            //    TestRemove(arr, i);
            //}

            {

                BSTree<int> bSTree = new BSTree<int>();
                for (int i = 0; i < arr.Length; ++i)
                {
                    bSTree.Insert(arr[i]);
                    //BinTreeLogHelper<int>.Log(bSTree.Root, false);

                    //Console.WriteLine();
                    //list = bSTree.TraverseLevel(bSTree.Root);
                    //Console.WriteLine();

                    //for (int n = 0; n < list.Count; ++n)
                    //{
                    //    BinNode<int> node = list[n];
                    //    Console.WriteLine(list[n].Value.ToString() + "   heigh:" + list[n].Height + "   deep:" + list[n].Deep);
                    //    int deep = -1;
                    //    while (null != node)
                    //    {
                    //        ++deep;
                    //        node = node.ParentNode;
                    //    }
                    //    if (deep != list[n].Deep)
                    //    {
                    //        Console.WriteLine(list[n].Value.ToString() + "  deep:" + list[n].Deep + "    Error Error Error Error Error Error");
                    //    }
                    //}

                    //list = bSTree.TraverseLevel(bSTree.Root);
                    //Console.WriteLine();

                    //for (int n = 0; n < list.Count; ++n)
                    //{
                    //    BinNode<int> node = list[n];
                    //    int heigh = node.Height;
                    //    //bSTree.UpdateHeight(node);
                    //    if (heigh != node.Height)
                    //    {
                    //        Console.WriteLine(list[n].Value.ToString() + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                    //    }
                    //}
                }
                BinTreeLogHelper<int>.Log(bSTree.Root, false);
                Console.WriteLine();
                Console.WriteLine();
                //list = bSTree.TraverseLevel(bSTree.Root);
                //for (int n = 0; n < list.Count; ++n)
                //{
                //    BinNode<int> node = list[n];
                //    int deep = -1;
                //    while (null != node)
                //    {
                //        ++deep;
                //        node = node.ParentNode;
                //    }
                //    if (deep != list[n].Deep)
                //    {
                //        Console.WriteLine(list[n].Value.ToString() + "  deep:" + list[n].Deep + "    Error Error Error Error Error Error");
                //    }
                //}
                Console.WriteLine();

                for (int i = 0; i < arr.Length; ++i)
                {
                    Console.WriteLine("Remove:" + arr[i]);
                    bSTree.Remove(arr[i]);
                    BinTreeLogHelper<int>.Log(bSTree.Root, false);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    LogBinTreeCheck<int>.Check(bSTree.TraverseLevel(bSTree.Root));
                    Console.WriteLine();

                    list = bSTree.TraverseLevel(bSTree.Root);

                    Console.WriteLine();
                    for (int n = 0; n < list.Count; ++n)
                    {
                        Console.WriteLine(list[n].Value.ToString() + "  heigh:" + list[n].Height);
                    }

                    list = bSTree.TraverseLevel(bSTree.Root);
                    Console.WriteLine();

                    for (int n = 0; n < list.Count; ++n)
                    {
                        BinNode<int> node = list[n];
                        int heigh = node.Height;
                        //bSTree.UpdateHeight(node);
                        if (heigh != node.Height)
                        {
                            Console.WriteLine(list[n].Value.ToString() + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                        }
                    }

                    Console.WriteLine();
                }
            }

            //{
            //    BSTree<int> bSTree = new BSTree<int>();
            //    bSTree.Insert(10);
            //    BinTreeLogHelper<int>.Log(bSTree.Root, false);
            //    Console.WriteLine();

            //    bSTree.Remove(10);
            //    BinTreeLogHelper<int>.Log(bSTree.Root, false);
            //}
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
            BinTreeLogHelper<int>.Log(bSTree.Root, false);

            Console.WriteLine();
            LogBinTreeCheck<int>.Check(bSTree.TraverseLevel(bSTree.Root));

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
        protected BinNode<T> _hot;

        public BSTree()
        {

        }

        public virtual BinNode<T> Insert(T t)
        {
            BinNode<T> node = Search(t);
            if (null != node)
            {
                return node;
            }
            node = Insert(t, _hot);

            UpdateHeightAbove(node);
            return node;
        }

        protected BinNode<T> Insert(T t, BinNode<T> parent)
        {
            if (null == parent)
            {
                Root = new BinNode<T>(t);
                return Root;
            }
            else
            {
                BinNode<T> node = (t.CompareTo(parent.Value) > 0) ? parent.InsertAsRc(t) : parent.InsertAsLc(t);
                return node;
            }
        }

        public virtual bool Remove(T t)
        {
            BinNode<T> node = Search(t);
            if (null == node)
            {
                return false;
            }

            BinNode<T> updateNode = null;
            BinNode<T> succ = null;
            if (!node.HasLChild())      // 如果节点没有左孩子，则直接以其右孩子代替
            {
                Replace(node.RightChild, node); // 令node的右孩子替换node
                updateNode = node.RightChild != null ? node.RightChild : node;
                _hot = node.ParentNode;
            }
            else if (!node.HasRChild()) // 如果节点没有右孩子，则直接以其左孩子代替
            {
                Replace(node.LeftChild, node); // 令 node 的左孩子替换node
                updateNode = node.LeftChild != null ? node.LeftChild : node;
                _hot = node.ParentNode;
            }
            else
            {
                succ = NodeSucc(node); // 要删除节点的直接后继
                T temp = succ.Value;
                succ.Value = node.Value;
                node.Value = temp;

                BinNode<T> u = succ.ParentNode;
                if (u == node)
                {
                    u.InsertAsRc(succ.RightChild);  //令实际要删除节点的右孩子作为 u 的右孩子
                }
                else
                {
                    u.InsertAsLc(succ.RightChild); // 令实际要删除节点的右孩子作为 u 的左孩子
                }
                _hot = u;
                updateNode = u;
            }

            //Console.WriteLine("Remove Update:" + t.ToString() + "     " + _hot.Value);

            UpdateHeightAbove(updateNode);
            return true;
        }

        private void Replace(BinNode<T> node, BinNode<T> beReplace)
        {
            if (beReplace.IsRoot())
            {
                Root = node;
                if (null != Root)
                {
                    Root.ParentNode = null;
                }
                return;
            }

            if (null != node)
            {
                node.ParentNode = beReplace.ParentNode;
            }
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
        private BinNode<T> NodeSucc(BinNode<T> node)
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
