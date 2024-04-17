using System;
using System.Collections.Generic;

namespace DataStruct.List
{
    public class DataListTest
    {
        public static void Test()
        {
            LinkList<int> list = new LinkList<int>();

            bool empty = list.IsEmpty();
            Console.WriteLine("is empty:" + empty);

            for (int i = 0; i < 10; ++i)
            {
                list.PushBack(i);
                Console.WriteLine("pushBack:" + i + "   Size:" + list.Size());
            }
            ListNode<int> front = list.Front();
            ListNode<int> back = list.Back();
            Console.WriteLine("front:" + front.Element + "    back:" + back.Element);

            empty = list.IsEmpty();
            Console.WriteLine("is empty:" + empty);

            Console.WriteLine("PushFront:10");
            list.PushFront(10);
            front = list.Front();
            back = list.Back();
            Console.WriteLine("front:" + front.Element + "    back:" + back.Element);

            List<int> nodeList = list.Traverse();
            string str = string.Empty;
            foreach(var element in nodeList)
            {
                str += element + "   ";
            }
            Console.WriteLine("Traverse:" + str);

            LinkListIterator<int> iterator = list.Begin();
            str = string.Empty;
            while (iterator != list.End())
            {
                str += iterator.Element + "   ";
                iterator++;
            }
            Console.WriteLine("iterator:" + str);

            Swap(list, 5, 2);

            iterator = list.Begin();
            str = string.Empty;
            while (iterator != list.End())
            {
                str += iterator.Element + "   ";
                iterator++;
            }
            Console.WriteLine("iterator:" + str);

            Console.WriteLine("Delete 3");
            list.Delete(3);
            Console.WriteLine("Delete 9");
            list.Delete(9);
            Console.WriteLine("Sort");
            list.Sort((a, b) => a.CompareTo(b));

            iterator = list.Begin();
            str = string.Empty;
            while (iterator != list.End())
            {
                str += iterator.Element + "   ";
                iterator++;
            }
            Console.WriteLine("iterator:" + str);
        }

        private static void Swap(LinkList<int> dataList, int value0, int value1)
        {
            ListNode<int> node0 = dataList.Find(value0);
            ListNode<int> node1 = dataList.Find(value1);

            dataList.Swap(node0, node1);
            dataList.Traverse();
        }
    }

    /// <summary>
    /// 链表节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListNode<T> where T : IComparable<T>
    {
        // 节点的值
        private T element;
        // 前置节点
        private ListNode<T> preNode;
        // 后置节点
        private ListNode<T> nextNode;

        public ListNode()
        {

        }

        public ListNode(T v)
        {
            element = v;
        }

        public ListNode(T v, ListNode<T> pre, ListNode<T> next)
        {
            element = v;
            preNode = pre;
            nextNode = next;
        }

        public T Element
        {
            get { return element; }
            set { element = value; }
        }

        public ListNode<T> PreNode
        {
            get { return preNode; }
            set { preNode = value; }
        }

        public ListNode<T> NextNode
        {
            get { return nextNode; }
            set { nextNode = value; }
        }
    }

