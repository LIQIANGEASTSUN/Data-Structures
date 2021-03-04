using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.List
{
    class LinkListIterator<T> where T : IComparable<T>
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

        public static bool operator ==(LinkListIterator<T> iteratorL, LinkListIterator<T> iteratorR)
        {
            return null != iteratorL.node && null != iteratorR.node && iteratorL.node == iteratorR.node;
        }

        public static bool operator !=(LinkListIterator<T> iteratorL, LinkListIterator<T> iteratorR)
        {
            return null == iteratorL.node || null == iteratorR.node || iteratorL.node != iteratorR.node;
        }

        public static LinkListIterator<T> operator ++(LinkListIterator<T> iterator)
        {
            iterator.node = iterator.node.NextNode;
            return iterator;
        }

    }
}
