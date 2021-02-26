using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    int deep = -1;
                    while (null != node)
                    {
                        ++deep;
                        node = node.ParentNode;
                    }
                    //if (deep != list[n].Deep)
                    //{
                    //    Console.WriteLine(list[n].Value.ToString() + "  deep:" + list[n].Deep + "    Error Error Error Error Error Error");
                    //}
                }
            }

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

                for (int n = 0; n < list.Count; ++n)
                {
                    BinNode<int> node = list[n];
                    Console.WriteLine(list[n].Value.ToString() + "   heigh:" + list[n].Height);
                    int deep = -1;
                    while (null != node)
                    {
                        ++deep;
                        node = node.ParentNode;
                    }
                    //if (deep != list[n].Deep)
                    //{
                    //    Console.WriteLine(list[n].Value.ToString() + "  deep:" + list[n].Deep + "    Error Error Error Error Error Error");
                    //}
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

            BinTreeLogHelper<T>.Log(Root, false);

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
                    }
                    else
                    {
                        g.ParentNode.RightChild = RotateAt(TallerChild(TallerChild(g)));
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
            if (!base.Remove(t))
            {
                return false;
            }

            BinNode<T> g = _hot;
            while (null != g)  //从_hot出发向上，逐层检查各代祖先g
            { 
                if (!AvlBalanced(g)) //一旦发现g失衡，则（采用“3 + 4”算法）使之复衡，并将该子树联至
                {
                    g = RotateAt(TallerChild(TallerChild(g)));  //原父亲
                }

                UpdateHeightAbove(g); // 并更新其高度（注意：即便g未失衡，高度亦可能降低）
                g = g.ParentNode;
            }

            return true;
        }

        // BST 节点旋转变换统一算法(3节点 + 4子树)，返回调整之后局部子树根节点的位置
        // 注意：尽管子树根会正确指向上层节点（如果存在），但反向的联接须由上层函数完成
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
                UpdateHeightAbove(a);
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
                UpdateHeightAbove(c);
            }

            b.LeftChild = a;
            a.ParentNode = b;

            b.RightChild = c;
            c.ParentNode = b;
            UpdateHeightAbove(b);

            return b;
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

        // 在 左、右 孩子中去更高者
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

        //public BinNode<T> TallerChild(BinNode<T> x)
        //{
        //    return NodeHeight(x.LeftChild) > NodeHeight(x.RightChild) ? x.LeftChild : (NodeHeight(x.LeftChild) < NodeHeight(x.RightChild) ? x.RightChild : ( x.IsLeaf() ? x.LeftChild : x.RightChild));
        //}

    }
}
