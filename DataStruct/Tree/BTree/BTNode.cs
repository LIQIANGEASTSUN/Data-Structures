using System;
using System.Collections.Generic;

namespace DataStruct.Tree.BTree
{
    // B-树节点
    class BTNode<T> where T : IComparable<T>
    {
        private BTNode<T> parentNode;// 父节点
        private List<T> keyList;       // 数值向量
        private List<BTNode<T>> childList;  // 孩子向量（其长度总比key多一）

        public BTNode()
        {
            keyList = new List<T>();
            childList = new List<BTNode<T>>();
            childList.Insert(0, null);
        }

        public BTNode(T t, BTNode<T> lc, BTNode<T> rc)
        {
            parentNode = null;
            keyList.Insert(0, t);
            // 左右孩子
            childList.Insert(0, lc);
            childList.Insert(1, rc);

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
            
        public List<T> KeyList
        {
            get { return keyList; }
            set { keyList = value; }
        }

        public List<BTNode<T>> ChildList
        {
            get { return childList; }
            set { childList = value; }
        }

        public void AddChild(BTNode<T> node)
        {
            ChildList.Add(node);
            if (null != node)
            {
                node.parentNode = this;
            }
        }
    }
}
