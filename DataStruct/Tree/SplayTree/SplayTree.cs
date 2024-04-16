using DataStruct.BinTree;
using DataStruct.BSTree;
using DataStruct.Log;
using System;
using System.Collections.Generic;

namespace DataStruct.Tree.SplayTree
{

    public class SplayTreeTest
    {
        public static void Test()
        {
            SplayTree<int> splayTree = new SplayTree<int>();

            int[] arr = new int[] { 22, 10, 8, 15, 17, 20, 19, 21, 12, 13};
            //int[] arr = new int[] { 10, 20, 8, 19, 15, 17, 21};
            //int[] arr = new int[] { 10, 20, 8, 19};
            for (int i = 0; i < arr.Length; ++i)
            {
                //Console.WriteLine("Insert:" + arr[i]);
                splayTree.Insert(arr[i]);
                //BinTreeLogHelper<int>.Log(splayTree.Root, true);
                //Console.WriteLine();

                List<BinNode<int>> list = new List<BinNode<int>>();
                //LogBinTreeCheck<int>.Check(list);

                //
                //list = splayTree.TraverseLevel(splayTree.Root);
                //for (int j = 0; j < list.Count; ++j)
                //{
                //    BinNode<int> ttt = list[j];

                //    int data = ttt.Value;
                //    while (ttt.ParentNode != null)
                //    {
                //        ttt = ttt.ParentNode;
                //        if (data == ttt.Value)
                //        {
                //            int a = 0;
                //        }
                //    }
                //}
            }

            BinTreeLogHelper<int>.Log(splayTree.Root, false, false);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //splayTree.Remove(20);
            //BinTreeLogHelper<int>.Log(splayTree.Root, false, false);

            //for (int i = 0; i < arr.Length; ++i)
            //{
            //    Console.WriteLine("Remove:" + arr[i]);
            //    splayTree.Remove(arr[i]);
            //    BinTreeLogHelper<int>.Log(splayTree.Root, false, false);
            //    Console.WriteLine();
            //    List<BinNode<int>> list = splayTree.TraverseLevel(splayTree.Root);
            //    Console.WriteLine();
            //}

            //BinNode<int> root = splayTree.Insert(8);
            //BinNode<int> node10 = new BinNode<int>(10);
            //root.InsertAsRc(node10);

            //BinNode<int> node20 = new BinNode<int>(20);
            //node10.InsertAsRc(node20);

            //BinTreeLogHelper<int>.Log(splayTree.Root, true);

            splayTree.Search(19);
            BinTreeLogHelper<int>.Log(splayTree.Root, false, false);

            splayTree.Search(8);
            BinTreeLogHelper<int>.Log(splayTree.Root, false, false);

            splayTree.Remove(19);
            BinTreeLogHelper<int>.Log(splayTree.Root, false, false);

            //Console.WriteLine();
            //Console.WriteLine();
            //splayTree.Insert(19);
            //BinTreeLogHelper<int>.Log(splayTree.Root, true);
        }
    }

    /*
    伸展树 也叫自适应查找树
    伸展树实质上是一个二叉搜索树，包含插入、查询、删除等二叉搜索树所有操作，这些操作的时间复杂度为O(logN)

    与平衡二叉树相比
    1.平衡二叉树每个节点都要存储平衡信息(节点高度）
    2.执行插入、删除操作后需要回复平衡，重新计算节点高度，操作复杂度高
    3.对于简单的输入，性能提升并不明显

    平衡二叉树提升性能的地方
    1.平衡二叉树在最差的平均时间复杂度基本都是保持在O(logN)

    伸展树原理：假设一个节点在一次访问后，这个节点很可能不久会被再次访问。
    伸展树的做法是在每一次访问一个节点后，就通过一些列操作把这个节点挪移到树根位置
    尽管最坏情况下单次查询时间复杂度会达到 O(N），线性复杂度
    而实际证明伸展树保证在 M 次连续搜索的过程中时间复杂度不大于O(M*logN)
    */

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

