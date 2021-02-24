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

            binTree.TraversePreRecursion(binTree.Root);
            Console.WriteLine();

            binTree.TraversePre(binTree.Root);
            Console.WriteLine();


            binTree.TraversePre(null);
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

        //先序遍历：先跟->左->右  递归实现
        public void TraversePreRecursion(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            Console.Write(node.Value.ToString() + "    ");
            if (node.HasLChild())
            {
                TraversePreRecursion(node.LeftChild);
            }
            if (node.HasRChild())
            {
                TraversePreRecursion(node.RightChild);
            }
        }

        //先序遍历：先跟->左->右  迭代实现
        public void TraversePre(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            Stack<BinNode<T>> stack = new Stack<BinNode<T>>();
            stack.Push(node);

            StringBuilder sb = new StringBuilder();
            while (stack.Count > 0)
            {
                while(null != node)
                {
                    Console.Write(node.Value.ToString() + "    ");
                    node = node.LeftChild;
                    if (null != node)
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

        public virtual void Release()
        {
            _root = null;
        }

    }
}
