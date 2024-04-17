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

            BinTreeLogHelper<int>.Log(binTree.Root, false, false);

            Console.WriteLine();
            List<BinNode<int>> list = binTree.TraverseLevel(binTree.Root);
            for (int i = 0; i < list.Count; ++i)
            {
                Console.WriteLine("heigh:" + list[i].Element.ToString() + "  heigh:" + list[i].Height);
            }

            binTree.Remove(node2);
            BinTreeLogHelper<int>.Log(binTree.Root, false, false);

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
                Console.WriteLine("heigh:" + list[i].Element.ToString() + "  heigh:" + list[i].Height);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 二叉树
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinTree<T> where T : IComparable<T>
    {
        private BinNode<T> _root = null;   // 跟节点

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

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
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
            return true;
        }

        /// <summary>
        /// 清空树，并创建新的根节点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual BinNode<T> InsertAsRoot(T t)
        {
            _root = new BinNode<T>(t);
            UpdateHeightAbove(_root);
            return _root;
        }

        /// <summary>
        /// 作为当前节点的左孩子插入新节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual BinNode<T> InsertAsLc(BinNode<T> node, T t)
        {
            node.InsertAsLc(t);
            UpdateHeightAbove(node);
            return node.LeftChild;
        }

        /// <summary>
        /// 作为当前节点的右孩子插入新节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual BinNode<T> InsertAsRc(BinNode<T> node, T t)
        {
            node.InsertAsRc(t);
            UpdateHeightAbove(node);
            return node.RightChild;
        }

        #region Recursion
        /// <summary>
        /// 递归实现 先序遍历：跟节点->左子树->右子树
        /// </summary>
        /// <param name="node"></param>
        public void TraversePreRecursion(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            Console.Write(node.Element.ToString() + "    ");
            TraversePreRecursion(node.LeftChild);
            TraversePreRecursion(node.RightChild);
        }

        /// <summary>
        /// 递归实现 中序遍历：左子树->跟节点->右子树  递归实现
        /// </summary>
        /// <param name="node"></param>
        public void TraverseiInRecursion(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            TraverseiInRecursion(node.LeftChild);
            Console.Write(node.Element.ToString() + "    ");
            TraverseiInRecursion(node.RightChild);
        }

        /// <summary>
        /// 递归实现 后序遍历：左子树->右子树->跟节点
        /// </summary>
        /// <param name="node"></param>
        public void TraverseiPostRecursion(BinNode<T> node)
        {
            if (null == node)
            {
                return;
            }

            TraverseiPostRecursion(node.LeftChild);
            TraverseiPostRecursion(node.RightChild);
            Console.Write(node.Element.ToString() + "    ");
        }
        #endregion

        #region Iteration
        /// <summary>
        /// 迭代实现 先序遍历：跟节点->左子树->右子树
        /// </summary>
        /// <param name="node"></param>
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
                Console.Write(node.Element.ToString() + "    ");
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

        /// <summary>
        /// 迭代实现 先序遍历：跟节点->左子树->右子树
        /// </summary>
        /// <param name="node"></param>
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
                    Console.Write(node.Element.ToString() + "    ");
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

        /// <summary>
        /// 迭代实现 中序遍历：左子树->跟节点->右子树
        /// </summary>
        /// <param name="node"></param>
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
                Console.Write(node.Element.ToString() + "    ");
                if (null != node && ((node = node.RightChild) != null))
                {
                    stack.Push(node);
                }
            }
        }

        /// <summary>
        /// 迭代实现 后序遍历：左子树->右子树->跟节点
        /// </summary>
        /// <param name="node"></param>
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
                Console.Write(temp.Element.ToString() + "    ");
            }

            Console.WriteLine();
        }

        //、层序遍历：按层从上到下，每层从左到右依次遍历
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
                Console.Write(node.Element.ToString() + "   ");
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

        public IList<IList<T>> LevelOrder(BinNode<T> root)
        {
            IList<IList<T>> resultList = new List<IList<T>>();
            if (null == root)
            {
                return resultList;
            }
            Queue<BinNode<T>> queue = new Queue<BinNode<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                // 当前层节点个数
                int count = queue.Count;
                List<T> list = new List<T>();

                // 从 queue 中取出来前 count 个，就是当前层的节点
                while (count > 0)
                {
                    --count;
                    BinNode<T> node = queue.Dequeue();
                    list.Add(node.Element);
                    // 当前层节点的子节点加入队列中
                    if (node.LeftChild != null)
                    {
                        queue.Enqueue(node.LeftChild);
                    }
                    if (node.RightChild != null)
                    {
                        queue.Enqueue(node.RightChild);
                    }
                }
                resultList.Add(list);
            }

            return resultList;
        }
        #endregion

        /// <summary>
        /// 更新树高度
        /// </summary>
        /// <param name="node"></param>
        protected void UpdateHeightAbove(BinNode<T> node)
        {
            while (null != node) // 从node出发，覆盖历代祖先
            {
                UpdateHeight(node);
                node = node.ParentNode;
            }
        }

        /// <summary>
        /// 更新树高度
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual int UpdateHeight(BinNode<T> node)
        {
            node.Height = 1 + Math.Max(NodeHeight(node.LeftChild), NodeHeight(node.RightChild));
            return node.Height;
        }

        /// <summary>
        /// 后去节点的高度
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected int NodeHeight(BinNode<T> node)
        {
            return (null != node) ? node.Height : -1;
        }

        public virtual void Release()
        {
            _root = null;
        }
    }
}