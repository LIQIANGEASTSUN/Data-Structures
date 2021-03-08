using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.List
{
    public class DataListTest
    {
        public static void Test()
        {
            LinkList<int> list = new LinkList<int>();

            bool empty = list.IsEmpty();

            {
                int front = list.Front();
                int back = list.Back();
                Console.WriteLine("front:" + front + "    back:" + back);
            }

            {
                list.PushBack(30000);
                int front = list.Front();
                int back = list.Back();
                Console.WriteLine("front:" + front + "    back:" + back);
            }

            for (int i = 0; i < 6; ++i)
            {
                list.PushBack(i);
                Console.WriteLine("Size:" + list.Size());
            }
            empty = list.IsEmpty();
            list.PushFront(10);
            list.Traverse();

            {
                int front = list.Front();
                int back = list.Back();
                Console.WriteLine("front:" + front + "    back:" + back);
            }


            Swap(list, 5, 2);

            Swap(list, 0, 4);

            Swap(list, 5, 3);
            Swap(list, 0, 2);

            list.Delete(3, Compare);
            list.Traverse();
            Console.WriteLine("Size:" + list.Size());
            Console.WriteLine();

            list.Delete(0, Compare);
            list.Traverse();
            Console.WriteLine("Size:" + list.Size());
            Console.WriteLine();

            list.Delete(4, Compare);
            list.Traverse();
            Console.WriteLine("Size:" + list.Size());
            Console.WriteLine();

            list.InsertSort(Compare);
            list.Traverse();

            LinkListIterator<int> iterator = list.Begin();
            while (iterator != list.End())
            {
                Console.WriteLine(iterator.Element);
                iterator++;
            }
        }

        private static void Swap(LinkList<int> dataList, int value0, int value1)
        {
            ListNode<int> node0 = dataList.Find(value0, Compare);
            ListNode<int> node1 = dataList.Find(value1, Compare);

            dataList.Swap(node0, node1);
            dataList.Traverse();
        }

        private static int Compare(int x, int y)
        {
            return x - y;
        }
    }

    /// <summary>
    /// 链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class LinkList<T>
    {
        private ListNode<T> _header;   // 头
        private ListNode<T> _trailer;  // 尾
        private int _size;

        public LinkList()
        {
            _header = new ListNode<T>();
            _trailer = new ListNode<T>();

            _header.PreNode = null;
            _header.NextNode = _trailer;

            _trailer.PreNode = _header;
            _trailer.NextNode = null;
        }

        // 返回头部指针的下一个
        public LinkListIterator<T> Begin()
        {
            return new LinkListIterator<T>(_header.NextNode);
        }

        // 返回末尾指针
        public LinkListIterator<T> End()
        {
            return new LinkListIterator<T>(_trailer);
        }

        /// <summary>
        /// 第一个元素
        /// </summary>
        public T Front()
        {
            return Size() > 0 ? _header.NextNode.Element : default(T);
        }

        /// <summary>
        /// 最后一个元素
        /// </summary>
        public T Back()
        {
            return Size() > 0 ? _trailer.PreNode.Element : default(T);
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
            List<int> list;
            return _size;
        }

        /// <summary>
        /// 查找元素指针位置
        /// </summary>
        public ListNode<T> Find(T t, Comparison<T> comparison)
        {
            ListNode<T> temp = _header.NextNode;
            while (temp != _trailer)
            {
                if (comparison(temp.Element, t) == 0)
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
        public void Delete(T t, Comparison<T> comparison)
        {
            ListNode<T> node = Find(t, comparison);
            Delete(node);
        }

        private void Delete(ListNode<T> node)
        {
            if (null != node)
            {
                node.PreNode.NextNode = node.NextNode;
                node.NextNode.PreNode = node.PreNode;
                --_size;
            }
        }

        /// <summary>
        /// 将元素插入到链表头
        /// </summary>
        /// <param name="t"></param>
        public void PushFront(T t)
        {
            ListNode<T> newNode = new ListNode<T>(t);
            InsertAsPre(_header.NextNode, newNode);
        }

        /// <summary>
        /// 将元素插入链表尾
        /// </summary>
        /// <param name="t"></param>
        public void PushBack(T t)
        {
            ListNode<T> newNode = new ListNode<T>(t);
            InsertAsPre(_trailer, newNode);
        }

        private void InsertAsPre(ListNode<T> node, ListNode<T> newNode)
        {
            InsertAsNext(node.PreNode, newNode);
        }

        public void InsertAsNext(ListNode<T> node, ListNode<T> newNode)
        {
            if (node == _trailer)
            {
                return;
            }
            newNode.PreNode = node;
            newNode.NextNode = node.NextNode;

            node.NextNode.PreNode = newNode;
            node.NextNode = newNode;

            ++_size;
        }

        /// <summary>
        /// 无序链表去重
        /// </summary>
        public void Deduplicate(Comparison<T> comparison)
        {
            for (ListNode<T> node = _header.NextNode; node != _trailer; node = node.NextNode)
            {
                for (ListNode<T> temp = node.NextNode; temp != _trailer; temp = temp.NextNode)
                {
                    if (comparison(node.Element, temp.Element) == 0)
                    {
                        Delete(temp);
                    }
                }
            }
        }

        /// <summary>
        /// 有序链表去重
        /// </summary>
        public void Uniquify(Comparison<T> comparison)
        {
            ListNode<T> node = _header.NextNode;
            while ( node != _trailer)
            {
                ListNode<T> next = node.NextNode;
                if (next != _trailer && comparison(next.Element, node.Element) == 0)
                {
                    Console.WriteLine("Del:" + next.Element.ToString());
                    Delete(next);
                    continue;
                }
                node = node.NextNode;
            }
        }

        /// <summary>
        /// 遍历
        /// </summary>
        public void Traverse()
        {
            StringBuilder sb = new StringBuilder();
            for (LinkListIterator<T> iterator = Begin(); iterator != End(); ++iterator)
            {
                sb.Append(iterator.Element.ToString() + "  ");
            }
            Console.WriteLine(sb.ToString());
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

        private void Swap(ListNode<T> pre, ListNode<T> next, ListNode<T> node1, ListNode<T> node2)
        {
            Delete(node1);
            Delete(node2);

            InsertAsNext(pre, node2);
            InsertAsPre(next, node1);
        }

        /// <summary>
        /// 插入排序
        /// </summary>
        public void InsertSort(Comparison<T> comparison)
        {
            ListNode<T> begin = _header.NextNode;
            if (null == begin || begin.NextNode == _trailer)
            {
                return;
            }

            for (ListNode<T> temp = begin.NextNode; temp != _trailer; temp = temp.NextNode)
            {
                T data = temp.Element;
                ListNode<T> j = temp.PreNode;
                while (j != _header.NextNode.PreNode && comparison(j.Element, data) > 0)
                {
                    j.NextNode.Element = j.Element;
                    j = j.PreNode;
                }

                j.NextNode.Element = data;
            }
        }

    }
}
