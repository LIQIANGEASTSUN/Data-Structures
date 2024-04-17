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

            int a = 0;
            if (a == 0)
            {
                return;
            }

            //BinTreeLogHelper<int>.Log(bsTree.Root, false, false);
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

            int[] arr = new int[] { 10, 5, 15, 2, 8, 13, 19, 1, 3, 6, 9, 12, 14, 17, 20, 18, };
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

                List<int> numberList = new List<int>();
                for (int i = 0;i <arr.Length; ++i)
                {
                    numberList.Add(arr[i]);
                }

                //bSTree.Remove(2);
                //BinTreeLogHelper<int>.Log(bSTree.Root, false, false);
                //if (!CheckBSTree(bSTree))
                //{
                //    BinTreeLogHelper<int>.Log(bSTree.Root, false, false);
                //    int a = 0;
                //}

                while (numberList.Count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(numberList.Count);
                    int number = numberList[index];
                    numberList.RemoveAt(index);
                    Console.WriteLine("Remove:" + number);

                    bSTree.Remove(number);
                    bool result = CheckBSTree(bSTree);
                    BinTreeLogHelper<int>.Log(bSTree.Root, false, false);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    if (!result)
                    {
                        //BinTreeLogHelper<int>.Log(bSTree.Root, false, false);
                    }
                }
                //BinTreeLogHelper<int>.Log(bSTree.Root, false, false);
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

                //for (int i = 0; i < arr.Length; ++i)
                //{
                //    Console.WriteLine("Remove:" + arr[i]);
                //    bSTree.Remove(arr[i]);
                //    BinTreeLogHelper<int>.Log(bSTree.Root, false, false);
                //    Console.WriteLine();
                //    Console.WriteLine();
                //    Console.WriteLine();
                //    LogBinTreeCheck<int>.Check(bSTree.TraverseLevel(bSTree.Root));
                //    Console.WriteLine();

                //    list = bSTree.TraverseLevel(bSTree.Root);

                //    Console.WriteLine();
                //    for (int n = list.Count - 1; n >= 0; --n)
                //    {
                //        BinNode<int> node = list[n];
                //        int heigh = node.Height;
                //        //bSTree.UpdateHeight(node);
                //        if (heigh == node.Height)
                //        {
                //            Console.WriteLine(list[n].Element.ToString() + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                //        }
                //    }
                //    //for (int n = 0; n < list.Count; ++n)
                //    //{
                //    //    BinNode<int> node = list[n];
                //    //    int heigh = node.Height;
                //    //    bSTree.UpdateHeight(node);
                //    //    if (heigh != node.Height)
                //    //    {
                //    //        Console.WriteLine(list[n].Value.ToString() + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                //    //    }
                //    //}
                //    Console.WriteLine();
                //}
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

        private static bool CheckBSTree(BSTree<int> bsTree)
        {
            List<BinNode<int>> list = new List<BinNode<int>>();

            bool result = true;
            list = bsTree.TraverseLevel(bsTree.Root);
            Console.WriteLine();
            Console.WriteLine();
            foreach (BinNode<int> node in list) {
                BinNode<int> left = node.LeftChild;
                BinNode<int> right = node.RightChild;

                if (left != null && node.Element <= left.Element)
                {
                    Console.WriteLine("Error left");
                    result = false;
                    break;
                }

                if (right != null && node.Element >= right.Element)
                {
                    Console.WriteLine("Error right");
                    result = false;
                    break;
                }
            }

            return result;
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
                succ = Replace(node, node.RightChild); // 令node的右孩子替换node
                hot = node.ParentNode;
            }
            else if (!node.HasRChild()) // 如果节点没有右孩子，则直接以其左孩子代替
            {
                succ = node.LeftChild;
                succ = Replace(node, node.LeftChild); // 令 node 的左孩子替换node
                hot = node.ParentNode;
            }
            else
            {
                // 要删除节点的直接后继节点
                BinNode<T> w = NodeSucc(node);
                // 令要删除节点的值等于其直接后继节点的值，然后将直接后继节点删除
                node.Element = w.Element;
                BinNode<T> parent = w.ParentNode;

                // 下面是删除直接后继节点逻辑
                // 如果 w 是 左子树最大的节点，则 w 没有右子树了(因为右子树比它的值更大，右子树应该是最大的节点)
                // 如果 w 是 右子树最小的节点，则 w 没有左子树了(因为左子树比它的值更小，左子树应该是最小的节点)
                // 所以 w 最多只能有一个节点

                // 如果 w 是 parent左子树，令 parent左子树 = w 唯一的子节点
                // 如果 w 是 parent右子树，令 parent右子树 = w 唯一的子节点
                BinNode<T> child = w.HasLChild() ? w.LeftChild : w.RightChild;
                if (w.IsLChild())
                {
                    parent.InsertAsLc(child);
                }
                else
                {
                    parent.InsertAsRc(child);
                }

                hot = parent;
                succ = w.RightChild;
            }

            return succ;
        }

        /// <summary>
        /// 替换节点：将 B 节点替换为 A
        /// </summary>
        private BinNode<T> Replace(BinNode<T> A, BinNode<T> B)
        {
            if (A.IsRoot())
            {
                Root = B;
                if (null != Root)
                {
                    Root.ParentNode = null;
                }
                return Root;
            }

            if (null != B)
            {
                B.ParentNode = A.ParentNode;
            }
            if (A.IsRChild())
            {
                A.ParentNode.RightChild = B;
            }
            else
            {
                A.ParentNode.LeftChild = B;
            }
            return B;
        }

        /// <summary>
        /// 节点的直接后继
        /// 如果有右孩子，则取右孩子及子孙后代中最小者
        /// 否则，取左孩子及子孙后代中最大者
        /// </summary>
        private BinNode<T> NodeSucc(BinNode<T> node)
        {
            if (node.HasLChild())
            {
                node = node.LeftChild;
                while (null != node && node.HasRChild())
                {
                    node = node.RightChild;
                }
            }
            else
            {
                node = node.RightChild;
                while (node.HasLChild())
                {
                    node = node.LeftChild;
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
