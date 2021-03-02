using DataStruct.BinTree;
using DataStruct.BSTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Tree.RedBlackTree
{
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
            return base.Remove(t);
        }

        /// <summary>
        /// 双红修正
        /// </summary>
        protected void SolveDoubleRed(BinNode<T> node)
        {

        }

        /// <summary>
        /// 双黑修正
        /// </summary>
        protected void SolveDoubleBlack(BinNode<T> node)
        {

        }

        protected override int UpdateHeight(BinNode<T> node)
        {
            node.Height = Math.Max(NodeHeight(node.LeftChild), NodeHeight(node.RightChild));
            if(node.IsBlack()) // 只记黑节点
            {
                node.Height++;
            }
            return node.Height;
        }

    }
}
