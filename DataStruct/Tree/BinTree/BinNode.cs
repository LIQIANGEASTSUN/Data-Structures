﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.BinTree
{
    public enum Color
    {
        Red, 
        Black,
    }

    public class BinNode<T> where T : IComparable<T>
    {
        private T _element;
        private BinNode<T> _parentNode;
        private BinNode<T> _leftChild;
        private BinNode<T> _rightChild;
        private int _height;
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

        public T Element
        {
            get { return _element; }
            set { _element = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        // 作为当前节点的左孩子插入新节点
        public BinNode<T> InsertAsLc(T t)
        {
            BinNode<T> node = new BinNode<T>(t);
            return InsertAsLc(node);
        }

        public BinNode<T> InsertAsLc(BinNode<T> node)
        {
            _leftChild = node;
            if (null != node)
            {
                node.ParentNode = this;
            }
            return _leftChild;
        }

        // 作为当前节点的右孩子插入新节点
        public BinNode<T> InsertAsRc(T t)
        {
            BinNode<T> node = new BinNode<T>(t);
            return InsertAsRc(node);
        }

        public BinNode<T> InsertAsRc(BinNode<T> node)
        {
            _rightChild = node;
            if(null != node)
            {
                node.ParentNode = this;
            }
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

        public bool IsBlack()
        {
            return Color == Color.Black;
        }

        public bool IsRed()
        {
            return Color == Color.Red;
        }

        public static bool operator == (BinNode<T> node1, BinNode<T> node2)
        {
            if (object.Equals(node1, null) || object.Equals(node2, null))
            {
                return object.Equals(node1, node2);
            }
            return node1.Element.CompareTo(node2.Element) == 0;
        }

        public static bool operator != (BinNode<T> node1, BinNode<T> node2)
        {
            if (object.Equals(node1, null) || object.Equals(node2, null))
            {
                return !object.Equals(node1, node2);
            }
            return node1.Element.CompareTo(node2.Element) != 0;
        }
    }
}
