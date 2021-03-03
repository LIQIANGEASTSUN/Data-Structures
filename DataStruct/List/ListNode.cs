using System;

namespace DataStruct.List
{
    class ListNode<T> where T : IComparable<T>
    {
        private T data;
        private ListNode<T> preNode;
        private ListNode<T> nextNode;

        public ListNode()
        {

        }

        public ListNode(T v)
        {
            data = v;
        }

        public ListNode(T v, ListNode<T> pre, ListNode<T> next)
        {
            data = v;
            preNode = pre;
            nextNode = next;
        }

        public T Data
        {
            get { return data; }
            set { data = value; }
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

}
