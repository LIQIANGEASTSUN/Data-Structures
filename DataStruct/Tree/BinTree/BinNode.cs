using System;

namespace DataStruct.BinTree
{
    /// <summary>
    /// 红黑树节点颜色枚举
    /// </summary>
    public enum Color
    {
        Red, 
        Black,
    }

    /// <summary>
    /// 二叉树节点定义
    /// </summary>
    /// <typeparam name="T">节点存储数据的类型</typeparam>
    public class BinNode<T> where T : IComparable<T>
    {
        // 节点值
        private T _element;
        // 父节点
        private BinNode<T> _parentNode;
        // 左子树
        private BinNode<T> _leftChild;
        // 右子树
        private BinNode<T> _rightChild;
        // 树高度
        private int _height;
        // 红黑树节点颜色
        private Color _color;

        public BinNode(T value)
        {
            _element = value;
        }

        public BinNode(T value, BinNode<T> parent)
        {
            _element = value;
            ParentNode = parent;
        }

        public BinNode<T> ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }

        /// <summary>
        /// 获取左子树
        /// </summary>
        public BinNode<T> LeftChild
        {
            get { return _leftChild; }
            set { _leftChild = value; }
        }

        /// <summary>
        /// 获取右子树
        /// </summary>
        public BinNode<T> RightChild
        {
            get { return _rightChild; }
            set { _rightChild = value; }
        }

        /// <summary>
        /// 获取节点值
        /// </summary>
        public T Element
        {
            get { return _element; }
            set { _element = value; }
        }

        /// <summary>
        /// 获取树高度
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// 获取红黑树节点颜色
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// 作为当前节点的左孩子插入新节点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public BinNode<T> InsertAsLc(T t)
        {
            BinNode<T> leftChild = new BinNode<T>(t);
            return InsertAsLc(leftChild);
        }

        /// <summary>
        /// 作为当前节点的左孩子插入新节点
        /// </summary>
        /// <param name="leftChild"></param>
        /// <returns></returns>
        public BinNode<T> InsertAsLc(BinNode<T> leftChild)
        {
            _leftChild = leftChild;
            if (null != leftChild)
            {
                leftChild.ParentNode = this;
            }
            return _leftChild;
        }

        /// <summary>
        /// 作为当前节点的右孩子插入新节点
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public BinNode<T> InsertAsRc(T t)
        {
            BinNode<T> rightChild = new BinNode<T>(t);
            return InsertAsRc(rightChild);
        }

        /// <summary>
        /// 作为当前节点的右孩子插入新节点
        /// </summary>
        /// <param name="rightChild"></param>
        /// <returns></returns>
        public BinNode<T> InsertAsRc(BinNode<T> rightChild)
        {
            _rightChild = rightChild;
            if(null != rightChild)
            {
                rightChild.ParentNode = this;
            }
            return _rightChild;
        }

        /// <summary>
        /// 是否根节点
        /// </summary>
        /// <returns></returns>
        public bool IsRoot()
        {
            return null == ParentNode;
        }

        public bool HasParent()
        {
            return !IsRoot();
        }

        public bool HasLChild()
        {
            return null != LeftChild;
        }

        public bool HasRChild()
        {
            return null != RightChild;
        }

        /// <summary>
        /// 是父节点的左孩子
        /// </summary>
        /// <returns></returns>
        public bool IsLChild()
        {
            return !IsRoot() && this == ParentNode.LeftChild;
        }

        /// <summary>
        /// 是父节点的右孩子
        /// </summary>
        /// <returns></returns>
        public bool IsRChild()
        {
            return !IsRoot() && this == ParentNode.RightChild;
        }

        /// <summary>
        /// 有左子树或者右子树
        /// </summary>
        /// <returns></returns>
        public bool HasChild()
        {
            return HasLChild() || HasRChild();
        }

        /// <summary>
        /// 有左子树并且有右子树
        /// </summary>
        /// <returns></returns>
        public bool HasBoothChild()
        {
            return HasLChild() && HasRChild();
        }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        /// <returns></returns>
        public bool IsLeaf()
        {
            return !HasChild();
        }

        /// <summary>
        /// 是红黑树黑节点
        /// </summary>
        /// <returns></returns>
        public bool IsBlack()
        {
            return Color == Color.Black;
        }

        /// <summary>
        /// 是红黑树红节点
        /// </summary>
        /// <returns></returns>
        public bool IsRed()
        {
            return Color == Color.Red;
        }

        /// <summary>
        /// 重写 == 方法
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        public static bool operator == (BinNode<T> node1, BinNode<T> node2)
        {
            if (object.Equals(node1, null) || object.Equals(node2, null))
            {
                return object.Equals(node1, node2);
            }
            return node1.Element.CompareTo(node2.Element) == 0;
        }

        /// <summary>
        /// 重写 != 方法
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        public static bool operator != (BinNode<T> node1, BinNode<T> node2)
        {
            if (object.Equals(node1, null) || object.Equals(node2, null))
            {
                return !object.Equals(node1, node2);
            }
            return node1.Element.CompareTo(node2.Element) != 0;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
