using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Tree.BTree
{
    public class BTreeTest
    {
        private static BTree<int> bTree;
        public static void Test()
        {
            TestInit();
            bTree.TraverseLevel(bTree.Root);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Start Insert");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            TestInsert(bTree);
            bTree.TraverseLevel(bTree.Root);

            Console.WriteLine();
            Console.WriteLine("Start Search");
            TestSearch(bTree);

            Console.WriteLine("Start Remove");
            TestRemove(bTree);
            bTree.TraverseLevel(bTree.Root);
            TestSearch(bTree);

        }

        private static void TestInit()
        {
            bTree = new BTree<int>(5);
            BTNode<int> root = new BTNode<int>();
            bTree.Root = root;
        }

        private static void TestInsert(BTree<int> bTree)
        {
            bTree.Insert(13);
            bTree.Insert(25);
            bTree.Insert(66);
            bTree.Insert(67);
            bTree.Insert(68);
            bTree.Insert(39);
            bTree.Insert(40);
            bTree.Insert(55);
            bTree.Insert(5);
            bTree.Insert(90);
            bTree.Insert(30);
            bTree.Insert(28);
        }

        private static void TestSearch(BTree<int> bTree)
        {
            TestSearch(bTree, 13);
            TestSearch(bTree, 25);
            TestSearch(bTree, 66);
            TestSearch(bTree, 67);
            TestSearch(bTree, 68);
            TestSearch(bTree, 39);
            TestSearch(bTree, 40);
            TestSearch(bTree, 55);
            TestSearch(bTree, 5);
            TestSearch(bTree, 90);
            TestSearch(bTree, 30);
            TestSearch(bTree, 28);

            TestSearch(bTree, 99);
            Console.WriteLine();
        }

        private static void TestSearch(BTree<int> bTree, int value)
        {
            StringBuilder sb = new StringBuilder();
            BTNode<int> node = null;

            sb.AppendLine("Search: " + value);
            node = bTree.Search(value);

            if (null != node)
            {
                sb.AppendLine("Search success:");
                for (int i = 0; i < node.KeyList.Count; ++i)
                {
                    sb.Append(node.KeyList[i].ToString() + "  ");
                }
            }
            else
            {
                sb.Append("Search Fail");
            }
            Console.WriteLine(sb.ToString());
            Console.WriteLine();
        }

        private static void TestRemove(BTree<int> bTree)
        {
            bTree.Remove(13);
            bTree.Remove(25);
            bTree.Remove(66);
            bTree.Remove(67);
            bTree.Remove(68);
            bTree.Remove(39);
            bTree.Remove(40);
            bTree.Remove(55);
            bTree.Remove(5);
            bTree.Remove(90);
            bTree.Remove(30);
            bTree.Remove(28);

            bTree.Remove(111);
            bTree.Remove(222);
            bTree.Remove(333);
            bTree.Remove(444);
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
                for (int i = 0; i < v.KeyList.Count; ++i)
                {
                    int compare = v.KeyList[i].CompareTo(t);
                    if (compare <= 0)
                    {
                        index = i;
                        if (compare == 0)
                        {
                            break;
                        }
                    }
                }

                // 若成功，则返回
                if (index >= 0 && v.KeyList[index].CompareTo(t) == 0)
                {
                    return v;
                }

                _hot = v;
                // 沿引用转至对应的下层子树，并载入其根
                v = v.ChildList.Count > (index + 1) ? v.ChildList[index + 1] : null;
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
            //for (int i = 0; i < _hot.Key.Count; ++i)
            //{
            //    T value = _hot.Key[i];
            //    int compare = value.CompareTo(t);
            //    if (compare > 0)
            //    {
            //        break;
            //    }
            //    ++index;
            //}

            for (int i = 0; i < _hot.KeyList.Count; ++i)
            {
                int compare = _hot.KeyList[i].CompareTo(t);
                if (compare <= 0)
                {
                    index = i;
                    if (compare == 0)
                    {
                        break;
                    }
                }
            }

            _hot.KeyList.Insert(index + 1, t);      // 将新关键码插至对应的位置
            _hot.ChildList.Insert(index + 2, null); // 创建一个空子树指针

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
            for (int i = 0; i < node.KeyList.Count; ++i)
            {
                if(node.KeyList[i].CompareTo(t) == 0)
                {
                    index = i;
                    break;
                }
            }

            // node 不是叶子节点
            if (null != node.ChildList[0])
            {
                BTNode<T> u = node.ChildList[index + 1]; // 在右子树中一直向左，即可
                while (null != u.ChildList[0])
                {
                    u = u.ChildList[0]; // 找到 t 的后继（必需于某叶节点）
                }
                // 至此，node 必然位于最底层，且其中第 r 个关键码就是待删除者

                node.KeyList[index] = u.KeyList[0];
                node = u;  // 并与之交换位置
                index = 0;
            }

            node.KeyList.RemoveAt(index);
            node.ChildList.RemoveAt(index + 1);

            SolveUnderflow(node); // 如有必要，需做旋转或合并

            return false;
        }

        /// <summary>
        /// 上溢:因插入而上溢后的分裂处理
        /// </summary>
        public void SolveOverflow(BTNode<T> v)
        {
            if (_order >= v.ChildList.Count)
            {
                return; //递归基：当前节点并未上溢
            }

            int s = _order / 2; //轴点（此时应有_order = key.Count = child.Count - 1）
            BTNode<T> u = new BTNode<T>(); //注意：新节点已有一个空孩子
            for (int j = 0; j < _order - s - 1; j++)
            { //v右侧_order-s-1个孩子及关键码分裂为右侧节点u
                BTNode<T> node = v.ChildList[s + 1];
                v.ChildList.RemoveAt(s + 1);
                u.ChildList.Insert(j, node); //逐个移动效率低

                T key = v.KeyList[s + 1];
                v.KeyList.RemoveAt(s + 1);
                u.KeyList.Insert(j, key); //此策略可改进
            }

            BTNode<T> node2 = v.ChildList[s + 1];
            v.ChildList.RemoveAt(s + 1);
            u.ChildList[_order - s - 1] = node2; //移动v最靠右的孩子
            if (null != u.ChildList[0]) //若u的孩子们非空，则
            {
                for (int j = 0; j < _order - s; j++) //令它们的父节点统一
                {
                    u.ChildList[j].ParentNode = u; //指向u
                }
            }
                
            BTNode<T> p = v.ParentNode; //v当前的父节点p
            if (null == p)
            {
                _root = p = new BTNode<T>();
                p.ChildList[0] = v;
                v.ParentNode = p;
            } //若p空则创建之

            int index = -1;
            for (int i = 0; i < p.KeyList.Count; ++i)
            {
                int compare = p.KeyList[i].CompareTo(v.KeyList[0]);
                if (compare <= 0)
                {
                    index = i;
                    if (compare == 0)
                    {
                        break;
                    }
                }
            }

            int r = 1 + index; //p中指向u的指针的秩

            T key2 = v.KeyList[s];
            v.KeyList.RemoveAt(s);
            p.KeyList.Insert(r, key2); //轴点关键码上升
            p.ChildList.Insert(r + 1, u); u.ParentNode = p; //新节点u与父节点p互联
            SolveOverflow(p); //上升一层，如有必要则继续分裂——至多递归O(logn)层
        }

        /// <summary>
        /// 下溢:因删除而下溢后的合并处理
        /// </summary>
        /// <param name="node"></param>
        public void SolveUnderflow(BTNode<T> v)
        {
            if ((_order + 1) / 2 <= v.ChildList.Count) return; //递归基：当前节点并未下溢
            BTNode<T> p = v.ParentNode;
            if (null == p)
            { //递归基：已到根节点，没有孩子的下限
                if (v.KeyList.Count <= 0 && null != v.ChildList[0])
                {
                    //但倘若作为树根的v已不含关键码，却有（唯一的）非空孩子，则
                    /*DSA*/
                    _root = v.ChildList[0];
                    _root.ParentNode = null; //这个节点可被跳过
                    v.ChildList[0] = null; //release(v); //并因不再有用而被销毁
                } //整树高度降低一层
                return;
            }
            int r = 0;
            while (p.ChildList[r] != v)
            {
                r++;
            }

            //确定v是p的第r个孩子——此时v可能不含关键码，故不能通过关键码查找
            //另外，在实现了孩子指针的判等器之后，也可直接调用Vector::find()定位
            /*DSA*/
            // 情况1：向左兄弟借关键码
            if (0 < r)
            { //若v不是p的第一个孩子，则
                BTNode<T> ls = p.ChildList[r - 1]; //左兄弟必存在
                if ((_order + 1) / 2 < ls.ChildList.Count)
                { //若该兄弟足够“胖”，则
                  /*DSA*/
                    v.KeyList.Insert(0, p.KeyList[r - 1]); //p借出一个关键码给v（作为最小关键码）
                    T key = ls.KeyList[ls.KeyList.Count - 1];
                    ls.KeyList.RemoveAt(ls.KeyList.Count - 1);
                    p.KeyList[r - 1] = key; //ls的最大关键码转入p

                    BTNode<T> node = ls.ChildList[ls.ChildList.Count - 1];
                    ls.ChildList.RemoveAt(ls.ChildList.Count - 1);
                    v.ChildList.Insert(0, node);
                    //同时ls的最右侧孩子过继给v
                    if (null != v.ChildList[0])
                    {
                        v.ChildList[0].ParentNode = v; //作为v的最左侧孩子
                    }
                    return; //至此，通过右旋已完成当前层（以及所有层）的下溢处理
                }
            } //至此，左兄弟要么为空，要么太“瘦”
              // 情况2：向右兄弟借关键码
            if (p.ChildList.Count - 1 > r)
            { //若v不是p的最后一个孩子，则
                BTNode<T> rs = p.ChildList[r + 1]; //右兄弟必存在
                if ((_order + 1) / 2 < rs.ChildList.Count)
                { //若该兄弟足够“胖”，则
                  /*DSA*/
                    v.KeyList.Insert(v.KeyList.Count, p.KeyList[r]); //p借出一个关键码给v（作为最大关键码）
                    T key = rs.KeyList[0];
                    rs.KeyList.RemoveAt(0);
                    p.KeyList[r] = key; //rs的最小关键码转入p

                    BTNode<T> node = rs.ChildList[0];
                    rs.ChildList.RemoveAt(0);
                    v.ChildList.Insert(v.ChildList.Count, node);
                    //同时rs的最左侧孩子过继给v
                    if (null != v.ChildList[v.ChildList.Count - 1]) //作为v的最右侧孩子
                    {
                        v.ChildList[v.ChildList.Count - 1].ParentNode = v;
                    }
                    return; //至此，通过左旋已完成当前层（以及所有层）的下溢处理
                }
            } //至此，右兄弟要么为空，要么太“瘦”
              // 情况3：左、右兄弟要么为空（但不可能同时），要么都太“瘦”——合并
            if (0 < r)
            { //与左兄弟合并
              /*DSA*/
                BTNode<T> ls = p.ChildList[r - 1]; //左兄弟必存在
                T key = p.KeyList[r - 1];
                p.KeyList.RemoveAt(r - 1);
                ls.KeyList.Insert(ls.KeyList.Count, key);
                p.ChildList.RemoveAt(r);

                //p的第r - 1个关键码转入ls，v不再是p的第r个孩子
                BTNode<T> node = v.ChildList[0];
                v.ChildList.RemoveAt(0);
                ls.ChildList.Insert(ls.ChildList.Count, node);
                if (null != ls.ChildList[ls.ChildList.Count - 1]) //v的最左侧孩子过继给ls做最右侧孩子
                {
                    ls.ChildList[ls.ChildList.Count - 1].ParentNode = ls;
                }

                while (v.KeyList.Count() > 0)
                { //v剩余的关键码和孩子，依次转入ls
                    T key2 = v.KeyList[0];
                    v.KeyList.RemoveAt(0);
                    ls.KeyList.Insert(ls.KeyList.Count, key2);

                    BTNode<T> node2 = v.ChildList[0];
                    v.ChildList.RemoveAt(0);
                    ls.ChildList.Insert(ls.ChildList.Count, node2);
                    if (null != ls.ChildList[ls.ChildList.Count - 1])
                    {
                        ls.ChildList[ls.ChildList.Count - 1].ParentNode = ls;
                    }
                }
                //release(v); //释放v
            }
            else
            { //与右兄弟合并
              /*DSA*/
               // printf(" ... case 3R\n");
                BTNode<T> rs = p.ChildList[r + 1]; //右兄弟必存在
                T key = p.KeyList[r];
                    p.KeyList.RemoveAt(r);
                rs.KeyList.Insert(0, key); p.ChildList.RemoveAt(r);
                //p的第r个关键码转入rs，v不再是p的第r个孩子

                BTNode<T> node = v.ChildList[v.ChildList.Count - 1];
                v.ChildList.RemoveAt(v.ChildList.Count - 1);
                rs.ChildList.Insert(0, node);
                if (null != rs.ChildList[0])
                {
                    rs.ChildList[0].ParentNode = rs; //v的最左侧孩子过继给ls做最右侧孩子
                }
                while (v.KeyList.Count > 0)
                { //v剩余的关键码和孩子，依次转入rs
                    T key2 = v.KeyList[v.KeyList.Count - 1];
                    v.KeyList.RemoveAt(v.KeyList.Count - 1);
                    rs.KeyList.Insert(0, key2);

                    BTNode<T> node2 = v.ChildList[v.ChildList.Count - 1];
                    v.ChildList.RemoveAt(v.ChildList.Count - 1);
                    rs.ChildList.Insert(0, node2);
                    if (null != rs.ChildList[0])
                    {
                        rs.ChildList[0].ParentNode = rs;
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

            int deep = 0;
            while (queue.Count > 0)
            {
                node = queue.Dequeue();

                int count = countQueue.Dequeue();

                if (count == 0)
                {
                    //Console.WriteLine("====================================================");
                    //Console.WriteLine("");
                    Console.Write("      ");
                }

                if (null == node)
                {
                    continue;
                }

                if (deep != Deep(node))
                {
                    deep = Deep(node);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("(");
                for (int i = 0; i < node.KeyList.Count; ++i)
                {
                    sb.Append(node.KeyList[i] + ",");
                }
                sb.Append(")");

                Console.Write(sb.ToString() + " ");

                list.Add(node);
                for (int i = 0; i < node.ChildList.Count; ++i)
                {
                    queue.Enqueue(node.ChildList[i]);
                    countQueue.Enqueue(i);
                }
            }
            return list;
        }

        private int Deep(BTNode<T> node)
        {
            int deep = -1;
            while (null != node)
            {
                ++deep;
                node = node.ParentNode;
            }
            return deep;
        }

    }
}
