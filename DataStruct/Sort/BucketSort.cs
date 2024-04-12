using System.Collections.Generic;
using DataStruct.Log;

namespace DataStruct.Sort
{
    // 桶排序
    class BucketSort
    {
        public void Test()
        {
            int[] arr = new int[] { 73, 30, 90, 89, 78, 10, 93, 60, 79, 98 };
            BucketSortFunc(arr);
            LogArr.Log(arr);
        }

        public void BucketSortFunc(int[] arr)
        {
            int max = arr[0];
            int min = arr[0];
            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }

            //桶数计算： 此处需要根据实际情况设计桶范围划分方法
            int bucketNum = (max - min) / arr.Length + 1;

            // 创建桶，因为需要空间未知所以用列表作为桶，便于扩展
            // 此处使用了 C# 的 List,可以根据自己使用语言修改，亦可自己实现一个链表
            List<List<int>> bucketList = new List<List<int>>(bucketNum);
            for (int i = 0; i < bucketNum; ++i)
            {
                bucketList.Add(new List<int>());
            }

            // 遍历所有元素，将元素放入相应的桶内
            for (int i = 0; i < arr.Length; ++i)
            {
                // 计算元素应该放置的桶位置
                // 次数算法必须遵照上方 桶数计算： 
                int num = (arr[i] - min) / arr.Length;
                bucketList[num].Add(arr[i]);
            }

            // 对每个桶进行排序
            for (int i = 0; i < bucketList.Count; ++i)
            {
                bucketList[i].Sort();
            }

            int index = 0;
            for (int i = 0; i < bucketList.Count; ++i)
            {
                List<int> dataList = bucketList[i];
                for (int j = 0; j < dataList.Count; ++j)
                {
                    arr[index] = dataList[j];
                    ++index;
                }
            }
        }
    }
}
