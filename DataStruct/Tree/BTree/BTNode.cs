using System;
using System.Collections.Generic;

namespace DataStruct.Tree.BTree
{
    // B-树节点
    class BTNode<T> where T : IComparable<T>
    {
        private BTNode<T> parentNode;// 父节点
        private List<T> key;       // 数值向量
        private List<BTNode<T>> child;  // 孩子向量（其长度总比key多一）

        public BTNode()
        {
            key = new List<T>();
            child = new List<BTNode<T>>();
            child.Add(null);
        }

        public BTNode(T t, BTNode<T> lc, BTNode<T> rc)
        {
            parentNode = null;
            key.Insert(0, t);
            // 左右孩子
            child.Insert(0, lc);
            child.Insert(1, rc);

            if (null != lc)
            {
                lc.parentNode = this;
            }
            if (null != rc)
            {
                rc.parentNode = this;
            }
        }

        public BTNode<T> ParentNode
        {
            get { return parentNode; }
            set { parentNode = value; }
        }
            
        public List<T> Key
        {
            get { return key; }
            set { key = value; }
        }

        public List<BTNode<T>> Child
        {
            get { return child; }
            set { child = value; }
        }
    }
}
