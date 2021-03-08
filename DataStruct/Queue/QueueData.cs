using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Queue
{

    public class QueueTest
    {
        public static void Test()
        {
            QueueData<int> queue = new QueueData<int>();

            Console.WriteLine("Empty:" + queue.IsEmpty());

            queue.Enqueue(1);

            Console.WriteLine("peek:" + queue.Peek());

            queue.Enqueue(2);
            Console.WriteLine("peek:" + queue.Peek());

            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            Console.WriteLine("peek:" + queue.Peek());

            queue.Enqueue(6);
            Console.WriteLine("peek:" + queue.Peek());

            queue.Enqueue(7);

            Console.WriteLine("Empty:" + queue.IsEmpty());
            queue.Enqueue(8);
            queue.Enqueue(9);
            queue.Enqueue(10);
            queue.Enqueue(11);
            queue.Enqueue(12);
            queue.Enqueue(13);

            while (!queue.IsEmpty())
            {
                Console.WriteLine(queue.Dequeue());
            }

        }
    }


    public class QueueNode<T>
    {
        private T _element;
        private QueueNode<T> _nextNode;

        public QueueNode() {       }

        public QueueNode(T element)
        {
            _element = element;
        }

        public T Element
        {
            get { return _element; }
        }

        public QueueNode<T> NextNode
        {
            get { return _nextNode; }
            set { _nextNode = value; }
        }
    }

    class QueueData<T>
    {
        private QueueNode<T> _header;
        private QueueNode<T> _trailer;

        public QueueData()
        {
            _header = new QueueNode<T>();
            _trailer = _header;
        }

        public bool IsEmpty()
        {
            return _header == _trailer;
        }

        public void MakeEmpty()
        {
            _header.NextNode = null;
            _trailer = _header;
        }

        public void Enqueue(T element)
        {
            QueueNode<T> temp = new QueueNode<T>(element);
            _trailer.NextNode = temp;
            _trailer = temp;
        }

        public T Dequeue()
        {
            if (IsEmpty())
            {
                return default(T);
            }

            QueueNode<T> temp = _header.NextNode;
            _header = temp;

            return temp.Element;
        }

        public T Peek()
        {
            if (IsEmpty())
            {
                return default(T);
            }

            QueueNode<T> temp = _header.NextNode;
            return temp.Element;
        }

    }
}
