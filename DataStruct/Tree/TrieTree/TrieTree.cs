using System;
using System.Collections.Generic;

namespace DataStruct.Tree.TrieTree
{
    /// <summary>
    /// 前缀树节点
    /// </summary>
    public class TrieNode
    {
        // 节点存的值
        public string value = string.Empty;

        // 经过该节点的次数
        public int passCount = 0;

        // 以此节点为终点的数量
        public int endCount = 0;

        // 子节点
        public Dictionary<string, TrieNode> childMap = new Dictionary<string, TrieNode>();
    }

    public class TrieTree
    {
        private TrieNode rootNode = null;

        // 下面代码中处理的字符串是以下划线分割的字符串，如 A_B_C_D

        public TrieTree()
        {
            rootNode = new TrieNode();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="msg"></param>
        public void Insert(string msg)
        {
            string[] arr = msg.Split('_');
            int index = 0;
            TrieNode node = rootNode;
            while (index < arr.Length)
            {
                string key = arr[index];
                TrieNode childNode = null;
                // 子节点中不包含 key 则创建一个节点添加
                if ( !node.childMap.TryGetValue(key, out childNode))
                {
                    childNode = new TrieNode();
                    childNode.value = key;
                    childNode.passCount = 0;
                    childNode.endCount = 0;
                    node.childMap[key] = childNode;
                }

                // 经过该节点的次数 +1
                childNode.passCount++;
                if (index >= arr.Length - 1)
                {
                    // 如果是结尾，则结尾数+1
                    childNode.endCount++;
                }
                
                // 令 node 等于 子节点
                node = childNode;
                ++index;
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public TrieNode Search(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return rootNode;
            }

            string[] arr = msg.Split('_');
            int index = 0;
            TrieNode node = rootNode;
            // 深度优先遍历
            while (index < arr.Length)
            {
                string key = arr[index];
                TrieNode childNode = null;
                // 子节点中以 key 查找
                if (!node.childMap.TryGetValue(key, out childNode))
                {
                    break;
                }

                if (node.passCount <= 0)
                {
                    break;
                }

                // 令 node 等于子节点
                node = childNode;
                ++index;
            }

            if (index < arr.Length)
            {
                return null;
            }

            return (node.endCount > 0) ? node : null;
        }

        /// <summary>
        /// 删除 msg
        /// 前缀树不会删除节点，只是修改节点记录的 passCount、endCount
        /// </summary>
        /// <param name="msg"></param>
        public void Remove(string msg)
        {
            string[] arr = msg.Split('_');
            int index = 0;
            TrieNode node = rootNode;
            while (index < arr.Length)
            {
                string key = arr[index];
                // 子节点中以 key 查找
                if (!node.childMap.TryGetValue(key, out node))
                {
                    break;
                }

                // 经过该节点的次数 -1
                node.passCount--;
                if (index == arr.Length - 1)
                {
                    // 如果是结尾，则结尾数 -1
                    node.endCount--;
                }
                ++index;
            }
        }

        /// <summary>
        /// 计算以 msg 为前缀的数量
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int PrefixCount(string msg)
        {
            TrieNode node = Search(msg);
            if (null == node)
            {
                return 0;
            }
            return node.passCount;
        }

        /// <summary>
        /// 计算存储的 msg 个数
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int EndCount(string msg)
        {
            TrieNode node = Search(msg);
            if (null == node)
            {
                return 0;
            }
            return node.endCount;
        }

        /// <summary>
        /// 打印所有前缀为 msg 的信息
        /// </summary>
        /// <param name="msg"></param>
        public void PrefixTraverse(string msg)
        {
            // 先查找以 msg 为前缀的节点
            TrieNode node = Search(msg);
            if (null == node)
            {
                return;
            }

            List<string> list = new List<string>();
            list.Add(msg);
            // 遍历 所有子节点
            foreach(var childNode in node.childMap.Values)
            {
                BackTracing(childNode, list);
            }
        }

        /// <summary>
        /// 回溯的查找所有子节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="list"></param>
        private void BackTracing(TrieNode node, List<string> list)
        {
            // 将节点的值添加到 list
            list.Add(node.value);

            // 如果节点是结尾则，将整个字符串打印出来
            if (node.endCount > 0)
            {
                string msg = string.Empty;
                foreach(var value in list)
                {
                    msg += value;
                }
                Console.WriteLine(msg);
            }

            // 遍历所有子节点
            foreach(var childNode in node.childMap.Values)
            {
                // 递归调用回溯算法
                BackTracing(childNode, list);
            }
            // 将节点的值从 list 中删除 (此为回溯)
            list.RemoveAt(list.Count - 1);
        }
    }

    public class TrieTreeTest
    {
        private static TrieTree tree = new TrieTree();

        private static List<string> list = new List<string>() {
            "A_B",
            "A_B_C_D",
            "A_B_C_D",
            "A_B_C_D",
            "A_B_C_F",
            "A_B_E",
            "A_B_E_D",
            "B_C",
            "B_C_D",
            "B_C_E"
        };

        public static void Test()
        {
            foreach (var msg in list)
            {
                tree.Insert(msg);
            }

            TrieNode node = tree.Search("A");

            foreach (var msg in list)
            {
                int preCount = tree.PrefixCount(msg);
                int endCount = tree.EndCount(msg);

                Console.WriteLine(msg + "  pre:" + preCount + "   end:" + endCount);
            }

            Console.WriteLine("=======================\n");
            tree.PrefixTraverse("");
            Console.WriteLine("=======================\n");

            tree.Remove("A_B_C_D");
            tree.Remove("A_B_C_D");
            tree.Remove("B_C_D");

            foreach (var msg in list)
            {
                int preCount = tree.PrefixCount(msg);
                int endCount = tree.EndCount(msg);
                Console.WriteLine(msg + "  pre:" + preCount + "   end:" + endCount);
            }

            Console.WriteLine("=======================\n");
            tree.PrefixTraverse("");
            Console.WriteLine("=======================\n");

        }

    }
}
