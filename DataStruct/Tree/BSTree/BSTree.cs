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

            BSTree<int> bsTree = new BSTree<int>();
            for (int i = 0; i < 9; ++i)
            {
                bsTree.Insert(i);
            }
            BinTreeLogHelper<int>.Log(bsTree.Root, false, false);
            //{
            //    int[] aaaRR = new int[] { 16, 9, 6, 22 };s
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
                BinTreeLogHelper<int>.Log(bSTree.Root, false, false);
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
                    BinTreeLogHelper<int>.Log(bSTree.Root, false, false);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    LogBinTreeCheck<int>.Check(bSTree.TraverseLevel(bSTree.Root));
                    Console.WriteLine();

                    list = bSTree.TraverseLevel(bSTree.Root);

                    Console.WriteLine();
                    for (int n = list.Count - 1; n >= 0; --n)
                    {
                        BinNode<int> node = list[n];
                        int heigh = node.Height;
                        //bSTree.UpdateHeight(node);
                        if (heigh == node.Height)
                        {
                            Console.WriteLine(list[n].Element.ToString() + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                        }
                    }
                    //for (int n = 0; n < list.Count; ++n)
                    //{
                    //    BinNode<int> node = list[n];
                    //    int heigh = node.Height;
                    //    bSTree.UpdateHeight(node);
                    //    if (heigh != node.Height)
                    //    {
                    //        Console.WriteLine(list[n].Value.ToString() + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                    //    }
                    //}
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
  
            BinTreeLogHelper<int>.Log(bSTree.Root, false, false);

            Console.WriteLine("Remove:" + arr[removeIndex]);
            bSTree.Remove(arr[removeIndex]);
            BinTreeLogHelper<int>.Log(bSTree.Root, false, false);

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
        protected BinNode<T> _hot; // search() 最后访问的非空节点位置

        public BSTree()
        {

        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 插入，仅内部调用
        /// </summary>
        /// <param name="t"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        protected BinNode<T> Insert(T t, BinNode<T> parent)
        {
            if (null == parent)
            {
                Root = new BinNode<T>(t);
                return Root;
            }
            else
            {
                BinNode<T> node = (t.CompareTo(parent.Element) > 0) ? parent.InsertAsRc(t) : parent.InsertAsLc(t);
                return node;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public virtual bool Remove(T t)
        {
            BinNode<T> node = Search(t);
            if (null == node)
            {
                return false;
            }

            Remove(node, ref _hot);

            UpdateHeightAbove(_hot);
            return true;
        }

        /// <summary>
        /// 删除,仅内部调用
        /// </summary>
        /// <param name="node"></param>
        /// <param name="hot"></param>
        protected BinNode<T> Remove(BinNode<T> node, ref BinNode<T> hot)
        {
            BinNode<T> succ = null;
            if (!node.HasLChild())      // 如果节点没有左孩子，则直接以其右孩子代替
            {
                succ = Replace(node.RightChild, node); // 令node的右孩子替换node
                hot = node.ParentNode;
            }
            else if (!node.HasRChild()) // 如果节点没有右孩子，则直接以其左孩子代替
            {
                succ = node.LeftChild;
                succ = Replace(node.LeftChild, node); // 令 node 的左孩子替换node
                hot = node.ParentNode;
            }
            else
            {
                BinNode<T> w = NodeSucc(node); // 要删除节点的直接后继
                T temp = w.Element;
                w.Element = node.Element;
                node.Element = temp;

                BinNode<T> u = w.ParentNode;
                if (u == node)
                {
                    u.InsertAsRc(w.RightChild);  //令实际要删除节点的右孩子作为 u 的右孩子
                }
                else
                {
                    u.InsertAsLc(w.RightChild); // 令实际要删除节点的右孩子作为 u 的左孩子
                }
                hot = u;
                succ = w.RightChild;
            }

            return succ;
        }

        /// <summary>
        /// 替换节点：将 beReplace 节点替换为 node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="beReplace">被替换的节点</param>
        private BinNode<T> Replace(BinNode<T> node, BinNode<T> beReplace)
        {
            if (beReplace.IsRoot())
            {
                Root = node;
                if (null != Root)
                {
                    Root.ParentNode = null;
                }
                return Root;
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
            return node;
        }

        /// <summary>
        /// 节点的直接后继
        /// 如果有右孩子，则取右孩子及子孙后代中最小者
        /// 否则，取左孩子及子孙后代中最大者
        /// </summary>
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

        /// <summary>
        /// 查询：返回查询结果，如果存在则 _hot 为查询结果的父节点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual BinNode<T> Search(T t)
        {
            if (null == Root || t.CompareTo(Root.Element) == 0)
            {
                _hot = null;
                return Root;
            }

            _hot = Root;
            while (null != _hot)
            {
                BinNode<T> c = _hot.Element.CompareTo(t) > 0 ? _hot.LeftChild : _hot.RightChild;
                if (null == c || c.Element.CompareTo(t) == 0)
                {
                    return c;
                }
                _hot = c;
            }

            return _hot;
        }

        /// <summary>
        /// BST 节点旋转变换统一算法(3节点 + 4子树)，返回调整之后局部子树根节点的位置
        /// 注意：尽管子树根会正确指向上层节点（如果存在），但反向的联接须由上层函数完成
        /// </summary>
        protected BinNode<T> RotateAt(BinNode<T> v)
        {
            if (null == v)
            {
                return v;
            }

            BinNode<T> p = v.ParentNode;
            BinNode<T> g = p.ParentNode; // 视v、p和g相对位置分四种情况

            if (p.IsLChild())
            {
                if (v.IsLChild())
                {
                    p.ParentNode = g.ParentNode; // 向上联接
                    return Connect34(v, p, g, v.LeftChild, v.RightChild, p.RightChild, g.RightChild);
                }
                else
                {
                    v.ParentNode = g.ParentNode; // 向上联接
                    return Connect34(p, v, g, p.LeftChild, v.LeftChild, v.RightChild, g.RightChild);
                }
            }
            else
            {
                if (v.IsRChild())
                {
                    p.ParentNode = g.ParentNode;// 向上联接
                    return Connect34(g, p, v, g.LeftChild, p.LeftChild, v.LeftChild, v.RightChild);
                }
                else
                {
                    v.ParentNode = g.ParentNode;// 向上联接
                    return Connect34(g, v, p, g.LeftChild, v.LeftChild, v.RightChild, p.RightChild);
                }
            }
        }

        /// <summary>
        /// 按照 "3 + 4" 结构联接3个节点及其4棵子树，返回重组之后的局子树根节点位置(即b)
        /// 子树根节点与上层节点之间的双向联接，均须由上层调用者完成
        /// 可用于AVL 和 RedBlack 的局部平衡调整
        /// </summary>
        /// <returns></returns>
        protected BinNode<T> Connect34(BinNode<T> a, BinNode<T> b, BinNode<T> c, BinNode<T> T0, BinNode<T> T1, BinNode<T> T2, BinNode<T> T3)
        {
            a.LeftChild = T0;
            if (null != T0)
            {
                T0.ParentNode = a;
            }

            a.RightChild = T1;
            if (null != T1)
            {
                T1.ParentNode = a;
            }

            c.LeftChild = T2;
            if (null != T2)
            {
                T2.ParentNode = c;
            }

            c.RightChild = T3;
            if (null != T3)
            {
                T3.ParentNode = c;
            }

            b.LeftChild = a;
            a.ParentNode = b;

            b.RightChild = c;
            c.ParentNode = b;

            UpdateHeight(a);
            UpdateHeight(c);
            UpdateHeight(b);

            return b;
        }
    }
}
