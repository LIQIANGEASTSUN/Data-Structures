using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Log
{
    // 打印结构形状时使用的 Node
    public abstract class LogNode<T>
    {
        public T t;

        public abstract int ParentIndex();
        public abstract int LeftChildIndex();
        public abstract int RightChildIndex();

        public abstract string ToString();
    }
}
