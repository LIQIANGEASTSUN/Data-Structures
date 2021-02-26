using System;
using System.Collections.Generic;
using System.Text;

namespace DataStruct.BinTree
{
    
    public class BinTreeTest
    {

        public static void Test()
        {
            BinTree<int> binTree = new BinTree<int>();

            Console.WriteLine("insert:" + 8);
            BinNode<int> root = binTree.InsertAsRoot(8);

            Console.WriteLine("insert:" + 5);
            BinNode<int> node1 = binTree.InsertAsLc(root, 5);

            Console.WriteLine("insert:" + 10);
            BinNode<int> node2 = binTree.InsertAsRc(root, 10);

            Console.WriteLine("insert:" + 3);
            BinNode<int> node3 = binTree.InsertAsLc(node1, 3);

            Console.WriteLine("insert:" + 6);
            BinNode<int> node4 = binTree.InsertAsRc(node1, 6);

            Console.WriteLine("insert:" + 7);
            BinNode<int> node5 = binTree.InsertAsRc(node4, 7);

            Console.WriteLine("insert:" + 9);
            BinNode<int> node6 = binTree.InsertAsLc(node2, 9);

            Console.WriteLine("insert:" + 11);
            BinNode<int> node7 = binTree.InsertAsRc(node2, 11);

            BinTreeLogHelper<int>.Log(binTree.Root);

            Console.WriteLine();
            List<BinNode<int>> list = binTree.TraverseLevel(binTree.Root);
            for (int i = 0; i < list.Count; ++i)
            {
                Console.WriteLine("heigh:" + list[i].Value.ToString() + "  heigh:" + list[i].Height);
            }

            binTree.Remove(node2);
            BinTreeLogHelper<int>.Log(binTree.Root);

            Console.WriteLine("先序遍历");
            binTree.TraversePreRecursion(binTree.Root);
            Console.WriteLine();

            binTree.TraversePre(binTree.Root);
            Console.WriteLine();

            binTree.TraversePre2(binTree.Root);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("中序遍历");
            binTree.TraverseiInRecursion(binTree.Root);
            Console.WriteLine();

            binTree.TraverseIn(binTree.Root);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("后序遍历");
            binTree.TraverseiPostRecursion(binTree.Root);
            Console.WriteLine();

            binTree.TraversePost(binTree.Root);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("层序遍历");
            list = binTree.TraverseLevel(binTree.Root);
            for (int i = 0; i < list.Count; ++i)
            {
                Console.WriteLine("heigh:" + list[i].Value.ToString() + "  heigh:" + list[i].Height);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    // 二叉树
    public class BinTree<T> where T : IComparable<T>
    {
        private BinNode<T> _root = null;

        public BinTree()
        {
            _root = null;
        }

        public BinNode<T> Root
        {
            get { return _root; }
            protected set { _root = value; }
        }

        public bool Empty()
        {
            return null == Root;
        }

        // node 为二叉树中的合法位置
        public virtual bool Remove(BinNode<T> node)
        {
            if (node.IsRoot())
            {
                Root = null;
                return true;
            }
            
            if (node.IsLChild())
            {
                node.ParentNode.LeftChild = null;
            }
            else
            {
                node.ParentNode.RightChild = null;
            }

            UpdateHeightAbove(node.ParentNode);
            UpdateDeep(node.ParentNode);
            return true;
        }

        public virtual BinNode<T> InsertAsRoot(T t)
        {
            _root = new BinNode<T>(t);
            UpdateHeightAbove(_root);
            UpdateDeep(_root);
            return _root;
        }

        public virtual BinNode<T> InsertAsLc(BinNode<T> node, T t)
        {
            node.InsertAsLc(t);
            UpdateHeightAbove(node);
            UpdateDeep(node);
            return node.LeftChild;
        }

        public virtual BinNode<T> InsertAsRc(BinNode<T> node, T t)
        {
            node.InsertAsRc(t);
            UpdateHeightAbove(node);
            UpdateDeep(node);
            return node.RightChild;
        }

        #region Recursion
        //先序遍历：先跟->左->右  递归实现
        public void TraversePreRecursion(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            Console.Write(node.Value.ToString() + "    ");
            TraversePreRecursion(node.LeftChild);
            TraversePreRecursion(node.RightChild);
        }

        //中序遍历：先左->跟->右  递归实现
        public void TraverseiInRecursion(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            TraverseiInRecursion(node.LeftChild);
            Console.Write(node.Value.ToString() + "    ");
            TraverseiInRecursion(node.RightChild);
        }

        //后序遍历：先左->右->跟  递归实现
        public void TraverseiPostRecursion(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            TraverseiPostRecursion(node.LeftChild);
            TraverseiPostRecursion(node.RightChild);
            Console.Write(node.Value.ToString() + "    ");
        }

        // 层序遍历：按层从上到下，每层从左到右依次遍历
        public void TraverseiLevelRecursion(BinNode<T> node)
        {

        }
        #endregion

        #region Iteration
        //先序遍历：先跟->左->右  迭代实现
        public void TraversePre(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            Stack<BinNode<T>> stack = new Stack<BinNode<T>>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                node = stack.Pop();
                Console.Write(node.Value.ToString() + "    ");
                if (node.HasRChild())
                {
                    stack.Push(node.RightChild);
                }
                if(node.HasLChild())
                {
                    stack.Push(node.LeftChild);
                }
            }
        }

        public void TraversePre2(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            Stack<BinNode<T>> stack = new Stack<BinNode<T>>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                while (null != node)
                {
                    Console.Write(node.Value.ToString() + "    ");
                    if ((node = node.LeftChild) != null)
                    {
                        stack.Push(node);
                    }
                }

                node = stack.Pop();
                if (null != node && ((node = node.RightChild) != null))
                {
                    stack.Push(node);
                }
            }
        }

        //中序遍历：先左->跟->右  迭代实现
        public void TraverseIn(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            Stack<BinNode<T>> stack = new Stack<BinNode<T>>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                while (null != node)
                {
                    if ((node = node.LeftChild) != null)
                    {
                        stack.Push(node);
                    }
                }

                node = stack.Pop();
                Console.Write(node.Value.ToString() + "    ");
                if (null != node && ((node = node.RightChild) != null))
                {
                    stack.Push(node);
                }
            }
        }

