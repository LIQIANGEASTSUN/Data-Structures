using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.Log
{
    class LogArr
    {
        public static void Log(int[] arr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var v in arr)
            {
                sb.Append(v + "    ");
            }
            Console.WriteLine(sb.ToString());
        }

        public static void Log(int[] arr, int low, int high)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = low; i < high; ++i)
            {
                int v = arr[i];
                sb.Append(v + "    ");
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
