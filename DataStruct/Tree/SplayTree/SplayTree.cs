using DataStruct.BinTree;
using DataStruct.BSTree;
using System;
using System.Collections.Generic;

namespace DataStruct.Tree.SplayTree
{
    /// <summary>
    /// 伸展树
    /// </summary>
    class SplayTree<T> : BSTree<T> where T : IComparable<T>
    {

        /// <summary>
        /// 查找：伸展树的查找也会引起整树的结构调整，固需重写
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override BinNode<T> Search(T t)
        {
            if (null == Root || t.CompareTo(Root.Value) == 0)
            {
                _hot = null;
                return Root;
            }

            _hot = Root;
            while (null != _hot)
            {
                BinNode<T> c = _hot.Value.CompareTo(t) > 0 ? _hot.LeftChild : _hot.RightChild;
                if (null == c || c.Value.CompareTo(t) == 0)
                {
                    return c;
                }
                _hot = c;
            }

            return _hot;
        }

        /// <summary>
        /// 插入
        /// </summary>
        public override BinNode<T> Insert(T t)
        {
            return base.Insert(t);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public override bool Remove(T t)
        {
            return base.Remove(t);
        }

    }


}