        //后序遍历：先左->右->跟  迭代实现
        public void TraversePost(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            Stack<BinNode<T>> result = new Stack<BinNode<T>>();
            Stack<BinNode<T>> stack = new Stack<BinNode<T>>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                node = stack.Pop();
                result.Push(node);
                if (node.HasLChild())
                {
                    stack.Push(node.LeftChild);
                }
                if (node.HasRChild())
                {
                    stack.Push(node.RightChild);
                }
            }

            while (result.Count > 0)
            {
                BinNode<T> temp = result.Pop();
                Console.Write(temp.Value.ToString() + "    ");
            }

            Console.WriteLine();
        }

        public List<BinNode<T>> TraverseLevel(BinNode<T> node)
        {
            List<BinNode<T>> list = new List<BinNode<T>>();
            if (null == node)
            {
                return list;
            }

            Queue<BinNode<T>> queue = new Queue<BinNode<T>>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                Console.Write(node.Value.ToString() + "   ");
                list.Add(node);
                if (node.HasLChild())
                {
                    queue.Enqueue(node.LeftChild);
                }
                if (node.HasRChild())
                {
                    queue.Enqueue(node.RightChild);
                }
            }
            return list;
        }
        #endregion

        // 更新高度
        protected void UpdateHeightAbove(BinNode<T> node)
        {
            while (null != node) // 从node出发，覆盖历代祖先
            {
                UpdateHeight(node);
                node = node.ParentNode;
            }
        }

        protected int UpdateHeight(BinNode<T> node)
        {
            node.Height = 1 + Math.Max(NodeHeight(node.LeftChild), NodeHeight(node.RightChild));
            return node.Height;
        }

        protected int NodeHeight(BinNode<T> node)
        {
            return (null != node) ? node.Height : -1;
        }

        protected void UpdateDeep(BinNode<T> node)
        {
            Queue<BinNode<T>> queue = new Queue<BinNode<T>>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                if (null == node)
                {
                    continue;
                }
                node.Deep = NodeDeep(node.ParentNode) + 1;
                queue.Enqueue(node.RightChild);
                queue.Enqueue(node.LeftChild);
            }
        }

        private int NodeDeep(BinNode<T> node)
        {
            return (null != node) ? node.Deep : -1;
        }

        public virtual void Release()
        {
            _root = null;
        }

    }
}