            BinNode<T> temp = (null != node) ? node : _hot;
            Root = Splay(temp);// 将最后一个被访问的节点伸展至跟

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
            // 首先 Search，如果找到 t 则返回，否则 Root 即为与 t 相近的节点
            if (null != node && node.Element.CompareTo(t) == 0) 
            {
                return Root;
            }
  
            BinNode<T> newNode = new BinNode<T>(t);

            // 创建新节点，以下调整 <=7 个指针以完成局部重构
            // 情况一：
            // 如果 新插入节点值 > Root的值,调整结构                  newNode
            // newNode.LeftChild = Root 节点                     ----------------
            // newNode.RightChild = Root.RightChild              |              |
            //                                                  Root       Root.RightChild
            // 最后令 Root = newNode

            // 情况二：
            // 如果 新插入节点值 < Root 的值,调整结构                  newNode
            // newNode.LeftChild = Root.LeftChild                  ----------------- 
            // newNode.RightChild = Root                           |               |
            //                                               Root.LeftChild       Root
            // 最后令 Root = newNode

            BinNode<T> tempRoot = Root;
            if (Root.Element.CompareTo(t) < 0)  //插入新根，以Root和Root.RC为左、右孩子
            {
                BinNode<T> rootRightChild = Root.RightChild;
                //2 + 3个
                newNode.LeftChild = tempRoot;
                tempRoot.ParentNode = newNode;

                newNode.RightChild = rootRightChild;
                if (null != rootRightChild)
                {
                    rootRightChild.ParentNode = newNode;
                    Root.RightChild = null;
                }
                Root = newNode; 
            }
            else  //插入新根，以Root.LeftChild和Root为左、右孩子
            {
                BinNode<T> rootLeftChild = Root.LeftChild;
                newNode.LeftChild = rootLeftChild;
                //2 + 3个
                if (null != rootLeftChild)
                {
                    rootLeftChild.ParentNode = newNode;
                    tempRoot.LeftChild = null;
                }

                newNode.RightChild = tempRoot;
                tempRoot.ParentNode = newNode;
                Root = newNode;
            }

            CheckNode(Root);
            CheckNode(newNode);

            UpdateHeightAbove(node); //更新t及其祖先（实际上只有_root一个）的高度
            return Root; //新节点必然置于树根，返回之
        }

        private void CheckNode(BinNode<T> node)
        {
            BinNode<T> temp = node;
            T rootData = temp.Element;
            while (temp.ParentNode != null)
            {
                temp = temp.ParentNode;
                if (rootData.CompareTo(temp.Element) == 0)
                {
                    int a = 0;
                }
            }
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
            if (null == node || node.Element.CompareTo(t) != 0)
            {
                return false;
            }
  
            BinNode<T> tempRoot = Root; //assert: 经search()后节点e已被伸展至树根
            if (!Root.HasLChild())      //若无左子树，则直接删除
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
                BinNode<T> LTree = Root.LeftChild;
                LTree.ParentNode = null;
                Root.LeftChild = null; //暂时将左子树切除
                Root = Root.RightChild;
                Root.ParentNode = null; //只保留右子树
                Search(tempRoot.Element); //以原树根为目标，做一次（必定失败的）查找
                                 ///// assert: 至此，右子树中最小节点必伸展至根，且（因无类同节点）其左子树必空，于是
                Root.LeftChild = LTree;
                LTree.ParentNode = Root; //只需将原左子树接回原位即可
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
                    AttachAsRChild(g, p.LeftChild);
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
                //Console.WriteLine(parent.Element.ToString() + "  LeftChild:" + lc.Element.ToString());
            }
        }

        protected void AttachAsRChild(BinNode<T> parent, BinNode<T> rc)
        {
            parent.RightChild = rc;
            if (null != rc)
            {
                rc.ParentNode = parent;
                //Console.WriteLine(parent.Element.ToString() + "  RightChild:" + rc.Element.ToString());
            }
        }
    }
}
