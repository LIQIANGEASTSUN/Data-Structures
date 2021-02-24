using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.BinTree
{
    public class BinNode<T> where T : IComparable<T>
    {
        private T _value;
        private BinNode<T> _parentNode;
        private BinNode<T> _leftChild;
        private BinNode<T> _rightChild;
        private int _height;

        public BinNode(T value)
        {
            _value = value;
        }

        public BinNode(T value, BinNode<T> parent)
        {
            _value = value;
            ParentNode = parent;
        }

        public BinNode<T> ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }

        public BinNode<T> LeftChild
        {
            get { return _leftChild; }
            set { _leftChild = value; }
        }

        public BinNode<T> RightChild
        {
            get { return _rightChild; }
            set { _rightChild = value; }
        }

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        // 作为当前节点的左孩子插入新节点
        public BinNode<T> InsertAsLc(T t)
        {
            _leftChild = new BinNode<T>(t);
            _leftChild.ParentNode = this;
            return _leftChild;
        }

        // 作为当前节点的右孩子插入新节点
        public BinNode<T> InsertAsRc(T t)
        {
            _rightChild = new BinNode<T>(t);
            _rightChild.ParentNode = this;
            return _rightChild;
        }

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

        public bool IsLChild()
        {
            return !IsRoot() && this == ParentNode.LeftChild;
        }

        public bool IsRChild()
        {
            return !IsRoot() && this == ParentNode.RightChild;
        }

        public bool HasChild()
        {
            return HasLChild() || HasRChild();
        }

        public bool HasBoothChild()
        {
            return HasLChild() && HasRChild();
        }

        public bool IsLeaf()
        {
            return !HasChild();
        }

        public static bool operator == (BinNode<T> node1, BinNode<T> node2)
        {
            if (object.Equals(node1, null) || object.Equals(node2, null))
            {
                return object.Equals(node1, node2);
            }
            return node1.Value.CompareTo(node2.Value) == 0;
        }

        public static bool operator != (BinNode<T> node1, BinNode<T> node2)
        {
            if (object.Equals(node1, null) || object.Equals(node2, null))
            {
                return !object.Equals(node1, node2);
            }
            return node1.Value.CompareTo(node2.Value) != 0;
        }
    }
}
