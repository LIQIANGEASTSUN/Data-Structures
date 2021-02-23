using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Log
{
    class LogHeap
    {
        private static int width = 10;
        private static int totalWidth = 0;
        private static int[] spArr = null;
        public static void Log(int[] arr)
        {
            int high = High(arr, 0);
            totalWidth = 2 * high * width;

            spArr = new int[arr.Length];
            NodePos(arr, 0, 1, 0);

            Draw(arr, 0);
        }

        private static void NodePos(int[] arr, int index, int deep, int offset)
        {
            if (index >= arr.Length)
            {
                return;
            }

            if (index == 0)
            {
                spArr[index] = totalWidth / 2;
            }
            else
            {
                int parentIndex = (index - 1) >> 1;
                spArr[index] = spArr[parentIndex] + offset;
            }

            offset = totalWidth / (int)Math.Pow(2, deep) / 2;
            int lc = index * 2 + 1;
            NodePos(arr, lc, deep + 1, offset * -1);

            int rc = index * 2 + 2;
            NodePos(arr, rc, deep + 1, offset);
        }

        private static int High(int[] arr, int index)
        {
            if (index >= arr.Length)
            {
                return 0;
            }

            int lc = index * 2 + 1;
            int highLc = High(arr, lc);

            int rc = index * 2 + 2;
            int highRc = highRc = High(arr, rc);

            return highLc >= highRc ? ++highLc : ++highRc;
        }

        private static void Draw(int[] arr, int index)
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

                    int lc = index * 2 + 1;
                    if (arr.Length > lc)
                    {
                        list.Add(lc);
                    }

                    int rc = index * 2 + 2;
                    if (arr.Length > rc)
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

        private static void DrawLineToChild(int[] arr, List<int> list, int lineCount)
        {
            int i = 0;
            int writeIndex = 0;
            int index = 0;

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            while (i < lineCount && list.Count > i)
            {
                index = list[i];
                int lc = index * 2 + 1;
                int leftSpace = (arr.Length > lc) ? spArr[lc] : spArr[index];

                int rc = index * 2 + 2;
                int rightSpace = (arr.Length > rc) ? spArr[rc] : spArr[index];

                while (writeIndex < totalWidth)
                {
                    if (leftSpace == rightSpace)
                    {
                        break;
                    }
                    ++writeIndex;
                    string split = (writeIndex < leftSpace || writeIndex > rightSpace + 1) ? " " : "-";
                    sb1.Append(split);
                    if (writeIndex == leftSpace && arr.Length > lc)
                    {
                        sb2.Append("|");
                        sb3.Append(" ");
                        Replace(sb3, arr[lc], writeIndex);
                    }
                    else if (writeIndex == rightSpace && arr.Length > rc)
                    {
                        sb2.Append("|");
                        sb3.Append(" ");
                        Replace(sb3, arr[rc], writeIndex);
                    }
                    else
                    {
                        sb2.Append(" ");
                        sb3.Append(" ");
                    }

                    if (writeIndex >= rightSpace)
                    {
                        break;
                    }
                }

                ++i;
            }
            Console.WriteLine(sb1.ToString());
            Console.WriteLine(sb2.ToString());
            Console.WriteLine(sb3.ToString());
        }

        private static void Replace(StringBuilder sb, int value, int index)
        {
            int count = 0;
            while (count < value.ToString().Length)
            {
                ++count;
                sb.Remove(sb.ToString().Length - 1, 1);
            }
            sb.Append(value.ToString());
        }
    }

}