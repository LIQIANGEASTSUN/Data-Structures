using System;

namespace DataStruct.List
{
    class ListNode<T>
    {
        private T element;
        private ListNode<T> preNode;
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

}
