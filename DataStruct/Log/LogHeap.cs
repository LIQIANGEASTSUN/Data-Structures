﻿using System;
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

        /*
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
         */

    }


}
