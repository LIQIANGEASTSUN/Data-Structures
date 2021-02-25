using DataStruct.BinTree;
using DataStruct.Log;
using System;
using System.Collections.Generic;

namespace DataStruct.BTree
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
                    BinTreeLogHelper<int>.Log(bSTree.Root, false);

                    Console.WriteLine();
                    list = bSTree.TraverseLevel(bSTree.Root);
                    Console.WriteLine();

                    for (int n = 0; n < list.Count; ++n)
                    {
                        BinNode<int> node = list[n];
                        Console.WriteLine(list[n].Value.ToString() + "   heigh:" + list[n].Height + "   deep:" + list[n].Deep);
                        int deep = -1;
                        while (null != node)
                        {
                            ++deep;
                            node = node.ParentNode;
                        }
                        if (deep != list[n].Deep)
                        {
                            Console.WriteLine(list[n].Value.ToString() + "  deep:" + list[n].Deep + "    Error Error Error Error Error Error");
                        }
                    }

                    list = bSTree.TraverseLevel(bSTree.Root);
                    Console.WriteLine();

                    for (int n = 0; n < list.Count; ++n)
                    {
                        BinNode<int> node = list[n];
                        int heigh = node.Height;
                        bSTree.UpdateHeight(node);
                        if (heigh != node.Height)
                        {
                            Console.WriteLine(list[n].Value.ToString() + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                        }
                    }
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
                        Console.WriteLine(list[n].Value.ToString() + "  heigh:" + list[n].Height + "  deep:" + list[n].Deep);
                        BinNode<int> node = list[n];
                        int deep = -1;
                        while (null != node)
                        {
                            ++deep;
                            node = node.ParentNode;
                        }
                        if (deep != list[n].Deep)
                        {
                            Console.WriteLine(list[n].Value.ToString() + "  deep:" + list[n].Deep + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                        }
                    }

                    list = bSTree.TraverseLevel(bSTree.Root);
                    Console.WriteLine();

                    for (int n = 0; n < list.Count; ++n)
                    {
                        BinNode<int> node = list[n];
                        int heigh = node.Height;
                        bSTree.UpdateHeight(node);
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
            UpdateHeight(node);
            UpdateDeep(node);
            return node;
        }

        public bool Remove(T t)
        {
            BinNode<T> node = Search(t);
            if (null == node)
            {
                return false;
            }

            BinNode<T> succ = null;
            if (!node.HasLChild())      // 如果节点没有左孩子，则直接以其右孩子代替
            {
                Replace(node.RightChild, node); // 令node的右孩子替换node
                _hot = node.RightChild != null ? node.RightChild : node;
            }
            else if (!node.HasRChild()) // 如果节点没有右孩子，则直接以其左孩子代替
            {
                Replace(node.LeftChild, node); // 令 node 的左孩子替换node
                _hot = node.LeftChild != null ? node.LeftChild : node;
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
            }

            UpdateHeight(_hot);
            UpdateDeep(_hot);
            return false;
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
