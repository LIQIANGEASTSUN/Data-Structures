﻿using System;
using System.Collections.Generic;
using DataStruct.BinTree;
using DataStruct.BSTree;

namespace DataStruct.Tree.AVL
{
    public class AVLTest
    {
        public static void Test()
        {
            AVL<int> aVL = new AVL<int>();
            int[] arr = new int[] { 10, 8, 15, 17, 20, 19, 21, 12, 13, 6, 9, 16, 22, };
            for (int i = 0; i < arr.Length; ++i)
            {
                Console.WriteLine("Insert:" + arr[i]);
                aVL.Insert(arr[i]);
                Console.WriteLine("===============================================");
                BinTreeLogHelper<int>.Log(aVL.Root, false);

                Console.WriteLine("===============================================");
                List<BinNode<int>> list = aVL.TraverseLevel(aVL.Root);
                Console.WriteLine();

                for (int n = 0; n < list.Count; ++n)
                {
                    BinNode<int> node = list[n];
                    Console.WriteLine(list[n].Value.ToString() + "   heigh:" + list[n].Height);
                }
            }

            BinTreeLogHelper<int>.Log(aVL.Root, false);
            Console.WriteLine("===============================================");

            Console.WriteLine("Star Remove ===================================");
            for (int i = 0; i < arr.Length; ++i)
            {
                Console.WriteLine("Remove:" + arr[i]);
                aVL.Remove(arr[i]);
                Console.WriteLine("===============================================");
                BinTreeLogHelper<int>.Log(aVL.Root, false);

                Console.WriteLine("===============================================");
                List<BinNode<int>> list = aVL.TraverseLevel(aVL.Root);
                Console.WriteLine();

                Console.WriteLine();
                for (int n = list.Count - 1; n >= 0; --n)
                {
                    BinNode<int> node = list[n];
                    int heigh = node.Height;
                    aVL.UpdateHeight(node);
                    //if (heigh == node.Height)
                    //{
                    //    Console.WriteLine(list[n].Value.ToString() + "  heigh:" + list[n].Height + "    Error Error Error Error Error Error");
                    //}
                    Console.WriteLine(list[n].Value.ToString() + "  heigh:" + heigh + "   " + (node.Height == heigh));
                }
            }
        }
    }

    class AVL<T> : BSTree<T> where T : IComparable<T>
    {
        public override BinNode<T> Insert(T t)
        {
            BinNode<T> node = Search(t);
            if (null != node)
            {
                return node;
            }
            node = Insert(t, _hot);

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

        public override bool Remove(T t)
        {
            BinNode<T> node = Search(t);
            if (null == node)
            {
                return false;
            }
            Remove(node, ref _hot);

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
