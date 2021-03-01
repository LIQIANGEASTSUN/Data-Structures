using System;
using System.Collections.Generic;
using System.Text;

namespace DataStruct.Log
{
    class LogBinTree<T>
    {
        private static int width = 12;
        private static int totalWidth = 0;
        private static int[] spArr = null;
        private static bool _showParent = false;
        public static void Log(LogNode<T>[] arr, bool showParent = false)
        {
            if (arr.Length <= 0)
            {
                return;
            }

            width = showParent ? 20 : 12;

            _showParent = showParent;
            totalWidth = 2 * High(arr, 0) * width;

            spArr = new int[arr.Length];
            NodePos(arr, 0, 1, 0);

            Draw(arr, 0);
        }

        private static void NodePos(LogNode<T>[] arr, int index, int deep, int offset)
        {
            if (index < 0 || index >= arr.Length)
            {
                return;
            }

            if (index == 0)
            {
                spArr[index] = totalWidth / 2;
            }
            else
            {
                int parentIndex = arr[index].ParentIndex();
                spArr[index] = spArr[parentIndex] + offset;
            }

            offset = totalWidth / (int)Math.Pow(2, deep) / 2;
            int lc = arr[index].LeftChildIndex();
            NodePos(arr, lc, deep + 1, offset * -1);

            int rc = arr[index].RightChildIndex();
            NodePos(arr, rc, deep + 1, offset);
        }

        private static int High(LogNode<T>[] arr, int index)
        {
            if (index < 0 || index >= arr.Length)
            {
                return 0;
            }
            int lc = arr[index].LeftChildIndex();
            int highLc = High(arr, lc);

            int rc = arr[index].RightChildIndex();
            int highRc = highRc = High(arr, rc);
            return highLc >= highRc ? ++highLc : ++highRc;
        }

        private static void Draw(LogNode<T>[] arr, int index)
        {
            List<int> list = new List<int>();
            list.Add(0);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= spArr[0]; ++i)
            {
                string split = i < spArr[0] ? " " : arr[0].ToString();
                sb.Append(split);
            }
            Console.WriteLine(sb.ToString());

            while (list.Count > 0)
            {
                int count = list.Count;
                int lineCount = count;
                while (count > 0)
                {
                    index = list[lineCount - count];
                    --count;
                    if (index < 0)
                    {
                        continue;
                    }

                    int lc = arr[index].LeftChildIndex();
                    if (arr.Length > lc && lc >= 0)
                    {
                        list.Add(lc);
                    }

                    int rc = arr[index].RightChildIndex();
                    if (arr.Length > rc && rc >= 0)
                    {
                        list.Add(rc);
                    }
                }

                DrawLineToChild(arr, list, lineCount);
                while (lineCount > 0 && list.Count > 0)
                {
                    --lineCount;
                    list.RemoveAt(0);
                }
            }
        }

        private static void DrawLineToChild(LogNode<T>[] arr, List<int> list, int lineCount)
        {
            int i = 0;
            int writeIndex = 0;
            int index = 0;

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            while (i < lineCount && list.Count > i)
            {
                index = list[i++];
                int lc = arr[index].LeftChildIndex();
                int leftSpace = (arr.Length > lc && lc >= 0) ? spArr[lc] : spArr[index];

                int rc = arr[index].RightChildIndex();
                int rightSpace = (arr.Length > rc && rc >= 0) ? spArr[rc] : spArr[index];

                while (writeIndex < totalWidth && writeIndex < rightSpace && leftSpace != rightSpace)
                {
                    ++writeIndex;
                    string split = (writeIndex < leftSpace || writeIndex > rightSpace + 1) ? " " : "-";
                    sb1.Append(split);
   
                    if (writeIndex == leftSpace && arr.Length > lc && lc >= 0)
                    {
                        sb2.Append("|");
                        sb3.Append(" ");
                        Replace(sb3, arr, lc);
                    }
                    else if (writeIndex == rightSpace && arr.Length > rc && rc >= 0)
                    {
                        sb2.Append("|");
                        sb3.Append(" ");
                        Replace(sb3, arr, rc);
                    }
                    else
                    {
                        sb2.Append(" ");
                        sb3.Append(" ");
                    }
                }
            }
            Console.WriteLine(sb1.ToString());
            Console.WriteLine(sb2.ToString());
            Console.WriteLine(sb3.ToString());
        }

        private static void Replace(StringBuilder sb, LogNode<T>[] arr, int index)
        {
            if (index < 0 || index >= arr.Length)
            {
                return;
            }

            if (arr[index].t.ToString().CompareTo("20") == 0
                || arr[index].t.ToString().CompareTo("19") == 0)
            {
                int a = 0;
            }

            LogNode<T> value = arr[index];
            string msg = value.ToString();

            int parentIndex = arr[index].ParentIndex();
            if (_showParent && arr[index].ParentIndex() >= 0 && arr[index].ParentIndex() < arr.Length)
            {
                msg = string.Format("{0}({1})", msg, arr[parentIndex].ToString());
            }

            int count = 0;
            while (count < msg.Length)
            {
                ++count;
                int length = sb.ToString().Length;
                if (length > 1)
                {
                    sb.Remove(length - 1, 1);
                }
            }
            sb.Append(msg);
        }
    }
}