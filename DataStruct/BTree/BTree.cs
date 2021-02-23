using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.BTree
{

    public class BTNode<T> where T : IComparable<T>
    {
        private BTNode<T> _leftChild;
        private BTNode<T> _rightChild;

        private BTNode<T> _parentNode;

        private T _value;

        public BTNode(T value)
        {
            _value = value;
        }

        public BTNode(T value, BTNode<T> parent)
        {
            _value = value;
            ParentNode = parent;
        }

        public BTNode<T> LeftChild
        {
            get { return _leftChild; }
            set { _leftChild = value; }
        }

        public BTNode<T> RightChild
        {
            get { return _rightChild; }
            set { _rightChild = value; }
        }

        public BTNode<T> ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public abstract class BTree<T> where T : IComparable<T>
    {
        private BTNode<T> _root = null;

        public BTree()
        {
            _root = null;
        }

        public BTNode<T> Root
        {
            get { return _root; }
            protected set { _root = value; }
        }

        public bool Empty()
        {
            return null == Root;
        }

        public virtual BTNode<T> Insert(T t)
        {
            return null;
        }

        public virtual bool Remove(T t)
        {
            return false;
        }

        public abstract BTNode<T> Search(T t);

        public virtual void Release()
        {

        }

    }
}
