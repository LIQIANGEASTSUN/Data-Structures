using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Tree.BTree
{
    public class BTreeTest
    {
        public static void Test()
        {
            BTree<int> bTree = new BTree<int>(4);

            BTNode<int> root = new BTNode<int>();
            root.Key = new List<int>() { 53, 75};

            bTree.Root = root;

            #region Level1
            BTNode<int> node1 = new BTNode<int>();
            node1.Key = new List<int>() { 19, 36};

            BTNode<int> node2 = new BTNode<int>();
            node2.Key = new List<int>() { 63, 69};

            BTNode<int> node3 = new BTNode<int>();
            node3.Key = new List<int>() { 84, 92};

            root.AddChild(node1);
            root.AddChild(node2);
            root.AddChild(node3);
            #endregion

            #region Level2
            BTNode<int> node4 = new BTNode<int>();
            node4.Key = new List<int>() { 13, 17 };

            BTNode<int> node5 = new BTNode<int>();
            node5.Key = new List<int>() { 27, 31 };

            BTNode<int> node6 = new BTNode<int>();
            node6.Key = new List<int>() { 38, 41, 49, 51 };

            node1.AddChild(node4);
            node1.AddChild(node5);
            node1.AddChild(node6);


            BTNode<int> node7 = new BTNode<int>();
            node7.Key = new List<int>() { 57, 59 };

            BTNode<int> node8 = new BTNode<int>();
            node8.Key = new List<int>() { 65, 66 };

            BTNode<int> node9 = new BTNode<int>();
            node9.Key = new List<int>() { 71, 73 };

            node2.AddChild(node7);
            node2.AddChild(node8);
            node2.AddChild(node9);



            BTNode<int> node10 = new BTNode<int>();
            node10.Key = new List<int>() { 77, 79 };

            BTNode<int> node11 = new BTNode<int>();
            node11.Key = new List<int>() { 89, 91 };

            BTNode<int> node12 = new BTNode<int>();
            node12.Key = new List<int>() { 93, 97, 99 };

            node3.AddChild(node10);
            node3.AddChild(node11);
            node3.AddChild(node12);

            #endregion

            bTree.TraverseLevel(bTree.Root);

            Console.WriteLine();
        }

    }

    // B-树
    class BTree<T> where T : IComparable<T>
    {
        private int _order;         // 介次
        protected BTNode<T> _root;  //跟节点
        protected BTNode<T> _hot;   // search() 最后访问的非空节点位置

        public BTree(int order)
        {
            _order = order;
        }

        public BTNode<T> Root
        {
            get { return _root; }
            set { _root = value; }
        }

        /// <summary>
        /// 查找
        /// </summary>
        public BTNode<T> Search(T t)
        {
            BTNode<T> v = Root; // 从根节点触发
            _hot = null;

            while (null != v)
            {
                int index = -1;
                for (int i = 0; i < v.Key.Count; ++i)
                {
                    if (v.Key[i].CompareTo(t) == 0)
                    {
                        index = i;
                        break;
                    }
                }

                // 若成功，则返回
                if (index >= 0)
                {
                    return v;
                }

                _hot = v;
                // 沿引用转至对应的下层子树，并载入其根
                v = v.Child[index + 1];
            }
            // 若因 null == v 而退出,则意味着抵达外部节点
            return null; // 失败
        }

        /// <summary>
        /// 插入
        /// </summary>
        public bool Insert(T t)
        {
            BTNode<T> node = Search(t);
            if (null != node)
            {
                return false;
            }

            int index = -1;
            for (int i = 0; i < _hot.Key.Count; ++i)
            {
                T value = _hot.Key[i];
                int compare = value.CompareTo(t);
                if (compare > 0)
                {
                    break;
                }
                ++index;
            }

            _hot.Key.Insert(index + 1, t);      // 将新关键码插至对应的位置
            _hot.Child.Insert(index + 2, null); // 创建一个空子树指针

            SolveOverflow(_hot); // 如发生上溢，需做分裂

            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Remove(T t)
        {
            BTNode<T> node = Search(t);
            if (null == node)
            {
                return false;
            }

            int index = -1;
            for (int i = 0; i < node.Key.Count; ++i)
            {
                if(node.Key[i].CompareTo(t) == 0)
                {
                    index = i;
                    break;
                }
            }

            // node 不是叶子节点
            if (null != node.Child[0])
            {
                BTNode<T> u = node.Child[index + 1]; // 在右子树中一直向左，即可
                while (null != u.Child[0])
                {
                    u = u.Child[0]; // 找到 t 的后继（必需于某叶节点）
                }
                // 至此，node 必然位于最底层，且其中第 r 个关键码就是待删除者

                node.Key[index] = u.Key[0];
                node = u;  // 并与之交换位置
                index = 0;
            }

            node.Key.RemoveAt(index);
            node.Child.RemoveAt(index + 1);

            SolveUnderflow(node); // 如有必要，需做旋转或合并

            return false;
        }

        /// <summary>
        /// 上溢:因插入而上溢后的分裂处理
        /// </summary>
        public void SolveOverflow(BTNode<T> v)
        {
            if (_order >= v.Child.Count)
            {
                return; //递归基：当前节点并未上溢
            }

            int s = _order / 2; //轴点（此时应有_order = key.Count = child.Count - 1）
            BTNode<T> u = new BTNode<T>(); //注意：新节点已有一个空孩子
            for (int j = 0; j < _order - s - 1; j++)
            { //v右侧_order-s-1个孩子及关键码分裂为右侧节点u
                BTNode<T> node = v.Child[s + 1];
                v.Child.RemoveAt(s + 1);
                u.Child.Insert(j, node); //逐个移动效率低

                T key = v.Key[s + 1];
                v.Key.RemoveAt(s + 1);
                u.Key.Insert(j, key); //此策略可改进
            }

            BTNode<T> node2 = v.Child[s + 1];
            v.Child.RemoveAt(s + 1);
            u.Child[_order - s - 1] = node2; //移动v最靠右的孩子
            if (null != u.Child[0]) //若u的孩子们非空，则
            {
                for (int j = 0; j < _order - s; j++) //令它们的父节点统一
                {
                    u.Child[j].ParentNode = u; //指向u
                }
            }
                
            BTNode<T> p = v.ParentNode; //v当前的父节点p
            if (null == p)
            {
                _root = p = new BTNode<T>();
                p.Child[0] = v;
                v.ParentNode = p;
            } //若p空则创建之

            int index = -1;
            for (int i = 0; i < p.Key.Count; ++i)
            {
                if (p.Key[i].CompareTo(v.Key[0]) == 0)
                {
                    index = i;
                    break;
                }
            }
            int r = 1 + index; //p中指向u的指针的秩

            T key2 = v.Key[s];
            v.Key.RemoveAt(s);
            p.Key.Insert(r, key2); //轴点关键码上升
            p.Child.Insert(r + 1, u); u.ParentNode = p; //新节点u与父节点p互联
            SolveOverflow(p); //上升一层，如有必要则继续分裂——至多递归O(logn)层
        }

        /// <summary>
        /// 下溢:因删除而下溢后的合并处理
        /// </summary>
        /// <param name="node"></param>
        public void SolveUnderflow(BTNode<T> v)
        {
            if ((_order + 1) / 2 <= v.Child.Count) return; //递归基：当前节点并未下溢
            BTNode<T> p = v.ParentNode;
            if (null == p)
            { //递归基：已到根节点，没有孩子的下限
                if (v.Key.Count <= 0 && null == v.Child[0])
                {
                    //但倘若作为树根的v已不含关键码，却有（唯一的）非空孩子，则
                    /*DSA*/
                    _root = v.Child[0];
                    _root.ParentNode = null; //这个节点可被跳过
                    v.Child[0] = null; //release(v); //并因不再有用而被销毁
                } //整树高度降低一层
                return;
            }
            int r = 0;
            while (p.Child[r] != v)
            {
                r++;
            }

            //确定v是p的第r个孩子——此时v可能不含关键码，故不能通过关键码查找
            //另外，在实现了孩子指针的判等器之后，也可直接调用Vector::find()定位
            /*DSA*/
            // 情况1：向左兄弟借关键码
            if (0 < r)
            { //若v不是p的第一个孩子，则
                BTNode<T> ls = p.Child[r - 1]; //左兄弟必存在
                if ((_order + 1) / 2 < ls.Child.Count)
                { //若该兄弟足够“胖”，则
                  /*DSA*/
                    v.Key.Insert(0, p.Key[r - 1]); //p借出一个关键码给v（作为最小关键码）
                    T key = ls.Key[ls.Key.Count - 1];
                    ls.Key.RemoveAt(ls.Key.Count - 1);
                    p.Key[r - 1] = key; //ls的最大关键码转入p

                    BTNode<T> node = ls.Child[ls.Child.Count - 1];
                    ls.Child.RemoveAt(ls.Child.Count - 1);
                    v.Child.Insert(0, node);
                    //同时ls的最右侧孩子过继给v
                    if (null != v.Child[0])
                    {
                        v.Child[0].ParentNode = v; //作为v的最左侧孩子
                    }
                    return; //至此，通过右旋已完成当前层（以及所有层）的下溢处理
                }
            } //至此，左兄弟要么为空，要么太“瘦”
              // 情况2：向右兄弟借关键码
            if (p.Child.Count - 1 > r)
            { //若v不是p的最后一个孩子，则
                BTNode<T> rs = p.Child[r + 1]; //右兄弟必存在
                if ((_order + 1) / 2 < rs.Child.Count)
                { //若该兄弟足够“胖”，则
                  /*DSA*/
                    v.Key.Insert(v.Key.Count, p.Key[r]); //p借出一个关键码给v（作为最大关键码）
                    T key = rs.Key[0];
                    rs.Key.RemoveAt(0);
                    p.Key[r] = key; //rs的最小关键码转入p

                    BTNode<T> node = rs.Child[0];
                    rs.Child.RemoveAt(0);
                    v.Child.Insert(v.Child.Count, node);
                    //同时rs的最左侧孩子过继给v
                    if (null != v.Child[v.Child.Count - 1]) //作为v的最右侧孩子
                    {
                        v.Child[v.Child.Count - 1].ParentNode = v;
                    }
                    return; //至此，通过左旋已完成当前层（以及所有层）的下溢处理
                }
            } //至此，右兄弟要么为空，要么太“瘦”
              // 情况3：左、右兄弟要么为空（但不可能同时），要么都太“瘦”——合并
            if (0 < r)
            { //与左兄弟合并
              /*DSA*/
                BTNode<T> ls = p.Child[r - 1]; //左兄弟必存在
                T key = p.Key[r - 1];
                p.Key.RemoveAt(r - 1);
                ls.Key.Insert(ls.Key.Count, key);
                p.Child.RemoveAt(r);

                //p的第r - 1个关键码转入ls，v不再是p的第r个孩子
                BTNode<T> node = v.Child[0];
                v.Child.RemoveAt(0);
                ls.Child.Insert(ls.Child.Count, node);
                if (null != ls.Child[ls.Child.Count - 1]) //v的最左侧孩子过继给ls做最右侧孩子
                {
                    ls.Child[ls.Child.Count - 1].ParentNode = ls;
                }

                while (v.Key.Count() > 0)
                { //v剩余的关键码和孩子，依次转入ls
                    T key2 = v.Key[0];
                    v.Key.RemoveAt(0);
                    ls.Key.Insert(ls.Key.Count, key2);

                    BTNode<T> node2 = v.Child[0];
                    v.Child.RemoveAt(0);
                    ls.Child.Insert(ls.Child.Count, node2);
                    if (null != ls.Child[ls.Child.Count - 1])
                    {
                        ls.Child[ls.Child.Count - 1].ParentNode = ls;
                    }
                }
                //release(v); //释放v
            }
            else
            { //与右兄弟合并
              /*DSA*/
               // printf(" ... case 3R\n");
                BTNode<T> rs = p.Child[r + 1]; //右兄弟必存在
                T key = p.Key[r];
                    p.Key.RemoveAt(r);
                rs.Key.Insert(0, key); p.Child.RemoveAt(r);
                //p的第r个关键码转入rs，v不再是p的第r个孩子

                BTNode<T> node = v.Child[v.Child.Count - 1];
                v.Child.RemoveAt(v.Child.Count - 1);
                rs.Child.Insert(0, node);
                if (null != rs.Child[0])
                {
                    rs.Child[0].ParentNode = rs; //v的最左侧孩子过继给ls做最右侧孩子
                }
                while (v.Key.Count > 0)
                { //v剩余的关键码和孩子，依次转入rs
                    T key2 = v.Key[v.Key.Count - 1];
                    v.Key.RemoveAt(v.Key.Count - 1);
                    rs.Key.Insert(0, key2);

                    BTNode<T> node2 = v.Child[v.Child.Count - 1];
                    v.Child.RemoveAt(v.Child.Count - 1);
                    rs.Child.Insert(0, node2);
                    if (null != rs.Child[0])
                    {
                        rs.Child[0].ParentNode = rs;
                    }
                }
                //release(v); //释放v
            }
            SolveUnderflow(p); //上升一层，如有必要则继续分裂——至多递归O(logn)层
        }

        public List<BTNode<T>> TraverseLevel(BTNode<T> node)
        {
            List<BTNode<T>> list = new List<BTNode<T>>();
            if (null == node)
            {
                return list;
            }

            Queue<int> countQueue = new Queue<int>();

            Queue<BTNode<T>> queue = new Queue<BTNode<T>>();
            queue.Enqueue(node);

            countQueue.Enqueue(0);

            while (queue.Count > 0)
            {
                node = queue.Dequeue();

                int count = countQueue.Dequeue();

                if (count == 0)
                {
                    Console.WriteLine("====================================================");
                    Console.WriteLine("");
                }

                if (null == node)
                {
                    continue;
                }

                StringBuilder sb = new StringBuilder();
                if (null != node.ParentNode)
                {
                    for (int i = 0; i < node.ParentNode.Key.Count; ++i)
                    {
                        sb.Append(node.ParentNode.Key[i] + "   ");
                    }
                    sb.Append("Child:  ");
                }
                else
                {
                    sb.Append("Root:  ");
                }
                for (int i = 0; i < node.Key.Count; ++i)
                {
                    sb.Append(node.Key[i] + "   ");
                }
                sb.AppendLine("");
                sb.AppendLine("");

                Console.WriteLine(sb.ToString() + "   ");

                list.Add(node);
                for (int i = 0; i < node.Child.Count; ++i)
                {
                    queue.Enqueue(node.Child[i]);
                    countQueue.Enqueue(i);
                }
            }
            return list;
        }

    }
}
