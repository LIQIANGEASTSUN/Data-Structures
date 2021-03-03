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
            DataList<int> list = new DataList<int>(int.MinValue, int.MaxValue);

            for (int i = 0; i < 6; ++i)
            {
                list.InsertAsLast(i);
            }


            Swap(list, 5, 2);
            Swap(list, 0, 4);
            list.Traverse();

            list.InsertSort();
            list.Traverse();
        }

        private static void Swap(DataList<int> dataList, int value0, int value1)
        {
            ListNode<int> node0 = dataList.Find(value0);
            ListNode<int> node1 = dataList.Find(value1);

            dataList.Swap(node0, node1);
            //dataList.Traverse();
        }

    }

    class DataList<T> where T : IComparable<T>
    {
        private ListNode<T> header;   // 头
        private ListNode<T> trailer;  // 尾

        public DataList(T head, T trai)
        {
            header = new ListNode<T>(head);
            trailer = new ListNode<T>(trai);

            header.PreNode = null;
            header.NextNode = trailer;

            trailer.PreNode = header;
            trailer.NextNode = null;
        }

        public ListNode<T> First()
        {
            return header.NextNode != trailer ? header.NextNode : null;
        }

        public ListNode<T> Last()
        {
            return trailer.PreNode != header ? trailer.PreNode : null;
        }

        public void MakeEmpty()
        {
            header.NextNode = trailer;
            trailer.PreNode = header;
        }

        public bool IsEmpty()
        {
            return header.NextNode == trailer;
        }

        public ListNode<T> Find(T t)
        {
            return FindFrom(header.NextNode, t);
        }

        public ListNode<T> FindFrom(ListNode<T> node, T t)
        {
            while (node != trailer)
            {
                if (node.Data.CompareTo(t) == 0)
                {
                    return node;
                }
                node = node.NextNode;
            }
            return null;
        }

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
            }
        }

        public void InsertAsFirst(T t)
        {
            InsertAsNext(header, t);
        }

        public void InsertAsLast(T t)
        {
            InsertAsPre(trailer, t);
        }

        public void InsertAsNext(ListNode<T> node, T t)
        {
            ListNode<T> newNode = new ListNode<T>(t);
            InsertAsNext(node, newNode);
        }

        public void InsertAsNext(ListNode<T> node, ListNode<T> newNode)
        {
            newNode.PreNode = node;
            newNode.NextNode = node.NextNode;

            node.NextNode.PreNode = newNode;
            node.NextNode = newNode;
        }

        public void InsertAsPre(ListNode<T> node, T t)
        {
            ListNode<T> newNode = new ListNode<T>(t);
            InsertAsPre(node, newNode);
        }

        public void InsertAsPre(ListNode<T> node, ListNode<T> newNode)
        {
            newNode.PreNode = node.PreNode;
            newNode.NextNode = node;

            node.PreNode.NextNode = newNode;
            node.PreNode = newNode;
        }

        /// <summary>
        /// 无序列表去重
        /// </summary>
        public void Deduplicate()
        {
            for (ListNode<T> node = First(); node != trailer; node = node.NextNode)
            {
                for (ListNode<T> temp = node.NextNode; temp != trailer; temp = temp.NextNode)
                {
                    if (node.Data.CompareTo(temp.Data) == 0)
                    {
                        Delete(temp);
                    }
                }
            }
        }

        /// <summary>
        /// 有序列表去重
        /// </summary>
        public void Uniquify()
        {
            ListNode<T> node = First();
            while ( node != trailer)
            {
                ListNode<T> next = node.NextNode;
                if (next != trailer && next.Data.CompareTo(node.Data) == 0)
                {
                    Console.WriteLine("Del:" + next.Data.ToString());
                    Delete(next);
                    continue;
                }
                node = node.NextNode;
            }
        }

        public void Traverse()
        {
            StringBuilder sb = new StringBuilder();
            for (ListNode<T> node = header.NextNode; node != trailer; node = node.NextNode)
            {
                sb.Append(node.Data.ToString() + "  ");
            }
            Console.WriteLine(sb.ToString());
        }

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
        public void InsertSort()
        {
            ListNode<T> first = First();
            if (null == first || first.NextNode == trailer)
            {
                return;
            }

            for (ListNode<T> temp = first.NextNode; temp != trailer; temp = temp.NextNode)
            {
                T data = temp.Data;
                ListNode<T> j = temp.PreNode;
                while (j != header && j.Data.CompareTo(data) > 0)
                {
                    j.NextNode.Data = j.Data;
                    j = j.PreNode;
                }

                j.NextNode.Data = data;
            }
        }

    }
}
