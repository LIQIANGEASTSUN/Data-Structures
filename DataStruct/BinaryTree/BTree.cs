using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.BinaryTree
{
    public class BTNode<T>
    {
        private BTNode<T> _leftChild;
        private BTNode<T> _rightChild;

        private T _value;

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

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    class BTree<T>
    {
        private BTNode<T> _root;

        public BTree()
        {

        }

        public BTNode<T> Root
        {
            get { return _root; }
        }

        public bool Empty()
        {
            return null == Root;
        }

        public virtual bool Insert(T t)
        {
            return true;
        }

        public virtual bool Remove(T t)
        {
            return false;
        }

        public void Release()
        {

        }

    }
}
