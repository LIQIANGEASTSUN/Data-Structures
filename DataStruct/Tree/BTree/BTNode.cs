using System;
using System.Collections.Generic;

namespace DataStruct.Tree.BTree
{
    /// <summary>
    /// B-树节点定义
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BTNode<T> where T : IComparable<T>
    {
        private BTNode<T> parentNode;       // 父节点
        private List<T> keyList;            // 关键字向量
        private List<BTNode<T>> childList;  // 子节点向量（其长度总比key多一）

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
            
        private List<T> KeyList
        {
            get { return keyList; }
            set { keyList = value; }
        }

        public int KeyCount
        {
            get { return KeyList.Count; }
        }

        public void InsertKey(int index, T key)
        {
            KeyList.Insert(index, key);
        }

        public T GetKey(int index)
        {
            return KeyList[index];
        }

        public void RemoveKeyAt(int index)
        {
            KeyList.RemoveAt(index);
        }

        public void SetKey(int index, T key)
        {
            KeyList[index] = key;
        }

        private List<BTNode<T>> ChildList
        {
            get { return childList; }
            set { childList = value; }
        }

        public void InsertChild(int index, BTNode<T> node)
        {
            ChildList.Insert(index, node);
            if (null != node)
            {
                node.parentNode = this;
            }
        }

        public BTNode<T> GetChild(int index)
        {
            return ChildList[index];
        }

        public void AddChild(BTNode<T> node)
        {
            InsertChild(ChildList.Count, node);
        }

        public BTNode<T> RemoveChildAt(int index)
        {
            BTNode<T> node = ChildList[index];
            ChildList.RemoveAt(index);
            return node;
        }

        public void SetChild(int index, BTNode<T> node)
        {
            ChildList[index] = node;
        }

        public int ChildCount
        {
            get { return ChildList.Count; }
        }
    }
}
