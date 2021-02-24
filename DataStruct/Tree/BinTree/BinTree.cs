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

            BinNode<int> root = binTree.InsertAsRoot(8);
            BinNode<int> node1 = root.InsertAsLc(5);
            BinNode<int> node2 = root.InsertAsRc(10);
            BinNode<int> node3 = node1.InsertAsLc(3);
            BinNode<int> node4 = node1.InsertAsRc(6);
            BinNode<int> node5 = node4.InsertAsRc(7);

            BinNode<int> node6 = node2.InsertAsLc(9);
            BinNode<int> node7 = node2.InsertAsRc(11);

            BinTreeLogHelper<int>.Log(binTree.Root);

            //Console.WriteLine("先序遍历");
            //binTree.TraversePreRecursion(binTree.Root);
            //Console.WriteLine();

            //binTree.TraversePre(binTree.Root);
            //Console.WriteLine();

            //binTree.TraversePre2(binTree.Root);
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine("中序遍历");
            //binTree.TraverseiInRecursion(binTree.Root);
            //Console.WriteLine();

            //binTree.TraverseIn(binTree.Root);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine("后序遍历");
            //binTree.TraverseiPostRecursion(binTree.Root);
            //Console.WriteLine();

            //binTree.TraversePost(binTree.Root);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine("层序遍历");
            //binTree.TraverseLevel(binTree.Root);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();


            Log.LogBinTreeTest<int> log = new Log.LogBinTreeTest<int>();
            log.TraverseIn(binTree.Root);
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

            return true;
        }

        public virtual BinNode<T> InsertAsRoot(T t)
        {
            _root = new BinNode<T>(t);
            return _root;
        }

        public virtual BinNode<T> InsertAsLc(BinNode<T> node, T t)
        {
            return node.InsertAsLc(t);
        }

        public virtual BinNode<T> InsertAsRc(BinNode<T> node, T t)
        {
            return node.InsertAsRc(t);
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

        public void TraverseLevel(BinNode<T> node)
        {
            Queue<BinNode<T>> queue = new Queue<BinNode<T>>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                node = queue.Dequeue();
                Console.Write(node.Value.ToString() + "    ");
                if (node.HasLChild())
                {
                    queue.Enqueue(node.LeftChild);
                }
                if (node.HasRChild())
                {
                    queue.Enqueue(node.RightChild);
                }
            }
        }
        #endregion

        public virtual void Release()
        {
            _root = null;
        }

    }
}
