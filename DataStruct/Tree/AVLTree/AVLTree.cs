using System;
using System.Collections.Generic;
using DataStruct.BinTree;
using DataStruct.BSTree;

namespace DataStruct.Tree.AVLTree
{
    public class AVLTest
    {
        public static void Test()
        {
            AVLTree<int> aVLTree = new AVLTree<int>();
            int[] arr = new int[] { 10, 8, 15, 17, 20, 19, 21, 12, 13, 6, 9, 16, 22, };
            for (int i = 0; i < arr.Length; ++i)
            {
                Console.WriteLine("Insert:" + arr[i]);
                aVLTree.Insert(arr[i]);
                Console.WriteLine("===============================================");
                BinTreeLogHelper<int>.Log(aVLTree.Root, false, false);

                Console.WriteLine("===============================================");
                List<BinNode<int>> list = aVLTree.TraverseLevel(aVLTree.Root);
                Console.WriteLine();

                for (int n = 0; n < list.Count; ++n)
                {
                    BinNode<int> node = list[n];
                    Console.WriteLine(list[n].Element.ToString() + "   heigh:" + list[n].Height);
                }
            }

            BinTreeLogHelper<int>.Log(aVLTree.Root, false, false);
            Console.WriteLine("===============================================");

            Console.WriteLine("Star Remove ===================================");
            for (int i = 0; i < arr.Length; ++i)
            {
                aVLTree.Remove(arr[i]);
                Console.WriteLine("===============================================");
                BinTreeLogHelper<int>.Log(aVLTree.Root, false, false);

                Console.WriteLine("===============================================");
                List<BinNode<int>> list = aVLTree.TraverseLevel(aVLTree.Root);
                Console.WriteLine();

                Console.WriteLine();
                for (int n = list.Count - 1; n >= 0; --n)
                {
                    BinNode<int> node = list[n];
                    int heigh = node.Height;
                    //aVLTree.UpdateHeight(node);
                    //if (heigh == node.Height)
                    //{
                    //    Console.WriteLine(list[n].Value.ToString() + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                    //}
                    Console.WriteLine(list[n].Element.ToString() + "  heigh:" + heigh + "   " + (node.Height == heigh));
                }
            }
        }
    }

    /// <summary>
    /// 二叉搜索树
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class AVLTree<T> : BSTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// 插入
        /// </summary>
        public override BinNode<T> Insert(T t)
        {
            BinNode<T> node = Search(t);
            if (null != node)
            {
                return node;
            }

            //按照二叉搜索树的方式将值插入
            node = Insert(t, _hot);

            // 一：从 BinNode<T> g = _hot节点(新插入节点的父节点)开始,
            // 二：如果节点 g 不平衡则旋转 g,使 g 平衡，到此结束
            // 三：令  g = g.ParentNode 跳转到 二 继续执行
            // 因为插入新节点只会导致一个节点失衡，所以一旦找到一个
            // 不平衡的节点，使之平衡后则树恢复平衡，结束退出
            for (BinNode<T> g = _hot; null != g; g = g.ParentNode)
            {
                if (!AvlBalanced(g))
                {
                    if (g.IsRoot())
                    {
                        Root = RotateAt(TallerChild(TallerChild(g)));
                    }
                    else if (g.IsLChild())
                    {
                        g.ParentNode.LeftChild = RotateAt(TallerChild(TallerChild(g)));
                        UpdateHeightAbove(g.ParentNode.LeftChild);
                    }
                    else
                    {
                        g.ParentNode.RightChild = RotateAt(TallerChild(TallerChild(g)));
                        UpdateHeightAbove(g.ParentNode.RightChild);
                    }

                    break;
                }
                else
                {
                    UpdateHeight(g);
                }
            }

            return node;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public override bool Remove(T t)
        {
            BinNode<T> node = Search(t);
            if (null == node)
            {
                return false;
            }

            // 按照二叉搜索树删除节点
            Remove(node, ref _hot);

            // 一：从 BinNode<T> g = _hot节点(新插入节点的父节点)开始,
            // 二：如果节点 g 不平衡则旋转 g，使节点 g 平衡。
            // 三：令 g = g.ParentNode,跳转到 二 继续执行
            // 删除节点后失衡比较复杂，因为删除一个节点后可能会导致一
            // 个节点失衡，旋转使其平衡后可能会导致其父节点再次失衡，
            // 最坏情况会导致每次调整后其父节点会再次的失衡，直到跟节
            BinNode<T> g = _hot;
            while(null != g)  //从_hot出发向上，逐层检查各代祖先g
            { 
                if (!AvlBalanced(g)) //一旦发现g失衡，则（采用“3 + 4”算法）使之复衡，并将该子树联至
                {
                    if (g.IsRoot())
                    {
                        Root = RotateAt(TallerChild(TallerChild(g)));  //原父亲
                        g = Root;
                    }
                    else if (g.IsLChild())
                    {
                        g.ParentNode.LeftChild = RotateAt(TallerChild(TallerChild(g)));  //原父亲
                        g = g.ParentNode.LeftChild;
                    }
                    else
                    {
                        g.ParentNode.RightChild = RotateAt(TallerChild(TallerChild(g)));  //原父亲
                        g = g.ParentNode.RightChild;
                    }
                }
                else
                {
                    UpdateHeight(g);
                }

                g = g.ParentNode;
            }

            return true;
        }

        /// <summary>
        /// 理想平衡
        /// </summary>
        public bool Balanced(BinNode<T> node)
        {
            return NodeHeight(node.LeftChild) == NodeHeight(node.RightChild);
        }

        /// <summary>
        /// 平衡因子
        /// </summary>
        public int BalFac(BinNode<T> node)
        {
            return NodeHeight(node.LeftChild) - NodeHeight(node.RightChild);
        }

        /// <summary>
        /// AVL 平衡条件
        /// </summary>
        public bool AvlBalanced(BinNode<T> node)
        {
            return -1 <= BalFac(node) && BalFac(node) <= 1; 
        }

        // 在 左、右 孩子中取更高者
        protected BinNode<T> TallerChild(BinNode<T> node)
        {
            int balFac = BalFac(node);
            if (balFac > 0) // 左高
            {
                return node.LeftChild;
            }
            else if (balFac < 0) // 右高
            {
                return node.RightChild;
            }
            else // 等高：与父亲x同侧者
            {
                return node.IsLChild() ? node.LeftChild : node.RightChild;
            }
        }
    }
}
