using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Stack
{

    public class StackNode<T>
    {
        public T _element;
        private StackNode<T> _nextNode;

        public StackNode() {   }

        public StackNode(T element)
        {
            _element = element;
        }

        public StackNode(T element, StackNode<T> nextNode)
        {
            _element = element;
            _nextNode = nextNode;
        }
        
        public T Element
        {
            get { return _element; }
            set { _element = value; }
        }

        public StackNode<T> NextNode
        {
            get { return _nextNode; }
            set { _nextNode = value; }
        }
    }


    /// <summary>
    /// 栈：LIFO(后进先出)
    /// 栈是限制插入和删除只能在一个位置上进行的表，该位置是表的末端。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class StackData<T>
    {
        private StackNode<T> _header;

        public bool IsEmpty()
        {
            return null == _header.NextNode;
        }

        public void MakeEmpty()
        {
            _header.NextNode = null;
        }

        public void Push(T element)
        {
            StackNode<T> temp = new StackNode<T>(element);
            temp.NextNode = _header.NextNode;
            _header.NextNode = temp;
        }

        public T Pop()
        {
            if (IsEmpty())
            {
                return default(T);
            }

            StackNode<T> temp = _header.NextNode;
            _header.NextNode = temp.NextNode;
            return temp.Element;
        }

        public T Peek()
        {
            if (IsEmpty())
            {
                return default(T);
            }
            return _header.NextNode.Element;
        }

    }
}
