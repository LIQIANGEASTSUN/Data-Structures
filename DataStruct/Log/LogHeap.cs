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
            for (int i = 0; i < arr.Length; ++i)
            {
                LogChild(arr, i);
            }
            Console.WriteLine("===================");

            int high = High(arr, 0);
            totalWidth = 2 * high * width;

            spArr = new int[arr.Length];
            NodePos(arr, 0, 1, 0);

            CCC(arr, 0);
        }

        private static void CCC(int[] arr, int index)
        {
            List<int> list = new List<int>();
            list.Add(0);

            while (list.Count > 0)
            {
                int count = list.Count;
                int lineCount = count;
                int spCount = 0;
                while (count > 0)
                {
                    index = list[lineCount - count];
                    --count;

                    for (int i = 0; i < spArr[index] - spCount - 1; ++i)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(arr[index]);
                    spCount = spArr[index];

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

                Console.WriteLine();

                DrawLineToChild(arr, list, lineCount);

                while (lineCount > 0 && list.Count > 0)
                {
                    --lineCount;
                    list.RemoveAt(0);
                }
            }
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
                    ++writeIndex;
                    string split = (writeIndex < leftSpace || writeIndex > rightSpace + 1) ? " " : "-";
                    sb1.Append(split);
                    if (writeIndex == leftSpace && arr.Length > lc)
                    {
                        sb2.Append("|");
                        sb3.Append(arr[lc].ToString());
                    }
                    else if (writeIndex == rightSpace && arr.Length > rc)
                    {
                        sb2.Append("|");
                        sb3.Append(arr[rc].ToString());
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

        private static int DrawLineToChild(int[] arr, int index, int space)
        {
            int lc = index * 2 + 1;
            int leftSpace = (arr.Length > lc) ? spArr[lc] : spArr[index];

            int rc = index * 2 + 2;
            int rightSpace = (arr.Length > rc) ? spArr[rc] : spArr[index];

            if (leftSpace == rightSpace)
            {
                return 0;
            }

            for (int i = 0; i < totalWidth - space; ++i)
            {
                if (i <= leftSpace || i > rightSpace + 1)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write("-");
                }
            }
            return rightSpace;
        }

        private static void LogChild(int[] arr, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(arr[index] + " ");
            int lc = index * 2 + 1;
            if (arr.Length > lc)
            {
                sb.Append("lc:" + arr[lc]);
            }

            int rc = index * 2 + 2;
            if (arr.Length > rc)
            {
                sb.Append("rc:" + arr[rc]);
            }

            Console.WriteLine(sb.ToString());
        }

    }


}




/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Log
{
    class LogHeap
    {

        public static void Log(int[] arr)
        {

            for (int i = 0; i < arr.Length; ++i)
            {
                LogChild(arr, i);
            }
            Console.WriteLine("===================");

            CCC(arr, 0);

            //Queue<int> queue = new Queue<int>();
            //queue.Enqueue(0);

            //while (queue.Count > 0)
            //{
            //    int index = queue.Dequeue();
            //    Console.Write(arr[index] + "=" + queue.Count + "=");
            //    Console.Write("    ");

            //    int lc = index * 2 + 1;
            //    if (arr.Length > lc)
            //    {
            //        queue.Enqueue(lc);
            //    }

            //    int rc = index * 2 + 2;
            //    if (arr.Length > rc)
            //    {
            //        queue.Enqueue(rc);
            //    }
            //}

            //Log(arr, 0, 0, 0);


            int leftWidth = LeftChildWidth(arr, 1);
            int rightWidth = RightChildWidth(arr, 1);
            Console.WriteLine(leftWidth + "   " + rightWidth);
        }

        private static void CCC(int[] arr, int index)
        {
            List<int> list = new List<int>();
            list.Add(0);
            int[] spArr = new int[arr.Length];

            int leftWidth = LeftChildWidth(arr, 1);
            int rightWidth = RightChildWidth(arr, 1);
            spArr[0] = (leftWidth + rightWidth + 2) * 8;

            while (list.Count > 0)
            {
                int count = list.Count;
                int spCount = 0;
                while (count > 0)
                {
                    index = list[0];
                    list.RemoveAt(0);
                    --count;
                    if (index == 7)
                    {
                        int a = 0;
                    }
                    
                    for (int i = 0; i < spArr[index] - spCount; ++i)
                    //for (int i = 0; i < spArr[index]; ++i)
                    {
                        Console.Write(" ");
                    }
                    spCount = spArr[index];

                    Console.Write(arr[index]);

                    if (count == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    int sp = spArr[index];

                    int lc = index * 2 + 1;
                    if (arr.Length > lc)
                    {
                        list.Add(lc);
                        rightWidth = RightChildWidth(arr, lc);
                        spArr[lc] = sp - 8 - rightWidth * 8;// sp - DDD(arr, lc, 0) * 8;
                    }

                    int rc = index * 2 + 2;
                    if (arr.Length > rc)
                    {
                        list.Add(rc);
                        leftWidth = LeftChildWidth(arr, rc);
                        spArr[rc] = sp + 8 + leftWidth * 8; //sp - DDD(arr, rc, 0) * 8;
                    }
                }
            }
        }

        private static int DDD(int[] arr, int index, int width)
        {
            int lc = index * 2 + 1;
            int rc = index * 2 + 2;
            while (arr.Length > lc || arr.Length > rc)
            {
                if (arr.Length > lc)
                {
                    width++;
                    lc = lc * 2 + 1;
                    //DDD(arr, lc, width);
                }

                if (arr.Length > rc)
                {
                    width++;
                    rc = rc * 2 + 2;
                    DDD(arr, rc, width);
                }
            }

            return width;
        }

        private static int LeftChildWidth(int[] arr, int index)
        {
            int lc = index * 2 + 1;
            int width = 0;
            while (arr.Length > lc)
            {
                width++;
                lc = lc * 2 + 1;
            }

            return width;
        }

        private static int RightChildWidth(int[] arr, int index)
        {
            int rc = index * 2 + 2;
            int width = 0;
            while (arr.Length > rc)
            {
                width++;
                rc = rc * 2 + 2;
            }

            return width;
        }

        private static void LogChild(int[] arr, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(arr[index] + " ");
            int lc = index * 2 + 1;
            if (arr.Length > lc)
            {
                sb.Append("lc:" + arr[lc]);
            }

            int rc = index * 2 + 2;
            if (arr.Length > rc)
            {
                sb.Append("rc:" + arr[rc]);
            }

            Console.WriteLine(sb.ToString());
        }

         private static void CCC(int[] arr, int index)
        {
            List<int> list = new List<int>();
            list.Add(0);
            int[] spArr = new int[arr.Length];
            spArr[0] = DDD(arr, 1, 0) * 8;

            while (list.Count > 0)
            {
                int count = list.Count;
                int spCount = 0;
                while (count > 0)
                {
                    index = list[0];
                    list.RemoveAt(0);
                    --count;
                    for (int i = 0; i < spArr[index] - spCount; ++i)
                    {
                        Console.Write(" ");
                    }
                    spCount = spArr[index];

                    Console.Write(arr[index]);

                    if (count == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    int sp = spArr[index];

                    int lc = index * 2 + 1;
                    if (arr.Length > lc)
                    {
                        list.Add(lc);
                        spArr[lc] = sp - 8;
                    }

                    int rc = index * 2 + 2;
                    if (arr.Length > rc)
                    {
                        list.Add(rc);
                        spArr[rc] = sp + 8;
                    }
                }
            }
        }

    }


}

*/