using DataStruct.BinTree;
using DataStruct.BSTree;
using System;
using System.Collections.Generic;

namespace DataStruct.Tree.SplayTree
{

    public class SplayTreeTest
    {
        public static void Test()
        {
            SplayTree<int> splayTree = new SplayTree<int>();

            //int[] arr = new int[] { 10, 8, 15, 17, 20, 19, 21, 12, 13, 6, 9, 16, 22, };
            int[] arr = new int[] { 10, 8, 15, 17, 20, 19, 21};
            for (int i = 0; i < arr.Length; ++i)
            {
                Console.WriteLine("Insert:" + arr[i]);
                splayTree.Insert(arr[i]);
                BinTreeLogHelper<int>.Log(splayTree.Root, false);
                Console.WriteLine();
            }

        }
    }


    /// <summary>
    /// 伸展树
    /// </summary>
    class SplayTree<T> : BSTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// 查找：伸展树的查找也会引起整树的结构调整，固需重写
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override BinNode<T> Search(T t)
        {
            BinNode<T> node = base.Search(t);

            Root = Splay((null != node) ? node : _hot);// 将最后一个被访问的节点伸展至跟

            return Root;
        }

        /// <summary>
        /// 插入
        /// </summary>
        public override BinNode<T> Insert(T t)
        {
            if (null == Root)  // 树为空
            {
                Root = new BinNode<T>(t);
                return Root;
            }

            BinNode<T> node = Search(t);
            if (null != node && node.Value.CompareTo(t) == 0)
            {
                return Root;
            }

            node = Root;// 创建新节点，以下调整 <=7 个指针以完成局部重构

            if (Root.Value.CompareTo(t) == 0)  //插入新根，以t和t->rc为左、右孩子
            {
                //2 + 3个
                BinNode<T> newNode = new BinNode<T>(t);
                newNode.LeftChild = node;
                newNode.RightChild = node.RightChild;

                Root = newNode; 
                node.ParentNode = Root;

                if (node.HasRChild())
                {
                    node.RightChild.ParentNode = Root;
                    node.RightChild = null;
                } //<= 2个
            }
            else  //插入新根，以t.LeftChild和t为左、右孩子
            {

                //2 + 3个
                BinNode<T> newNode = new BinNode<T>(t);
                newNode.LeftChild = node.LeftChild;
                newNode.RightChild = node;

                Root = newNode;
                node.ParentNode = Root;

                if (node.HasLChild())
                {
                    node.LeftChild.ParentNode = Root;
                    node.LeftChild = null;
                } //<= 2个
            }
            UpdateHeightAbove(node); //更新t及其祖先（实际上只有_root一个）的高度
            return Root; //新节点必然置于树根，返回之
        }

        /// <summary>
        /// 删除
        /// </summary>
        public override bool Remove(T t)
        {
            if (null == Root)
            {
                return false;
            }
            BinNode<T> node = Search(t);
            //若树空或目标不存在，则无法删除
            if (null == node || node.Value.CompareTo(t) != 0)
            {
                return false;
            }


            BinNode<T> w = Root; //assert: 经search()后节点e已被伸展至树根
            if (!Root.HasLChild())   //若无左子树，则直接删除
            { 
                Root = Root.RightChild;
                if (null != Root)
                    Root.ParentNode = null;
            }
            else if (!Root.HasRChild())  //若无右子树，也直接删除
            { 
                Root = Root.LeftChild;
                if (null != Root)
                    Root.ParentNode = null;
            }
            else
            { //若左右子树同时存在，则
                BinNode<T> lTree = Root.LeftChild;
                lTree.ParentNode = null;
                Root.LeftChild = null; //暂时将左子树切除
                Root = Root.RightChild;
                Root.ParentNode = null; //只保留右子树
                Search(w.Value); //以原树根为目标，做一次（必定失败的）查找
                                 ///// assert: 至此，右子树中最小节点必伸展至根，且（因无雷同节点）其左子树必空，于是
                Root.LeftChild = lTree; lTree.ParentNode = Root; //只需将原左子树接回原位即可
            }

            if (null != Root)
                UpdateHeight(Root); //此后，若树非空，则树根的高度需要更新
            return true; //返回成功标志
        }

        /// <summary>
        /// 将节点 node 伸展至跟
        /// </summary>
        protected BinNode<T> Splay(BinNode<T> v)
        {
            if (null == v)
            {
                return null;
            }

            // node 的父亲与祖父
            BinNode<T> p;
            BinNode<T> g;
            while ((p = v.ParentNode) != null && (g = p.ParentNode) != null) //自下而上，反复对 v 做双层伸展
            {
                BinNode<T> gg = g.ParentNode; // 每轮之后 v 都以原曾祖父(great-grand parent)为父
                if (v.IsLChild())
                {
                    if (p.IsLChild()) // zig-zig
                    {
                        AttachAsLChild(g, p.RightChild);
                        AttachAsLChild(p, v.RightChild);
                        AttachAsRChild(p, g);
                        AttachAsRChild(v, p);
                    }
                    else
                    {
                        AttachAsLChild(p, v.RightChild);
                        AttachAsRChild(g, v.LeftChild);
                        AttachAsLChild(v, g);
                        AttachAsRChild(v, p);
                    }
                }
                else if (p.IsRChild())
                {
                    AttachAsRChild(g, p.RightChild);
                    AttachAsRChild(p, v.LeftChild);
                    AttachAsLChild(p, g);
                    AttachAsLChild(v, p);
                }
                else
                {
                    AttachAsRChild(p, v.LeftChild);
                    AttachAsLChild(g, v.RightChild);
                    AttachAsRChild(v, g);
                    AttachAsLChild(v, p);
                }

                if (null == gg)
                {
                    v.ParentNode = null;// 若 v 原先的曾祖父 gg 不存在，则v现在应为树根
                }
                else
                {
                    if (g == gg.LeftChild)
                    {
                        AttachAsLChild(gg, v);
                    }
                    else
                    {
                        AttachAsRChild(gg, v);
                    }
                }

                UpdateHeight(g);
                UpdateHeight(p);
                UpdateHeight(v);
            }//双层伸展结束时，必有g == null，但p可能非空

            p = v.ParentNode;
            if (null != p) //若p果真非空，则额外再做一次单旋
            {
                if (v.IsLChild()) {
                    AttachAsLChild(p, v.RightChild);
                    AttachAsRChild(v, p);
                }
                else {
                    AttachAsRChild(p, v.LeftChild);
                    AttachAsLChild(v, p);
                }
                UpdateHeight(p);
                UpdateHeight(v);
            }

            v.ParentNode = null;
            return v;
        }

        protected void AttachAsLChild(BinNode<T> parent, BinNode<T> lc)
        {
            parent.LeftChild = lc;
            if (null != lc)
            {
                lc.ParentNode = parent;
            }
        }

        protected void AttachAsRChild(BinNode<T> parent, BinNode<T> rc)
        {
            parent.RightChild = rc;
            if (null != rc)
            {
                rc.ParentNode = parent;
            }
        }

    }

}
