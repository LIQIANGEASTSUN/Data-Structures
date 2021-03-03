using DataStruct.BinTree;
using DataStruct.BSTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Tree.RedBlackTree
{

    public class RedBlackTest
    {

        public static void Test()
        {
            RedBlackTree<int> rbTree = new RedBlackTree<int>();

            int[] arr = new int[] { 10, 8, 15, 17, 20, 19, 21, 12, 13, 6, 9, 16, 22, };
            for (int i = 0; i < arr.Length; ++i)
            {
                rbTree.Insert(arr[i]);
            }

            BinTreeLogHelper<int>.Log(rbTree.Root, true, false);

        }

    }

    /// <summary>
    /// 红黑树
    /// (1)树根：必为黑色
    /// (2)外部节点：均为黑色
    /// (3)其余节点：若为红，则只能有黑色孩子。（所以红色节点的父节点一定是黑色）
    /// (4)外部节点到根：途中黑节点数目相等
    /// </summary>
    public class RedBlackTree<T> : BSTree<T> where T : IComparable<T>
    {
        public override BinNode<T> Insert(T t)
        {
            BinNode<T> node = Search(t);
            if (null != node)
            {
                return node;
            }
            //创建新节点以 _hot 为父亲
            node = Insert(t, _hot);
            node.Color = Color.Red;

            //双红修正
            SolveDoubleRed(node);

            return null != node ? node : node.ParentNode;
        }

        public override bool Remove(T t)
        {
            BinNode<T> node = Search(t);
            if (null == node)
            {
                return false;
            }

            BinNode<T> r = Remove(node, ref _hot);
            if (null == Root)
            {
                return true;
            }

            // assert: _hot某一孩子刚被删除，且被r所指节点（可能是null）接替。以下检查是否失衡，并做必要调整
            if (null == _hot) //若刚被删除的是根节点，则将其置黑，并更新黑高度
            {
                Root.Color = Color.Black;
                UpdateHeight(Root);
                return true;
            }
            // assert: 以下，原x（现r）必非根，_hot必非空
            if (BlackHeightUpdated(_hot))
            {
                return true; //若所有祖先的黑深度依然平衡，则无需调整
            }

            if (IsRed(r)) //否则，若r为红，则只需令其转黑
            {
                r.Color = Color.Black;
                r.Height++;
                return true;
            }
            // assert: 以下，原x（现r）均为黑色

            SolveDoubleBlack(r); //经双黑调整后返回
            return true; 
        }

        /// <summary>
        /// 双红修正
        /// </summary>
        protected void SolveDoubleRed(BinNode<T> x)
        {
            if (x.IsRoot()) //若已（递归）转至树根，则将其转黑，整树黑高度也随之递增
            {
                Root.Color = Color.Black;
                Root.Height++; return;
            } //否则，x的父亲p必存在

            BinNode<T> p = x.ParentNode;
            if (IsBlack(p))
            {
                return; //若p为黑，则可终止调整。否则
            }

            BinNode<T> g = p.ParentNode; //既然p为红，则x的祖父必存在，且必为黑色
            BinNode<T> u = Uncle(x); //以下，视x叔父u的颜色分别处理
            if (null == u)
            {
                return;
            }
            if (IsBlack(u))
            { //u为黑色（含null）时 //*DSA*/printf("  case RR-1:\n");
                if (x.IsLChild() == p.IsLChild()) //若x与p同侧（即zIg-zIg或zAg-zAg），则
                {
                    p.Color = Color.Black; //p由红转黑，x保持红
                }
                else //若x与p异侧（即zIg-zAg或zAg-zIg），则
                {
                    x.Color = Color.Black; //x由红转黑，p保持红
                }
                g.Color = Color.Red; //g必定由黑转红
                                   ///// 以上虽保证总共两次染色，但因增加了判断而得不偿失
                                   ///// 在旋转后将根置黑、孩子置红，虽需三次染色但效率更高
                BinNode<T> gg = g.ParentNode; //曾祖父（great-grand parent）

                BinNode<T> r = null;//调整后的子树根节点
                if (g.IsRoot())
                {
                    r = Root = RotateAt(x); //调整后的子树根节点
                }
                else if (g.IsLChild())
                {
                    r = g.ParentNode.LeftChild = RotateAt(x); //调整后的子树根节点
                }
                else
                {
                    r = g.ParentNode.RightChild = RotateAt(x); //调整后的子树根节点
                }

                r.ParentNode = gg; //与原曾祖父联接
            }
            else
            { //若u为红色 //*DSA*/printf("  case RR-2:\n");
                p.Color = Color.Black; p.Height++; //p由红转黑
                u.Color = Color.Black; u.Height++; //u由红转黑
                if (!g.IsRoot())
                {
                    g.Color = Color.Red; //g若非根，则转红
                }
                SolveDoubleRed(g); //继续调整g（类似于尾递归，可优化为迭代形式）
            }
        }

        /// <summary>
        /// 双黑修正
        /// </summary>
        protected void SolveDoubleBlack(BinNode<T> r)
        {
            BinNode<T> p = null != r ? r.ParentNode : _hot;
            if (null == p)
            {
                return; //r的父亲
            }
            BinNode<T> s = (r == p.LeftChild) ? p.RightChild : p.LeftChild; //r的兄弟
            if (IsBlack(s))
            { //兄弟s为黑
                BinNode<T> t = null; //s的红孩子（若左、右孩子皆红，左者优先；皆黑时为null）
                if (IsRed(s.RightChild))
                {
                    t = s.RightChild; //右子
                }
                if (IsRed(s.LeftChild))
                {
                    t = s.LeftChild; //左子
                }
                if (null != t)
                { //黑s有红孩子：BB-1
                  //*DSA*/printf("  case BB-1: Child ("); print(s.LeftChild); printf(") of BLACK sibling ("); print(s); printf(") is RED\n");
                    Color oldColor = p.Color; //备份原子树根节点p颜色，并对t及其父亲、祖父
                                              // 以下，通过旋转重平衡，并将新子树的左、右孩子染黑
                    BinNode<T> b = null;
                    if (p.IsRoot())
                    {
                        b = Root = RotateAt(t); //旋转
                    }
                    else if (p.IsLChild())
                    {
                        b = p.ParentNode.LeftChild = RotateAt(t);  //旋转
                    }
                    else
                    {
                        b = p.ParentNode.RightChild = RotateAt(t);  //旋转
                    }

                    if (b.HasLChild())
                    {
                        b.LeftChild.Color = Color.Black;
                        UpdateHeight(b.LeftChild);
                    } //左子
                    if (b.HasRChild())
                    {
                        b.RightChild.Color = Color.Black;
                        UpdateHeight(b.RightChild);
                    } //右子
                    b.Color = oldColor; UpdateHeight(b); //新子树根节点继承原根节点的颜色
                                                          //*DSA*/printBinTree(b, 0, 0);
                }
                else
                { //黑s无红孩子
                    s.Color = Color.Red; s.Height--; //s转红
                    if (IsRed(p))
                    { //BB-2R
                      //*DSA*/printf("  case BB-2R: Both children ("); print(s.LeftChild); printf(") and ("); print(s.RightChild); printf(") of BLACK sibling ("); print(s); printf(") are BLACK, and parent ("); print(p); printf(") is RED\n"); //s孩子均黑，p红
                        p.Color = Color.Black; //p转黑，但黑高度不变
                                             //*DSA*/printBinTree(p, 0, 0);
                    }
                    else
                    { //BB-2B
                      //*DSA*/printf("  case BB-2R: Both children ("); print(s.LeftChild); printf(") and ("); print(s.RightChild); printf(") of BLACK sibling ("); print(s); printf(") are BLACK, and parent ("); print(p); printf(") is BLACK\n"); //s孩子均黑，p黑
                        p.Height--; //p保持黑，但黑高度下降
                                    //*DSA*/printBinTree(p, 0, 0);
                        SolveDoubleBlack(p); //递归上溯
                    }
                }
            }
            else
            { //兄弟s为红：BB-3
              //*DSA*/printf("  case BB-3: sibling ("); print(s); printf(" is RED\n"); //s红（双子俱黑）
                s.Color = Color.Black;
                p.Color = Color.Red; //s转黑，p转红
                BinNode<T> t = s.IsLChild() ? s.LeftChild : s.RightChild; //取t与其父s同侧
                _hot = p;
                if (p.IsRoot())
                {
                    Root = RotateAt(t); //对t及其父亲、祖父做平衡调整
                }
                else if (p.IsLChild())
                {
                    p.ParentNode.LeftChild = RotateAt(t); //对t及其父亲、祖父做平衡调整
                }
                else
                {
                    p.ParentNode.RightChild = RotateAt(t); //对t及其父亲、祖父做平衡调整
                }

                SolveDoubleBlack(r); //继续修正r处双黑——此时的p已转红，故后续只能是BB-1或BB-2R
            }
        }

        protected override int UpdateHeight(BinNode<T> node)
        {
            node.Height = Math.Max(NodeHeight(node.LeftChild), NodeHeight(node.RightChild));
            if(IsBlack(node)) // 只记黑节点
            {
                node.Height++;
            }
            return node.Height;
        }

        protected bool BlackHeightUpdated(BinNode<T> node)
        {
            int Height = IsRed(node) ? NodeHeight(node.LeftChild) : NodeHeight(node.RightChild) + 1;
            if (   (NodeHeight(node.LeftChild) == NodeHeight(node.RightChild))
                && (node.Height == Height)
                )
            {
                return true;
            }

            return false;
        }

        public BinNode<T> Uncle(BinNode<T> node)
        {
            if (node.ParentNode.IsLChild())
            {
                return node.ParentNode.ParentNode.RightChild;
            }
            return node.ParentNode.ParentNode.LeftChild;
        }

        // 外部节点也视作黑节点
        private bool IsBlack(BinNode<T> node)
        {
            return null == node || node.Color == Color.Black;
        }

        // 非黑即红
        private bool IsRed(BinNode<T> node)
        {
            return !IsBlack(node);
        }
    }
}