    /// <summary>
    /// 链表迭代器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkListIterator<T> where T : IComparable<T>
    {
        private ListNode<T> node;

        public LinkListIterator(ListNode<T> node)
        {
            this.node = node;
        }

        public ListNode<T> Node
        {
            get { return node; }
        }

        public T Element
        {
            get
            {
                return node.Element;
            }
        }

        /// <summary>
        /// 重写 == 方法
        /// </summary>
        /// <param name="iteratorL"></param>
        /// <param name="iteratorR"></param>
        /// <returns></returns>
        public static bool operator ==(LinkListIterator<T> iteratorL, LinkListIterator<T> iteratorR)
        {
            return null != iteratorL.node && null != iteratorR.node && iteratorL.node == iteratorR.node;
        }

        /// <summary>
        /// 重写 != 方法
        /// </summary>
        /// <param name="iteratorL"></param>
        /// <param name="iteratorR"></param>
        /// <returns></returns>
        public static bool operator !=(LinkListIterator<T> iteratorL, LinkListIterator<T> iteratorR)
        {
            return null == iteratorL.node || null == iteratorR.node || iteratorL.node != iteratorR.node;
        }

        /// <summary>
        /// 重写 ++ 方法
        /// </summary>
        /// <param name="iterator"></param>
        /// <returns></returns>
        public static LinkListIterator<T> operator ++(LinkListIterator<T> iterator)
        {
            iterator.node = iterator.node.NextNode;
            return iterator;
        }

        /// <summary>
        /// 重写 -- 方法
        /// </summary>
        /// <param name="iterator"></param>
        /// <returns></returns>
        public static LinkListIterator<T> operator --(LinkListIterator<T> iterator)
        {
            iterator.node = iterator.node.PreNode;
            return iterator;
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

    /// <summary>
    /// 链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkList<T> where T : IComparable<T>
    {
        /// <summary>
        /// 头节点
        /// </summary>
        private ListNode<T> _header;
        /// <summary>
        /// 尾节点
        /// </summary>
        private ListNode<T> _trailer;  
        // 除去 头、尾，节点的个数
        private int _size;

        public LinkList()
        {
            // 为了减少操作上的复杂度
            // 头节点和尾节点一直存在，添加、删除都是在 头节点、尾节点 中间操作
            _header = new ListNode<T>();
            _trailer = new ListNode<T>();

            _header.PreNode = null;
            _header.NextNode = _trailer;

            _trailer.PreNode = _header;
            _trailer.NextNode = null;
        }

        /// <summary>
        /// 迭代器开始
        /// </summary>
        /// <returns></returns>
        public LinkListIterator<T> Begin()
        {
            return new LinkListIterator<T>(_header.NextNode);
        }

        /// <summary>
        /// 迭代器结束
        /// </summary>
        /// <returns></returns>
        public LinkListIterator<T> End()
        {
            return new LinkListIterator<T>(_trailer);
        }

        /// <summary>
        /// 第一个元素
        /// </summary>
        public ListNode<T> Front()
        {
            return Size() > 0 ? _header.NextNode : null;
        }

        /// <summary>
        /// 最后一个元素
        /// </summary>
        public ListNode<T> Back()
        {
            return Size() > 0 ? _trailer.PreNode : null;
        }

        /// <summary>
        /// 清空链表
        /// </summary>
        public void MakeEmpty()
        {
            _header.NextNode = _trailer;
            _trailer.PreNode = _header;

            _size = 0;
        }

        /// <summary>
        /// 链表为空
        /// </summary>
        public bool IsEmpty()
        {
            return _header.NextNode == _trailer;
        }

        /// <summary>
        /// 链表元素个数
        /// </summary>
        public int Size()
        {
            return _size;
        }

        /// <summary>
        /// 查找元素指针位置
        /// </summary>
        public ListNode<T> Find(T t)
        {
            ListNode<T> temp = _header.NextNode;
            while (temp != _trailer)
            {
                if (temp.Element.CompareTo(t) == 0)
                {
                    return temp;
                }
                temp = temp.NextNode;
            }
            return null;
        }

        /// <summary>
        /// 删除元素
        /// </summary>
        public void Delete(T t)
        {
            ListNode<T> node = Find(t);
            Delete(node);
        }

        public void Delete(ListNode<T> node)
        {
            if (null != node)
            {
                node.PreNode.NextNode = node.NextNode;
                node.NextNode.PreNode = node.PreNode;
                --_size;
            }
        }

        /// <summary>
        /// 将元素插入到链表第一个位置
        /// </summary>
        /// <param name="t"></param>
        public void PushFront(T t)
        {
            ListNode<T> newNode = new ListNode<T>(t);
            InsertAsPre(_header.NextNode, newNode);
        }

        /// <summary>
        /// 将元素插入到链表最后一个位置
        /// </summary>
        /// <param name="t"></param>
        public void PushBack(T t)
        {
            ListNode<T> newNode = new ListNode<T>(t);
            InsertAsPre(_trailer, newNode);
        }

        private void InsertAsPre(ListNode<T> node, ListNode<T> next)
        {
            InsertAsNext(node.PreNode, next);
        }

        /// <summary>
        /// 将 next 插入到 node 后
        /// </summary>
        /// <param name="node"></param>
        /// <param name="next"></param>
        public void InsertAsNext(ListNode<T> node, ListNode<T> next)
        {
            if (node == _trailer)
            {
                return;
            }
            next.PreNode = node;
            next.NextNode = node.NextNode;

            node.NextNode.PreNode = next;
            node.NextNode = next;

            ++_size;
        }

        /// <summary>
        /// 无序链表删除重复元素
        /// </summary>
        public void Deduplicate()
        {
            for (ListNode<T> node = _header.NextNode; node != _trailer; node = node.NextNode)
            {
                for (ListNode<T> temp = node.NextNode; temp != _trailer; temp = temp.NextNode)
                {
                    if (node.Element.CompareTo(temp.Element) == 0)
                    {
                        Delete(temp);
                    }
                }
            }
        }

        /// <summary>
        /// 有序链表删除重复元素
        /// </summary>
        public void Uniquify()
        {
            ListNode<T> node = _header.NextNode;
            while ( node != _trailer)
            {
                ListNode<T> next = node.NextNode;
                if (next != _trailer && next.Element.CompareTo(node.Element) == 0)
                {
                    Delete(next);
                    continue;
                }
                node = node.NextNode;
            }
        }

        /// <summary>
        /// 遍历
        /// </summary>
        public List<T> Traverse()
        {
            List<T> list = new List<T>();
            for (LinkListIterator<T> iterator = Begin(); iterator != End(); ++iterator)
            {
                list.Add(iterator.Element);
            }
            return list;
        }

        /// <summary>
        /// 交换两个节点位置
        /// </summary>
        public void Swap(ListNode<T> node1, ListNode<T> node2)
        {
            if (node1.NextNode == node2)
            {
                Swap(node1.PreNode, node2.NextNode, node1, node2);
            }
            else if (node2.NextNode == node1)
            {
                Swap(node2.PreNode, node1.NextNode, node2, node1);
            }
            else
            {
                Swap(node1.PreNode, node2.NextNode, node1, node2);
            }
        }

        /// <summary>
        /// 交换两个节点位置
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="next"></param>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        private void Swap(ListNode<T> pre, ListNode<T> next, ListNode<T> node1, ListNode<T> node2)
        {
            Delete(node1);
            Delete(node2);

            InsertAsNext(pre, node2);
            InsertAsPre(next, node1);
        }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort(Comparison<T> comparison)
        {
            ListNode<T> begin = _header.NextNode;
            if (null == begin || begin.NextNode == _trailer)
            {
                return;
            }

            for (ListNode<T> temp = begin.NextNode; temp != _trailer; temp = temp.NextNode)
            {
                T element = temp.Element;
                ListNode<T> preNode = temp.PreNode;
                while (preNode != _header.NextNode.PreNode && comparison(preNode.Element, element) > 0)
                {
                    preNode.NextNode.Element = preNode.Element;
                    preNode = preNode.PreNode;
                }

                preNode.NextNode.Element = element;
            }
        }
    }
}
